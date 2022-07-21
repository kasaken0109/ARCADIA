using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSet
{
    [Tooltip("���̃X�e�[�g��")]
    public string _NextStateName = "";

    [SerializeReference,SubclassSelector]
    [Tooltip("����N���X")]
    public ICondition _condition = default;
}
