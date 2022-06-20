using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNormalController : MonoBehaviour,IDamage
{
    [SerializeField] int m_hp = 150;
    [SerializeField] GameObject m_deadBody = null;
    [SerializeField] GameObject m_attackCol = null;
    [SerializeField] GameObject m_gate = null;
    [SerializeField] float m_skillWaitTime = 0.5f;
    [SerializeField] int m_mp = 200;
    [SerializeField] float m_freezeTime = 5f;
    public GameObject m_froznBody = null;
    int mp;

    NavMeshAgent navMeshAgent;
    Animator m_anim;
    public void AddDamage(int damage)
    {
        if (IsNoDamege) return;
        if(damage <= m_hp)
        {
            m_hp -= damage;
            mp -= (30 - damage);
        }
        else
        {
            Instantiate(m_deadBody, transform.position, transform.rotation);
            m_gate.SetActive(true);
            GameManager.Instance.SetGameState(GameManager.GameState.PLAYERWIN);
            Destroy(this.gameObject);
        }

    }

    bool IsNoDamege = true;
    public void SetNoDamege(bool value)
    {
        IsNoDamege = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        mp = m_mp;
        m_anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(nameof(FrostMode));

    }
    IEnumerator FrostMode()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (mp <= 0)
            {
                navMeshAgent.SetDestination(gameObject.transform.position);
                m_froznBody.SetActive(true);
                m_anim.SetBool("IsFreeze", true);
                yield return new WaitForSeconds(m_freezeTime);
                m_anim.SetBool("IsFreeze", false);
                m_froznBody.SetActive(false);
                navMeshAgent.SetDestination(GameManager.Player.transform.position);
                mp = m_mp;
            }
        }
    }

    // Update is called once per frame
    public void ActiveCol()
    {
        ColliderGenerater.Instance.StartActiveCollider(m_attackCol, m_skillWaitTime);
    }
}
