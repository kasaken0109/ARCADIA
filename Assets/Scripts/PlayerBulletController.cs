using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    // Start is called before the first frame update
    
    /// <summary>弾の飛ぶ速度</summary>
    [SerializeField] float m_bulletSpeed = 10f;
    Rigidbody m_rb;
    //[SerializeField] float m_bulletPower = 12f;
    //[SerializeField] GameObject m_bounceEffect;
    //[SerializeField]GameObject m_bulletEffect;
    //bool bounceflag = true;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        this.m_rb.velocity = Camera.main.transform.forward * m_bulletSpeed;
    }
}
