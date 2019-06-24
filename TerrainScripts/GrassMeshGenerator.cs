using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassMeshGenerator : MonoBehaviour {

    public MeshFilter meshF;
    public int length;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
	
	void Start () {

        meshF = GetComponent<MeshFilter>();
        length = Random.Range(4, 10);
        vertices = new Vector3[length * 2 - 1];
        triangles = new int[(vertices.Length - 2)*6];

        mesh = new Mesh();

        for(int i=0; i < length; i++)
        {
            float s = Mathf.Cos(((float)i / length) * Mathf.PI * 0.5f);
            if (i != length - 1)
            {
                

                vertices[i * 2] = new Vector3(-0.25f*s, i, 0.5f*s);
                vertices[i * 2 + 1] = new Vector3(0.25f*s,i, 0.5f*s);
                
                
            }else
            {
                vertices[i * 2] = new Vector3(0,i,0.5f*s);
            }
       
            if (i < length - 2)
            {
                //front faces

                triangles[i * 6] = i * 2 + 1;
                triangles[i * 6 + 1] = i * 2 ;
                triangles[i * 6 + 2] = (i + 1) * 2;

                triangles[i * 6 + 3] = i * 2 + 1;
                triangles[i * 6 + 4] = (i + 1) * 2;
                triangles[i * 6 + 5] = (i + 1) * 2 + 1;

                //back faces

                triangles[i * 6+triangles.Length/2] = i * 2 ;
                triangles[i * 6 + 1+triangles.Length/2] = i * 2+1;
                triangles[i * 6 + 2+triangles.Length/2] = (i + 1) * 2;

                triangles[i * 6 + 3+triangles.Length/2] = i * 2 +1;
                triangles[i * 6 + 4+triangles.Length/2] = (i + 1) * 2 +1;
                triangles[i * 6 + 5 + triangles.Length/2] = (i + 1) * 2 ;


            }
            else if (i < length - 1)
            {
                //front face
                triangles[i * 6] = i * 2 +1;
                triangles[i * 6 + 1] = i * 2;
                triangles[i * 6 + 2] = (i + 1) * 2;

                //back face
                triangles[i * 6+triangles.Length/2] = i * 2 ;
                triangles[i * 6 + 1+triangles.Length/2] = i * 2+1;
                triangles[i * 6 + 2+triangles.Length/2] = (i + 1) * 2;
            }

            

        }
        

        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        meshF.mesh = mesh;

        //transform.position = new Vector3(transform.position.x, 1000, transform.position.z);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;

        }

    }
    
}
