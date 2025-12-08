using UnityEngine;

public class TriangleScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector2[] uv;
    private int[] triangles;
    void Start()
    {
        mesh=new Mesh();
        vertices=new Vector3[4];
        uv=new Vector2[4];
        triangles=new int[6];
        vertices[0]=new Vector3(0,0,0);
        vertices[1]=new Vector3(0,1,0);
        vertices[2]=new Vector3(1,1,0);
        vertices[3]=new Vector3(1,0,0);
        uv[0]=new Vector2(0,0);
        uv[1]=new Vector2(0,1);
        uv[2]=new Vector2(1,1);
        uv[3]=new Vector2(1,0);
        triangles[0]=0;
        triangles[1]=1;
        triangles[2]=2;
        triangles[3]=2;
        triangles[4]=3;
        triangles[5]=0;
        mesh.vertices=vertices;
        mesh.uv=uv;
        mesh.triangles=triangles;
        gameObject.GetComponent<MeshFilter>().mesh=mesh;
    }
    void Update()
    {
        
    }
}
