using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform),typeof(Image))]
public class UISelecter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("拡大率")]
    private float m_zoomRate = 1.1f;

    [SerializeField]
    [Tooltip("ハイライト色")]
    private Color m_highlight = default;

    [SerializeField]
    [Tooltip("通常色")]
    private Color m_normal = default;

    RectTransform rectTransform;
    Image image;
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    /// <summary>
    /// EventTrigger登録用の選択時呼ばれる昨日
    /// </summary>
    /// <param name="isSelected">選択されているかどうか</param>
    public void SelectedUI(bool isSelected)
    {
        rectTransform.localScale = isSelected ? new Vector3(m_zoomRate, m_zoomRate, 1) : Vector3.one;//選択されたらサイズを拡大
        image.color = isSelected ? m_highlight : m_normal;//色を強調表示色にする
    }
}
