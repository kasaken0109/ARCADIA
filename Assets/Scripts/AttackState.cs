using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃の処理を行うState(現在は不使用)
/// </summary>
public class AttackState : StateBase
{
    [SerializeField] 
    private Animator m_animator = null;

    [SerializeField]
    private string m_stateDistance;

    [SerializeField] 
    private int m_maxBreathCount;

    [SerializeField] 
    private StateBase m_moveState;

    [SerializeField]
    private int[] attackValue;

    [SerializeField]
    private GameObject m_enemy = null;

    StateBase _idleState;

    int breathCount;
    string[] triggersDistances;
    List<int> triggerDistance;

    protected override void Setup()
    {
        _idleState = GetComponent<IdleState>();
        triggersDistances = m_stateDistance.Split(' ');
        triggerDistance = new List<int>();
        foreach (var item in triggersDistances)
        {
            triggerDistance.Add(int.Parse(item));
        }
        SetDelegate(Event.Enter, EnterCallback);
        SetDelegate(Event.Leave, ExitCallback);
    }

    int EnterCallback()
    {
        m_animator.SetBool("Attack", true);
        //StartCoroutine(nameof(AttackRoutine));
        if (_opponentTag == "Player")
        {
            var e = GetComponentInParent<EnemyBossManager>();
            //e.transform.LookAt(GameManager.Player.transform.position);
        }
        Debug.Log("EnterAttack");
        return 0;
    }

    int ExitCallback()
    {
        m_animator.SetBool("Attack", false);
        Debug.Log("ExitAttack");
        return 0;
    }

    void SetActionVariable()
    {
        int value = attackValue[Random.Range(0, attackValue.Length)];
        m_animator.SetInteger("AttackCombo", value);
    }

    
}
