using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMeshCollider : MonoBehaviour
{
    [SerializeField]
    float m_interval;
    [SerializeField]
    Mesh mesh;
    private MeshCollider meshCollider;
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    Mesh bakedMesh;
    private float nextTime;

    // Start is called before the first frame update
    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        bakedMesh = mesh;
        nextTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nextTime -= Time.deltaTime;
        if (nextTime <= 0)
        {
            nextTime = m_interval;
            SkinnedMeshRenderer.BakeMesh(bakedMesh);
            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = bakedMesh;
        }
    }
}
