using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatternGenerator : MonoBehaviour {

    public float radius = 10;
    public GameObject sourceMeshGO;
    public int instanceCount = 30;
    public int subMeshIndex = 0;

    Mesh sourceMesh;
    Transform center;
    MaterialPropertyBlock matProp;

    const int HALF_TYPE_BYTES = 2;
    const int FLOAT_TYPE_BYTES = 4;
    int shaderColorPropID;

    Material srcMeshMaterial;
    Matrix4x4[] transformMats;

    int cachedInstanceCount = -1;

    private void Start()
    {
        if(sourceMeshGO == null)
        {
            sourceMeshGO = gameObject;
        }

        var meshFilter = sourceMeshGO.GetComponent<MeshFilter>();
        sourceMesh = meshFilter.mesh;
        center = sourceMeshGO.transform;
        
        var meshRenderer = sourceMeshGO.GetComponent<MeshRenderer>();
        if (meshRenderer)
        {
            srcMeshMaterial = meshRenderer.material;
            meshRenderer.enabled = false;
        }
        else
            enabled = false;

        matProp = new MaterialPropertyBlock();
        shaderColorPropID = Shader.PropertyToID("_Color");

        if (sourceMesh == null)
            enabled = false;
        
    }
    
    private void Update()
    {
        if (instanceCount <= 0)
            return;

        float angleStep = 360f / instanceCount * Mathf.Deg2Rad;
        int i = 0;

        Vector3 originalPos = center.position;
        Quaternion originalRot = center.rotation;

        Vector3 xBasis = center.right;
        Vector3 yBasis = center.up;
        Vector3 planeNormal = -center.forward;
        float randVal = Random.value;
        while(i < instanceCount)
        {
            Vector3 translateVec = radius * ( Mathf.Cos(angleStep * i) * xBasis + Mathf.Sin(angleStep * i) * yBasis );
            Vector3 instancePos = center.position + translateVec;
            
            Vector3 tangent = Vector3.Cross(planeNormal, translateVec);
            Vector3 diagonal = center.right + center.up + center.forward;
            Vector3 rotAxis = Vector3.Cross(diagonal.normalized, tangent.normalized);
            float angleInRad = Mathf.Asin(rotAxis.magnitude);
            Vector3 pivot = center.position - Vector3.Scale(diagonal, center.lossyScale) * 0.5f;
            center.RotateAround(pivot, rotAxis, angleInRad * Mathf.Rad2Deg);
            
            float cosVal = (Mathf.Cos(angleStep * i + Time.realtimeSinceStartup) + 1)/2f;
            float sinVal = (Mathf.Sin(angleStep * i + Time.realtimeSinceStartup) + 1)/2f;
            float complexVal = (1 - Mathf.Cos((1 - sinVal) * angleStep * i + Time.realtimeSinceStartup))/2f;
            matProp.SetColor(shaderColorPropID, new Color(cosVal, sinVal, cosVal, 1));
            Graphics.DrawMesh(sourceMesh, instancePos, center.rotation, srcMeshMaterial, 0, Camera.main, subMeshIndex, matProp);
            
            center.SetPositionAndRotation(originalPos, originalRot);
            i++;
        }

    }

    private void OnDisable()
    {

    }

}
