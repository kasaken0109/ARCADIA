#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

public class SaveMeshAssets : MonoBehaviour
{
    [MenuItem("APIExamples/SaveAssets")]
    // Start is called before the first frame update
    public static void SaveMesh(Mesh mesh)
    {
        AssetDatabase.CreateAsset(mesh, "Assets/Mesh/" + mesh.name + ".asset");
        AssetDatabase.SaveAssets();
    }
}
#endif