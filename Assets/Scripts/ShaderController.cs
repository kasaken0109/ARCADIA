using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderController : MonoBehaviour
{
    [SerializeField]
    GameObject _gameObject = default;
    [SerializeField]
    Material _set = default;
    float _time = 0;
    bool IsStopped = false;
    Image render; 
    Material set;
    float timeUntilSceneStart;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        render = GetComponent<Image>();
        render.material = null;
        timeUntilSceneStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStopped) _time += Time.deltaTime;
    }

    public void SetMaterialProparty()
    {
        IsStopped = false;
        if (render)
        {
            render.material = _set;
            render.material.SetFloat("_TimeScale", timeUntilSceneStart + _time);
        }
        else
        {
            Debug.LogError("mee");
        }
    }
}
