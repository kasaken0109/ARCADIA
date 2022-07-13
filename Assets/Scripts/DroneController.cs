using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("追跡対象のオブジェクト")]
    GameObject m_chaseTarget = default;
    [SerializeField]
    [Tooltip("対象との距離")]
    Vector3 m_chaseOffset = default;
    [SerializeField]
    [Tooltip("追跡を開始する距離")]
    float m_startChaseDistance = 0.5f;
    [SerializeField]
    [Tooltip("追跡時間")]
    float m_chaseTime = 2f;
    [SerializeField]
    [Tooltip("回転時間")]
    float m_chaseLookTime = 0.5f;
    [SerializeField]
    [Tooltip("浮遊の高さ")]
    float m_floatHeight = 0.7f;

    float distance;
    GameObject attackTarget;

    void Start()
    {
        attackTarget = GameObject.FindGameObjectWithTag("Enemy");
    }
    void Update()
    {
        LookPlayer();
        transform.position = new Vector3(transform.position.x, m_chaseTarget.transform.position.y + m_floatHeight, transform.position.z);
        distance = Vector3.Distance(transform.position,m_chaseTarget.transform.position);
        if (distance >= m_startChaseDistance) Chase();
    }

    /// <summary>
    /// プレイヤーを追いかける
    /// </summary>
    void Chase()
    {
        Vector3 destination = m_chaseTarget.transform.position + m_chaseOffset;
        transform.position = Vector3.Slerp(transform.position,destination,m_chaseTime * Time.deltaTime);
    }

    /// <summary>
    /// 敵の方向を見る
    /// </summary>
    void LookEnemy()
    {
        if (!attackTarget)
        {
            return;
        }
        Vector3 diff = attackTarget.transform.position - transform.position;
        Quaternion lookAngle = Quaternion.LookRotation(diff);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAngle, m_chaseLookTime * Time.deltaTime);
    }

    /// <summary>
    /// 敵の方向を見る
    /// </summary>
    void LookPlayer()
    {
        var player = GameObject.Find("Player");
        if (!player) return;
        Quaternion lookAngle = Quaternion.LookRotation(player.transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAngle, m_chaseLookTime * Time.deltaTime);
    }




}
