using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(PlayerControll))]
public class PlayerManager : MonoBehaviour,IDamage
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    [Tooltip("プレイヤーの体力")]
    private int m_hp = 100;

    [SerializeField]
    [Tooltip("回避時の無敵時間")]
    private float m_godTime = 0.4f;

    [SerializeField]
    [Tooltip("回避成功時の無敵時間")]
    private float m_changeTime = 2f;

    [SerializeField]
    [Tooltip("回復時に発生するエフェクト")]
    private GameObject m_healEffect;

    [SerializeField]
    [Tooltip("死亡時に発生するプレイヤーの死体")]
    private GameObject m_dead;

    [SerializeField]
    [Tooltip("プレイヤーのアニメーター")]
    private Animator m_animator = null;

    [SerializeField]
    [Tooltip("体力赤バー")]
    private Image hpslider = null;

    [SerializeField]
    [Tooltip("体力緑バー")]
    private Image hpsliderGreen = null;

    [SerializeField]
    private PostEffect postEffect = null;
    
    private int m_maxhp;
    private bool IsInvisible = false;

    [HideInInspector]
    public bool IsAlive = true;
 
    private PlayerControll _playerControll;

    [HideInInspector]
    public enum StanceTypes
    {
        NORMAL,
        GOD,
    }

    public StanceTypes stanceTypes;

    private void Awake()
    {
        Instance = this;
        stanceTypes = StanceTypes.NORMAL;
        m_maxhp = m_hp;
        _playerControll = GetComponent<PlayerControll>();

    }

    public void AddDamage(int damage)
    {
        if (IsInvisible)
        {
            if (damage > 0) return;
        }
        if (m_hp > damage)
        {
            if(damage < 0)
            {
                Heal(damage);
            }
            else
            {
                if (damage > 1f)
                {
                    m_animator.Play("Damage", 0);
                     _playerControll.BasicHitAttack();
                }
                m_hp -= damage;
                if(damage > 1)SoundManager.Instance.PlayPlayerHit();
            }
            //Debug.Log($"UIComplete:{hpslider.fillAmount * 100}%");

            hpsliderGreen.DOFillAmount((float)m_hp / m_maxhp,
                0f).OnComplete(() =>
                {
                    //Debug.Log($"Complete:{(float)m_hp / m_maxhp*100}%");

                    hpslider.DOFillAmount((float)m_hp / m_maxhp,
                        1f).OnComplete(() => {
                            //Debug.Log("AllComplete");
                            //Debug.Log($"UIComplete:{hpslider.fillAmount * 100}%");
                        });
                }
            );

        }
        else
        {
            hpsliderGreen.DOFillAmount(0,
                0f).OnComplete(() =>
                {
                    //Debug.Log($"Complete:{(float)m_hp / m_maxhp*100}%");

                    hpslider.DOFillAmount(0,
                        1f).OnComplete(() => {
                            //Debug.Log("AllComplete");
                            //Debug.Log($"UIComplete:{hpslider.fillAmount * 100}%");
                        });
                }
            );
            var m =Instantiate(m_dead,transform.position,transform.rotation);
            GameManager.Instance.SetGameState(GameManager.GameState.PLAYERLOSE);
            gameObject.SetActive(false);
        }
    }

    private void Heal(int damage)
    {
        m_hp -= damage;
        if (m_hp >= m_maxhp) m_hp = m_maxhp;
        Instantiate(m_healEffect, transform.position, Quaternion.identity);
        SoundManager.Instance.PlayHeal();
    }

    public void SetInvisible()
    {
        StartCoroutine(nameof(Invisible));
    }

    bool ActiveDodge = false;
    IEnumerator Invisible()
    {
        SoundManager.Instance.PlayDodge();
        IsInvisible = true;
        ActiveDodge = true;
        yield return new WaitForSeconds(m_godTime);
        IsInvisible = false;
        ActiveDodge = false;
    }

    private float dodgeRate = 1f;

    IEnumerator DodgeRate(float time,float value)
    {
        dodgeRate = value;
        yield return new WaitForSeconds(time);
        dodgeRate = 1f;
    }

    public void SetDodgeRate(float time, float value)
    {
        StartCoroutine(DodgeRate(time, value));
    }
    IEnumerator Invisible(float time)
    {
        IsInvisible = true;
        yield return new WaitForSeconds(time);
        IsInvisible = false;
    }
    bool IsGod = false;
    bool IsActiveCoroutine = false;
    float timer = 0;
    IEnumerator GodTime()
    {
        SoundManager.Instance.PlayFrost();
        timer = 0;
        StopCoroutine(nameof(Invisible));
        ActiveDodge = false;
        IsInvisible = true;
        while (timer < m_changeTime)
        {
            timer += 0.02f;
            yield return new WaitForSeconds(0.02f);

        }
        //postEffect.enabled = false;
        IsInvisible = false;
        IsActiveCoroutine = false;
    }
}
