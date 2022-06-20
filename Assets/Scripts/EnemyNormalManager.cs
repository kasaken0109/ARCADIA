using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNormalManager : MonoBehaviour
{
    NavMeshAgent navMeshAgent = null;
    Animator m_animator = null;
    GameObject m_attack = null;
    [SerializeField]GameObject m_attackCol = null;
    [SerializeField] GameObject m_camera = null;

    float m_timer = 0f;
    [SerializeField] float m_skillWaitTime = 0.5f;
    [SerializeField] EnemyNormalController controller;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
        m_animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsFind) return;
        if (controller.m_froznBody.activeSelf) return;
        m_timer += Time.deltaTime;
        m_animator.SetFloat("Speed",navMeshAgent.velocity.magnitude);
        //Debug.Log($"ss:{navMeshAgent.velocity.magnitude}");
        if(navMeshAgent.velocity.magnitude <= 0.1f)
        {
            m_animator.SetTrigger("Attack");
        }
        if (m_attack && m_timer >= 2f)
        {
            Debug.Log("a");
            navMeshAgent.SetDestination(m_attack.transform.position);
            transform.LookAt(m_attack.transform);
            m_timer = 0f;
        }
        
    }
    bool IsFirst = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enter");
            m_attack = other.gameObject;
            //m_camera?.SetActive(true);
            if (IsFirst)
            {
                StartCoroutine(nameof(ZoomEnemy));
                IsFirst = false;
            }
        }
    }

    bool IsFind = false;
    IEnumerator ZoomEnemy()
    {
        if (IsFind) yield break;
        m_camera?.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_animator.Play("shout");
        yield return new WaitForSeconds(4f);
        m_camera?.SetActive(false);
        //m_attack.GetComponent<CameraController>().ResetCamera();
        controller.SetNoDamege(false);
        IsFind = true;
        navMeshAgent.SetDestination(m_attack.transform.position);
        
    }

    
}
