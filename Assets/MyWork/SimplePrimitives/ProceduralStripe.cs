using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStripe : ProceduralBase {

    public int stripeLength = 4;
    public float lengthUnit = 1;
    public Vector3 startPt = Vector3.zero;
    public Vector3 extendDir = Vector3.right;
    public Vector3 normalDir = -1 * Vector3.forward;

    protected override void CreateMesh()
    {
        mesh.Clear();

        List<Vector3> vertexBuffer = new List<Vector3>();
        List<int> triangleBuffer = new List<int>();
        List<Vector2> uvBuffer = new List<Vector2>();

        int numVerticesInOneCol = 2;
        
        for (int col = 0; col <= stripeLength; col++)
        {
            var vertexMidPt = startPt + col * lengthUnit * extendDir.normalized;
            var expandDir = Vector3.Cross(extendDir, normalDir).normalized;
            vertexBuffer.Add(vertexMidPt + expandDir * -0.5f);
            vertexBuffer.Add(vertexMidPt + expandDir * 0.5f);

            if(col % 2 == 0) //even
            {
                uvBuffer.Add(Vector2.right);
                uvBuffer.Add(Vector2.zero);
            }
            else //odd
            {
                uvBuffer.Add(Vector2.up);
                uvBuffer.Add(Vector2.one);
            }

            var index0 = col * numVerticesInOneCol;
            if(index0 > 0)
            {
                triangleBuffer.Add(index0); //last triangle

                //current triangle
                triangleBuffer.Add(index0); 
                triangleBuffer.Add(index0 - 1);
                triangleBuffer.Add(index0 + 1);
            }
            
            if(col < stripeLength)
            {
                //next triangle
                triangleBuffer.Add(index0);
                triangleBuffer.Add(index0 + 1);
            }
            
        }

        mesh.vertices = vertexBuffer.ToArray();
        mesh.triangles = triangleBuffer.ToArray();
        mesh.uv = uvBuffer.ToArray();
    }

}
