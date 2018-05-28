using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatternGenerator : MonoBehaviour {

    public GameObject sourceMeshGO;
    Mesh sourceMesh;
    Transform center;

    Vector3 originalPos;
    Quaternion originalRot;

    private void Start()
    {
        if(sourceMeshGO == null)
        {
            sourceMeshGO = gameObject;
        }

        var meshFilter = sourceMeshGO.GetComponent<MeshFilter>();
        sourceMesh = meshFilter.mesh;
        center = sourceMeshGO.transform;

        originalPos = center.position;
        originalRot = center.rotation;

        if (sourceMesh == null)
            enabled = false;
        
    }

    private void Update()
    {
        //TODO: GPU instancing 
    }

}
