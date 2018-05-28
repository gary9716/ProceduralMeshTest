using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralQuad : ProceduralBase {

    protected override void CreateMesh() {
            mesh.Clear();

            mesh.vertices = new Vector3[] { new Vector3(0,0,0), new Vector3(0,1,0), new Vector3(1,0,0), new Vector3(1,1,0) };
            mesh.triangles = new int[] { 0, 1, 2, 1, 3, 2 }; //specify them clockwisely and the normal vector will be decided with Left-Hand rule.
            mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1) };

    }

}
