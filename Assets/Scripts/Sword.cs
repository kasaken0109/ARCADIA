using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sword : MonoBehaviour,IWeapon
{
    [SerializeField]
    private float[] m_activeColliderTime;
    [SerializeField]
    private GameObject[] m_activeCollider;
    [SerializeField]
    private AttackcolliderController controller;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameManager.Player.GetComponent<Rigidbody>();
    }

    public void NormalAttack()
    {
        StartCoroutine(ColliderGenerater.Instance.GenerateCollider(m_activeCollider[0], 0.5f));
    }

    public void BasicAttack()
    {
        ColliderGenerater.Instance.StartActiveCollider(m_activeCollider[0], 0.5f);
    }

    public void SpecialAttack()
    {
        ColliderGenerater.Instance.StartActiveCollider(m_activeCollider[1], 1f);
        rb.DOMoveY(0,0.5f);
    }

    public void StopEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = false;
    }

    public void StartEmitting()
    {
        GetComponentInChildren<TrailRenderer>().emitting = true;
        
        int dmg = (int)Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("Floor").transform.position.y) * 8;
        int correctDmg = dmg >= 100 ? 100 : dmg;
        controller.AddDamageCount(correctDmg);
    }
    public void FloatUp()
    {
        rb.DOMoveY(GameManager.Player.transform.position.y + 2, 0.3f);
    }
}
