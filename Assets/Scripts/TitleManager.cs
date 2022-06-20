using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TitleManager : MonoBehaviour
{
    [SerializeField] PlayableDirector m_playable;
    [SerializeField] GameObject[] m_canvasObj = null;
    [SerializeField] GameObject m_skip = null;
    [SerializeField] GameObject m_anim = null;
    // Start is called before the first frame update
    void Start()
    {
        m_playable.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDone())
        {
            SetShowUI();
            m_playable.Stop();
            m_skip?.SetActive(false);
        }
        else
        {
            if(Input.GetButtonDown("Fire1")) m_skip?.SetActive(true);
        }
    }

    public bool IsDone()
    {
        return m_playable.time >= m_playable.duration - 3;
    }

    public void ActiveButton()
    {
        m_playable.Stop();
        SetShowUI();
    }

    void SetShowUI()
    {
        if (m_canvasObj == null) return;
        foreach (var item in m_canvasObj)
        {
            item.SetActive(true);
        }
        m_anim?.SetActive(false);
        m_playable.time = 0f;
    }
}
