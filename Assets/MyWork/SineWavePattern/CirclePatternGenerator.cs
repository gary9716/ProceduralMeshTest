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
    
    private void UpdateBuffers()
    {
        cachedInstanceCount = instanceCount;

        transformMats = new Matrix4x4[instanceCount];
        for(int i = 0;i < instanceCount;i++)
        {
            transformMats[i] = new Matrix4x4();
        }
    }

    private void Update()
    {
        if (instanceCount <= 0)
            return;

        float angleStep = 360f / instanceCount * Mathf.Deg2Rad;
        int i = 0;

        if (instanceCount != cachedInstanceCount)
            UpdateBuffers();

        Vector3 originalPos = center.position;
        Quaternion originalRot = center.rotation;

        Vector3 xBasis = center.right;
        Vector3 yBasis = center.up;
        Vector3 planeNormal = -center.forward;
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
            
            //Set the position/rotation/scale for this matrix
            transformMats[i].SetTRS(instancePos, center.rotation, Vector3.one);
            
            center.SetPositionAndRotation(originalPos, originalRot);
            i++;
        }

        Graphics.DrawMeshInstanced(sourceMesh, 0, srcMeshMaterial, transformMats);
    }

    private void OnDisable()
    {

    }

}
