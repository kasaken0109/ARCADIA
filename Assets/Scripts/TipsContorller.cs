using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsContorller : MonoBehaviour
{
    [SerializeField]
    GameObject m_tipsUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) m_tipsUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) m_tipsUI.SetActive(false);
    }
}
