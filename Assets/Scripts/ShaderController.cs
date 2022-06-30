using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderController : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject = default;
    float _time = 0;
    bool IsStopped = false;
    Image render;

    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
        render = GetComponent<Image>();
        render.material = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsStopped) _time += Time.deltaTime;
    }

    public void SetMaterialProparty(Material material)
    {
        IsStopped = false;
        if (render)
        {
            render.material = material;
            render.material.SetFloat("_TimeScale", _time);
        }
        else
        {
            Debug.LogError("mee");
        }
    }
}
