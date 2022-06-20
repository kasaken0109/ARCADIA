using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// StateMachine の状態を管理する(現在は不必要)
/// </summary>
public class ActionCtrl
{
    StateBase _currentState = null;
    string currentName;

    /// <summary>
    /// 現在の状態をセットする
    /// </summary>
    /// <param name="s">現在の状態</param>
    public void SetCurrent(StateBase s)
    {
        if (s == null) return;
        if (_currentState != null)
        {
            _currentState.EventCall(StateBase.Event.Leave);
        }

        _currentState = s;
        currentName = s.name;
        _currentState.SetCtrl(this);
        _currentState.EventCall(StateBase.Event.Enter);
    }

    public void SetCurrentName(string s)
    {
        currentName = s;
    }


    public string GetCurrentStateName()
    {
        return currentName;
    }
}