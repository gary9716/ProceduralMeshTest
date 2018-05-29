using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralBase : MonoBehaviour {

    MeshRenderer _renderer;
    MeshFilter meshFilter;
    protected Mesh mesh;

    private void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        if (_renderer == null)
            _renderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        mesh = meshFilter.mesh;
        CreateMesh();
    }
	
	protected virtual void CreateMesh() {

	}
}
