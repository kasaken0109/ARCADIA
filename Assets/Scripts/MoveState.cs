using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : StateBase
{
    /// <summary>発生エフェクト</summary>
    [SerializeField] GameObject m_roarEffect = null;
    /// <summary>エフェクト発生地点</summary>
    [SerializeField] Transform m_spawnEffect = null;
    [SerializeField] StateBase _attackState;
    [SerializeField] EnemyBossManager enemy;
    [SerializeField] int m_runSpeed = 10;
    /// <summary>移動対象</summary>
    Vector3 m_cachedTargetPosition;
    StateBase _idleState;
    Animator animator;
    NavMeshAgent m_agent;

    protected override void Setup()
    {
        _idleState = GetComponent<IdleState>();
        animator = GetComponentInParent<Animator>();
        m_agent = GetComponentInParent<NavMeshAgent>();
        //m_agent = GameObject.FindGameObjectWithTag("NaviMesh").GetComponent<NavMeshAgent>();
        SetDelegate(Event.Enter, EnterCallback);
        SetDelegate(Event.Leave, ExitCallback);
    }

    int EnterCallback()
    {
        StartCoroutine("routine");
        Debug.Log("EnterMove");
        return 0;
    }

    int ExitCallback()
    {
        //Debug.Log("ExitRoutine");
        StopCoroutine("routine");
        animator.SetFloat("Speed", 0);
        if (m_agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            m_agent.SetDestination(this.transform.position);
        }
        return 0;
    }
    IEnumerator routine()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            //Debug.Log(EnemyManager.Instance.m_froznBody);
            //if (!EnemyBossManager.Instance.m_froznBody.activeSelf)
            {
                NavMeshHit hit;
                //if (NavMesh.SamplePosition(m_cachedTargetPosition, out hit, 1.0f, NavMesh.AllAreas))
                //{
                //    // 位置をNavMesh内に補正
                //    m_cachedTargetPosition = hit.position;
                //}
                //else
                //{
                //    m_cachedTargetPosition = GameManager.Player.transform.position;
                //}
                m_cachedTargetPosition = GameManager.Player.transform.position;
                //m_cachedTargetPosition = new Vector3(-100,0,3);
                float distance = Vector3.Distance(m_cachedTargetPosition, transform.position);
                if (m_agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    m_agent.SetDestination(m_cachedTargetPosition); // Navmesh Agent に目的地をセットする（Vector3 で座標を設定していることに注意。Transform でも GameObject でもなく、Vector3 で目的地を指定する)
                }
                
                enemy.gameObject.transform.LookAt(GameManager.Player.transform);
                animator.SetFloat("Speed", m_agent.velocity.magnitude);
                if (distance <= m_agent.stoppingDistance)
                {
                    animator.Play("Roar");
                    yield return new WaitForSeconds(1f);
                    //GameManager.Instance.m_player.gameObject.GetComponent<CameraController>().ResetCamera();
                    yield return new WaitForSeconds(1f);
                    //GameManager.Instance.m_player.gameObject.GetComponent<CameraController>().ResetCamera();
                    _actionCtrl.SetCurrent(_attackState);

                }
                else
                {
                    if (distance > 8)
                    {
                        if (m_agent.pathStatus != NavMeshPathStatus.PathInvalid)
                        {
                            m_agent.SetDestination(m_cachedTargetPosition); // Navmesh Agent に目的地をセットする（Vector3 で座標を設定していることに注意。Transform でも GameObject でもなく、Vector3 で目的地を指定する)
                        }
                        m_agent.speed = m_runSpeed;
                        animator.SetFloat("Speed", m_runSpeed);
                    }
                    else
                    {
                        if (m_agent.pathStatus != NavMeshPathStatus.PathInvalid)
                        {
                            m_agent.SetDestination(m_cachedTargetPosition); // Navmesh Agent に目的地をセットする（Vector3 で座標を設定していることに注意。Transform でも GameObject でもなく、Vector3 で目的地を指定する)
                        }
                        m_agent.speed = m_runSpeed * 0.5f;
                        animator.SetFloat("Speed", m_runSpeed * 0.5f);
                    }
                }
            }
            yield return null;
        }
         
    }

    public void SpawnEffect()
    {
        StartCoroutine("SpawnWait");
        var source = GetComponentInParent<Cinemachine.CinemachineImpulseSource>();
        source.GenerateImpulse(5);
        SoundManager.Instance.PlayRoar();
    }

    IEnumerator SpawnWait()
    {
        Instantiate(m_roarEffect, m_spawnEffect.transform.position, m_spawnEffect.transform.rotation);
        yield return new WaitForSeconds(2f);
    }
    private void OnTriggerExit(Collider other)
    {
        if (_opponentTag == "") return;
        if (other.gameObject == null) return;
        if (other.gameObject.CompareTag(_opponentTag))
        {
            Debug.Log("EixtArea");
            _actionCtrl.SetCurrent(_idleState);
            _actionCtrl.SetCurrentName("IdleState");
        }
    }
}
