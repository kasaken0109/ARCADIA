using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AttackSet 
{
    public AttackMotion attackMotion;
    //public I
}

[System.Serializable]
public class AttackBehavior :IInputBehavior
{
    [SerializeField]
    Animator _animator = default;

    [SerializeField]
    AttackMotion _attackMotion = default;
    public bool IsEnd => throw new System.NotImplementedException();

    private bool _isEnd = false;

    private float time = 0;

    public void Execute()
    {
        _animator.Play(_attackMotion.AttackClipName);
        _isEnd = false;
        while (_attackMotion.ClipDuraration >= time)
        {
            time += Time.deltaTime;
        }
        _isEnd = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
