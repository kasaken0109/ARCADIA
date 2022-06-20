using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerBase
{
    [SerializeField]
    [Tooltip("表示するUIオブジェクト")]
    private GameObject m_uiObj;

    /// <summary>
    /// UIオブジェクトを有効にする
    /// </summary>
    public virtual void EnableUI()
    {
        m_uiObj.SetActive(true);
    }

    /// <summary>
    /// UIオブジェクトを無効にする
    /// </summary>
    public virtual void DisableUI()
    {
        m_uiObj.SetActive(false);
    }
}
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
