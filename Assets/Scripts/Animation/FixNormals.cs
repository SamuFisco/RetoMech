using UnityEngine;

public class FixNormals : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            meshFilter.mesh.RecalculateNormals();
            Debug.Log("Normales recalculadas en tiempo de ejecución.");
        }
    }
}
