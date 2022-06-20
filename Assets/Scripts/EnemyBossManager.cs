using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using BehaviourAI;

/// <summary>
/// 敵の状態を管理する
/// </summary>
public class EnemyBossManager : MonoBehaviour, IDamage
{
    public static EnemyBossManager Instance { get; private set; }

    [SerializeField]
    [Tooltip("HP")]
    private int m_hp = 100;

    [SerializeField]
    [Tooltip("怯み値")]
    [Range(1, 100)]
    private int m_rate;

    [SerializeField]
    [Tooltip("Animator")]
    private Animator m_animator = null;

    [SerializeField]
    [Tooltip("死亡時に発生する死体")]
    private GameObject m_deathBody = null;

    [SerializeField]
    [Tooltip("地面から砂が発生する攻撃のエフェクト")]
    private GameObject m_sandEffect = null;

    [SerializeField]
    private Image hpSlider;

    private bool IsCritical = false;//特殊攻撃のいフラグ

    int maxHp;
    int hitRate = 0;//怯み値
    int rateTemp;
    int count = 0;//特殊攻撃の回数

    float hitSpeed = 1f;//ヒットストップのスピード

    #region EnemyAnimatorHash
    int hpHash = Animator.StringToHash("HP");
    int mpHash = Animator.StringToHash("MP");

    #endregion

    public void AddDamage(int damage)
    {
        hitSpeed = (float)(damage / 10f);
        StopCoroutine(HitStop());
        StartCoroutine(HitStop());
        if (m_hp < maxHp * 0.5f && count == 0)
        {
            StartCoroutine(nameof(DeathCombo));
        }
        else if (m_hp < maxHp * 0.2f && count == 1)
        {
            StopCoroutine(nameof(DeathCombo));
            StartCoroutine(nameof(DeathCombo));
        }
        if (m_hp > damage)
        {
            m_hp -= damage;
            DOTween.To(
                () => hpSlider.fillAmount, // getter
                x => hpSlider.fillAmount = x, // setter
                (float)(float)m_hp / maxHp, // ターゲットとなる値
                1f  // 時間（秒）
                ).SetEase(Ease.OutCubic);
            hitRate += damage;
            if(hitRate >= m_rate)
            {
                m_animator.SetInteger(hpHash, 1);
                m_animator.SetTrigger("Hit");
                hitRate = 0;
            }
        }
        else
        {
            m_hp = 0;
            StopCoroutine(HitStop());
            Time.timeScale = 1;
            DOTween.To(
                () => hpSlider.fillAmount, // getter
                x => hpSlider.fillAmount = x, // setter
                (float)(float)0, // ターゲットとなる値
                1f  // 時間（秒）
                ).SetEase(Ease.OutCubic);
            Instantiate(m_deathBody,this.transform.position,this.transform.rotation);
            GameManager.Instance.SetGameState(GameManager.GameState.PLAYERWIN);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ヒットストップを発生させる
    /// </summary>
    /// <returns></returns>
    IEnumerator HitStop()
    {
        var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        source.GenerateImpulse();
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.1f * hitSpeed);
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        maxHp = m_hp;
    }

    IEnumerator DeathCombo()
    {
        IsCritical = true;
        count++;
        rateTemp = hitRate;
        hitRate = -500;
        yield return new WaitForSeconds(5f);
        float distance = Vector3.Distance(GameManager.Player.transform.position, gameObject.transform.position);
        
        int type = distance >= 7 ? 5 : 4;
        m_animator.SetTrigger("DeathAttack");
        m_animator.SetInteger("AttackType", type);
        yield return new WaitForSeconds(2f);
        IsCritical = false;
        hitRate = rateTemp;
    }

    public void SpawnEffects() => m_sandEffect.SetActive(true);
}
