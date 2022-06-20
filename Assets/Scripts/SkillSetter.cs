using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetter : MonoBehaviour
{
    public void DataSetter(int value)
    {
        EquipmentManager.Instance.SetEquipSkillID(value);
    }
}
