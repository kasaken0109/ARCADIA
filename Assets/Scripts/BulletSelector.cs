using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSelector : MonoBehaviour
{
    [SerializeField]
    private BulletList _list = default;
    [SerializeField]
    private ButtonSelector[] _buttons = default;

    // Start is called before the first frame update
    void Awake()
    {
        BulletInformationInit();
    }

    public void BulletInformationInit()
    {
        for (int i = 0; i < _list.Bullets.Count; i++)
        {
            _buttons[i].SetInformation(_list.Bullets[i]);
        }
    }
}
