using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TimelineController : MonoBehaviour
{
    List<Text> _textList;
    List<Image> _imageList;
    // Start is called before the first frame update
    void Start()
    {
        _textList = FindObjectsOfType<Text>().Where(x => x.gameObject.activeInHierarchy == true).ToList();
        _imageList = FindObjectsOfType<Image>().Where(x => x.gameObject.activeInHierarchy == true).ToList();
        SetActiveUI(false);
    }

    public void SetActiveUI(bool isActive)
    {
        _textList.ForEach(x => x.enabled = isActive);
        _imageList.ForEach(x => x.enabled = isActive);
    }

    private void OnDisable()
    {
        SetActiveUI(true);
    }
}
