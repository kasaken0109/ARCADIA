using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTextManager : MonoBehaviour
{
    [SerializeField]
    public Text m_text;
    FireLine fireLine;
    fire fire;
    [SerializeField]
    bool fireSelect = true;


    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireSelect)
        {
            FireLineText();
        }
        else
        {
            FireText();
        }
    }

    void FireLineText()
    {
        m_text.text = fireLine.m_bulletNum.ToString(); 
    }

    void FireText()
    {
        m_text.text = fireLine.m_bulletNum.ToString();
    }
}
