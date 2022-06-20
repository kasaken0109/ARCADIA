using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BulletType
{
    Lay,
    NonBullet,
    Skill,
}

public class BulletFire : MonoBehaviour
{
    [SerializeField]
    [Tooltip("弾のエフェクトの発生する地点")]
    Transform m_particleMuzzle = null;

    [SerializeField]
    [Tooltip("パッシブスキルのエフェクトが発生する地点")]
    Transform m_passiveEffectPoint = null;

    [SerializeField]
    [Tooltip("スタンス")]
    Image m_stance = default;

    [SerializeField]
    [Tooltip("スタンス値の消費量予測線")]
    private Image m_line = default;

    [SerializeField]
    private PlayerControll m_player;

    [SerializeField]
    private BulletInformation m_bullet;

    [SerializeField]
    private PassiveDisplayController m_passiveDisplay = default;

    private Bullet m_equip;

    private float stanceValue = 0.5f;

    float consumeValue;
    // Start is called before the first frame update
    void Start()
    {
        m_stance.fillAmount = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameStatus == GameManager.GameState.STOP) return;//一時停止中には射撃不可
        stanceValue = m_stance.fillAmount;

        if(m_equip != null)m_line.fillAmount = m_stance.fillAmount - consumeValue;
    }

    public void ShootBullet()
    {
        SoundManager.Instance.StopSE();

        //銃弾を生成
        if (stanceValue >= m_equip.ConsumeStanceValue)
        {
            SoundManager.Instance.PlayShoot();
            var instance = Instantiate(m_equip.MyBullet, m_particleMuzzle.position, Camera.main.transform.rotation);
            stanceValue -= m_equip.ConsumeStanceValue;
            m_stance.fillAmount = stanceValue;

            if (m_equip.BulletType == BulletType.Lay && instance.GetComponent<BulletLaserController>()) instance.GetComponent<BulletLaserController>().Damage = Mathf.CeilToInt(m_equip.Damage * addDamageRate);
            if (m_equip.BulletType == BulletType.Skill)
            {
                FindObjectOfType<PlayerManager>().AddDamage(Mathf.CeilToInt(m_equip.Damage * addDamageRate));
                Destroy(instance, 5);
            }
        }
        //パッシブ用のコストがある場合パッシブを発動
        if (stanceValue >= consumeValue - m_equip.ConsumeStanceValue)
        {
            CallPassiveInstance(m_equip.passiveSkill_1);
            CallPassiveInstance(m_equip.passiveSkill_2);
        }
    }

    private void CallPassiveInstance(PassiveSkill passiveSkill)
    {
        if (passiveSkill != null)
        {
            SkillBehavior.Instance.CallPassive(ref passiveSkill);
            var obj = Instantiate(passiveSkill.Effect, m_passiveEffectPoint.position, Quaternion.identity,m_passiveEffectPoint);
            var particle = obj.GetComponent<ParticleSystem>().main;
            particle.duration = passiveSkill.EffectableTime;
            stanceValue -= passiveSkill.ConsumeCost;
        }
    }

    public void EquipBullet(Bullet bullet) {
        m_equip = bullet;
        m_bullet._NameDisplay.text = bullet.Name;
        m_bullet._skill1Display.sprite = bullet.passiveSkill_1 ?bullet.passiveSkill_1.ImageBullet : null;
        m_bullet._skill2Display.sprite = bullet.passiveSkill_2 ? bullet.passiveSkill_2.ImageBullet:null;

        //パッシブスキルセット時のコストを計算
        consumeValue = m_equip.ConsumeStanceValue + (m_equip.PassiveSkill1 != null ? m_equip.PassiveSkill1.ConsumeCost : 0)
            + (m_equip.PassiveSkill2 != null ? m_equip.PassiveSkill2.ConsumeCost : 0);    
    }

    private float addDamageRate = 1;
    IEnumerator UpDamage(float time, float value)
    {
        addDamageRate = value;
        yield return new WaitForSeconds(time);
        addDamageRate = 1;
    }

    public void SetUpDamage(float time, float value)
    {
        StartCoroutine(UpDamage(time, value));
    }
}
