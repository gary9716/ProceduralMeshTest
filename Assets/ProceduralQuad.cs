using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralQuad : MonoBehaviour {

    MeshRenderer renderer;
    MeshFilter meshFilter;
    Mesh mesh;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
            renderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        mesh = meshFilter.mesh;
        CreateMesh();
    }

    void CreateMesh()
    {
        mesh.Clear();

        mesh.vertices = new Vector3[] { new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,0,0), new Vector3(1,1,0) };
        mesh.triangles = new int[] { 0, 1, 2, 1, 3, 2 };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

    }


}
