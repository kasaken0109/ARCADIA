using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillList")]
public class SkillList : ScriptableObject
{
    [SerializeField]
    private List<PassiveSkill> _skills = default;

    public List<PassiveSkill> Skills => _skills;
}

