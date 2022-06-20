using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 接触判定(攻撃)を制御する
/// </summary>
public class AttackcolliderController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("攻撃力")]
    private int m_attackPower = 15;

    [SerializeField]
    [Tooltip("スタンス値の上昇値")]
    [Range(-1, 1)]
    private float m_upStanceValue = 0.1f;

    [SerializeField]
    [Tooltip("衝突時に発生するエフェクト")]
    private GameObject m_hitEffect = null;

    [SerializeField]
    [Tooltip("ヒットサウンド")]
    private AudioClip m_hit;

    [SerializeField]
    [Tooltip("攻撃対象のタグ名")]
    private string m_opponentTagName = "Player";

    [Tooltip("攻撃が有効かどうか")]
    private bool CanHit;

    private float attackCorrectionValue = 1f;

    private float defenceCorrectionValue = 1f;

    private int attackPower = 0;

    public int AttackPower => attackPower;

    private int frostattackPower = 0;

    private void Start()
    {
        CanHit = true;
        SetActiveAttack(false);
    }
    public void SetActiveAttack(bool value)
    {
        attackPower = value ? m_attackPower : 0;
        frostattackPower = value ? 1 : 0;

        CanHit = value ? true : false;
    }
    public void StartAttackCorrectionValue(float value, float time)
    {
        StopCoroutine(nameof(SetAttackEffectTime));
        StartCoroutine(SetAttackEffectTime(value, time));
    }

    IEnumerator SetAttackEffectTime(float value, float time)
    {
        var setValue = attackCorrectionValue > value ? attackCorrectionValue : value;
        attackCorrectionValue = setValue;
        yield return new WaitForSeconds(time);
        attackCorrectionValue = 1f;
    }

    public void StartDefenceCorrectionValue(float value, float time)
    {
        StopCoroutine(nameof(SetDefenceEffectTime));
        StartCoroutine(SetDefenceEffectTime(value, time));
    }

    IEnumerator SetDefenceEffectTime(float value, float time)
    {
        var setValue = defenceCorrectionValue > value ? defenceCorrectionValue : value;
        defenceCorrectionValue = setValue;
        yield return new WaitForSeconds(time);
        attackCorrectionValue = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item")) other.gameObject.GetComponentInParent<IDamage>().AddDamage(attackPower);
        
        if (other.CompareTag(m_opponentTagName) && CanHit)
        {
            var frostAttack = other.GetComponentInChildren<FrostAttackController>();
            if (frostAttack)
            {
                other.gameObject.GetComponentInParent<IDamage>().AddDamage(Mathf.CeilToInt(frostattackPower * frostAttack.Damage * attackCorrectionValue) );
                Destroy(frostAttack.gameObject);
            }

            var stance = GetComponentInParent<PlayerControll>();
            if (stance)
            {
                stance.AddStanceValue(m_upStanceValue);
                other.gameObject.GetComponentInParent<IDamage>().AddDamage(Mathf.CeilToInt(attackPower * attackCorrectionValue));
            }
            else
            {
                var idmg = other.gameObject.GetComponentInParent<IDamage>();
                idmg = idmg == null ? other.gameObject.GetComponent<IDamage>() : idmg;
                idmg.AddDamage(Mathf.CeilToInt(attackPower / defenceCorrectionValue));
            }

            if(m_hit)SoundManager.Instance.PlayHit(m_hit,gameObject.transform.position);
            if(m_hitEffect) Instantiate(m_hitEffect, other.ClosestPoint(transform.position), GameManager.Player.transform.rotation);
            CanHit = false;
        }
    }

    /// <summary>
    /// 追加ダメージを返す
    /// </summary>
    /// <param name="addDamage"></param>
    /// <returns></returns>
    public int AddDamageCount(int addDamage) { return m_attackPower + addDamage; }
}
