using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private string weaponTempleteName = "RigPistolRight";
    [SerializeField]
    private GameObject m_weaponTemplete;
    [SerializeField]
    private GameObject m_equipEffect = default;
    [SerializeField]
    private GameObject m_weaponSwordTemplete = default;
    [SerializeField]
    private GameObject m_weapon = default;

    public GameObject NowWeapon => m_weapon;

    // Start is called before the first frame update
    void Start()
    {
        EquipWeapon("PlayerRifle");
    }

    public void EquipWeapon(string name)
    {
        GameObject find = GameObject.Find($"{name}(Clone)");
        if (find != null) return;
        if (m_weapon != null)
        {
            Destroy(m_weapon);
            m_weapon = null;
            Resources.UnloadUnusedAssets();
        }
        if (name.Equals("Sword"))
        {
            m_weapon = Instantiate(Resources.Load(name), m_weaponSwordTemplete.transform.position,m_weaponSwordTemplete.transform.rotation ) as GameObject;
            m_weapon.transform.parent = m_weaponSwordTemplete.transform;
        }
        else
        {
            m_weapon = Instantiate(Resources.Load(name), m_weaponTemplete.transform.position, m_weaponTemplete.transform.rotation) as GameObject;
            m_weapon.transform.parent = m_weaponTemplete.transform;
        }
        Instantiate(m_equipEffect, m_weapon.transform);
    }
}
