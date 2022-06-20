using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create StateData")]
class State : ScriptableObject
{
    [SerializeField] Motion m_motion;
    [SerializeField] State[] nextState;
}
