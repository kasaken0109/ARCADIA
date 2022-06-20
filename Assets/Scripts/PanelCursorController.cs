using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCursorController : MonoBehaviour
{
    [SerializeField] GameObject[] m_tipsTextObj;
    // Start is called before the first frame update
    void Start()
    {
        SetCursor(0);
    }

    public void SetCursor(int index)
    {
        if (index >= m_tipsTextObj.Length) index = 0;
        for (int i = 0; i < m_tipsTextObj.Length; i++)
        {
            if (i == index) m_tipsTextObj[i].SetActive(true);
            else
            {
                m_tipsTextObj[i].SetActive(false);
            }
        }
    }
}
