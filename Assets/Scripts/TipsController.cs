using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TipsController : MonoBehaviour
{
    [SerializeField] GameObject m_panel = null;
    [SerializeField] GameObject[] m_tipsPanel;
    [SerializeField] VideoController controller;
    [SerializeField] VideoClip[] clip;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Display(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_panel?.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_panel?.SetActive(false);
        }
    }

    public void Display(int index)
    {
        if (index >= m_tipsPanel.Length) return;
        for (int i = 0; i < m_tipsPanel.Length; i++)
        {
            m_tipsPanel[i].SetActive(i == index);
        }
        controller.DisplayVideo(clip[index]);
    }
}
