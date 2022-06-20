using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillBehavior : MonoBehaviour
{
    public static SkillBehavior Instance { get; private set; }

    [SerializeField]
    AttackcolliderController[] m_attackControllers = default;

    [SerializeField]
    AttackcolliderController[] m_defenceControllers = default;

    [SerializeField]
    UnityEvent _speedBuf;

    PlayerControll playerControll;
    PlayerManager playerManager;
    private void Awake()
    {
        Instance = this;
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

    }
    
    public void CallPassive(ref PassiveSkill passiveSkill)
    {
        switch (passiveSkill.PassiveType)
        {
            case PassiveType.SwordAttackBuf:
                foreach (var item in m_attackControllers)
                {
                    item.StartAttackCorrectionValue(passiveSkill.EffectAmount, passiveSkill.EffectableTime);
                }
                break;
            case PassiveType.DefenceBuf:
                foreach (var item in m_attackControllers)
                {
                    item.StartDefenceCorrectionValue(passiveSkill.EffectAmount, passiveSkill.EffectableTime);
                }
                break;
            case PassiveType.MoveSpeedBuf:
                playerControll.SpeedStart(passiveSkill.EffectableTime,passiveSkill.EffectAmount);
                break;
            case PassiveType.AttackSpeedBuf:
                playerControll.SetAttackspeedRateUp(passiveSkill.EffectableTime, passiveSkill.EffectAmount);
                break;
            case PassiveType.DodgeTimeBuf:
                playerManager.SetDodgeRate(passiveSkill.EffectableTime, passiveSkill.EffectAmount);
                break;
            case PassiveType.BulletAttackBuf:
                FindObjectOfType<BulletFire>().SetUpDamage(passiveSkill.EffectableTime, passiveSkill.EffectAmount);
                break;
            case PassiveType.DodgeDistanceBuf:
                playerControll.GetComponent<PlayerControll>().SetDodgeDistanceUp(passiveSkill.EffectableTime, passiveSkill.EffectAmount);
                break;
            case PassiveType.AttackReachBuf:
                break;
            default:
                break;
        }
    }
}
