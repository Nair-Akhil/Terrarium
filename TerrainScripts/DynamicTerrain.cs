using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class DynamicTerrain : MonoBehaviour {

    public int dim;
    public float quadDim;
    
    public MeshFilter meshF;
    public MeshCollider collider;
    private Mesh mesh;
    
    public float dimensions;

	void Start () {

        int randomSeed = Random.Range(100,-100);


        dimensions = dim*quadDim;
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[dim*dim];
        int[] tris = new int[((dim-1)*(dim-1))*6];
        for (int x=0; x < dim; x++)
        {

            for(int y=0; y < dim; y++)
            {

                int index = x + dim * y;
                int nIndex = x + dim * (y + 1);
                float cX = x * quadDim;
                float cY = y * quadDim;

                float cZ = (Mathf.PerlinNoise(cX * 0.1f + 1000+randomSeed, cY * 0.1f + 1000+randomSeed) * 40f);
                cZ += (Mathf.PerlinNoise(cX * 0.5f + 4000+randomSeed, cY * 0.5f + 4000+randomSeed) * 3f);
                cZ += (Mathf.PerlinNoise(cX*2 + 4000+randomSeed, cY*2 + 4000+randomSeed));
                cZ -= Mathf.Pow(cX-dimensions*0.5f,2)*0.05f+Mathf.Pow(cY-dimensions*0.5f,2)*0.05f;

                vertices[index] = new Vector3(cX-(dim*quadDim*0.5f), cZ, cY - (dim * quadDim * 0.5f));

                if (!((x == dim - 1) || (y == dim - 1)))
                {

                    int tIndex = x + (dim - 1) * y;

                    tris[tIndex * 6] = index + 1;
                    tris[tIndex * 6 + 1] = index ;
                    tris[tIndex * 6 + 2] = nIndex;

                    tris[tIndex * 6 + 3] = index + 1;
                    tris[tIndex * 6 + 4] = nIndex;
                    tris[tIndex * 6 + 5] = nIndex + 1;

                }


            }

        }
        
        mesh.vertices = vertices;
        mesh.triangles = tris;

        mesh.RecalculateNormals();
        
        meshF.mesh = mesh;
        collider.sharedMesh = mesh;

    }
	
}
