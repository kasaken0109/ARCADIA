using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldObject : MonoBehaviour,IDamage
{
    [SerializeField]
    [Tooltip("オブジェクトのHP")]
    int m_hp = 5;
    [SerializeField]
    [Tooltip("破壊時に生成されるオブジェクト")]
    GameObject m_instance = default;
    public void AddDamage(int damage)
    {
        m_hp = m_hp > damage ? m_hp - damage : 0;
        if (m_hp == 0) 
        {
            Instantiate(m_instance, transform);
            Destroy(this.gameObject,1);
        }
    }
}
