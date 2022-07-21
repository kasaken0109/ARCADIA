using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackSet
{
    [Tooltip("次のステート名")]
    public string _NextStateName = "";

    [SerializeReference,SubclassSelector]
    [Tooltip("判定クラス")]
    public ICondition _condition = default;
}
