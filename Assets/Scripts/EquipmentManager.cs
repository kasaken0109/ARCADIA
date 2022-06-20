using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private static EquipmentManager instance = null;

    public static EquipmentManager Instance
    {
        get
        {
            var target = FindObjectOfType<EquipmentManager>();
            if (target)
            {
                instance = target;
            }
            else
            {
               var gm = GameObject.Find("GM");
                if (!gm) gm = new GameObject("GM");
                instance = gm.AddComponent<EquipmentManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    [HideInInspector]
    public Bullet[] Equipments = new Bullet[3];

    private int _equipID = 1;

    public int GetEquipID => _equipID;

    private int _equipSkillID = 1;

    public void SetEquipments(Bullet bullet)
    {
        Equipments[_equipID - 1] = bullet;
    }

    public void SetSkill(PassiveSkill skill)
    {
        var equip = Equipments[_equipID - 1];
        if(_equipSkillID == 1) equip.passiveSkill_1 = skill;
        else
        {
            equip.passiveSkill_2 = skill;
        }
    }

    public void SetEquipID(int value) => _equipID = value;

    public void SetEquipSkillID(int value) => _equipSkillID = value;

}
