using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostWallController : MonoBehaviour,IDamage
{
    /// <summary> 破壊するときに発生する攻撃エフェクト/// </summary>
    [SerializeField] GameObject m_effect;
    /// <summary> 破壊するときに発生する攻撃コライダー/// </summary>
    [SerializeField] GameObject m_attackCollider = null;
    /// <summary> 破壊されそうな時のマテリアル/// </summary>
    [SerializeField] Material m_break;
    /// <summary> 壁のHP/// </summary>
    [SerializeField] int m_hp = 2;
    /// <summary> 破壊するFrostWall/// </summary>
    [SerializeField] GameObject m_frostwall = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttackCollider")
        {
            AddDamage(m_hp);
        }
        else if(other.tag == "EnemyFire")
        {
            AddDamage(1);
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(m_effect, this.transform.position, this.transform.rotation);
    }

    IEnumerator SetNonActive()
    {
        m_attackCollider.SetActive(true);
        yield return new WaitForSeconds(1);
        m_attackCollider.SetActive(false);
    }

    public void AddDamage(int damage)
    {
        if(m_hp <= damage)
        {
            m_frostwall.SetActive(false);
            
            StartCoroutine(SetNonActive());
            Destroy(this.gameObject, 1);
        }
        else
        {
            m_hp -= damage;
            if (m_hp <= 5) GetComponentInChildren<Renderer>().material = m_break;
        }
        Debug.Log(m_hp);

    }
}

