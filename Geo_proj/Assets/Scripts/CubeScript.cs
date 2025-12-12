using UnityEngine;

public class CubeScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    [Min(0)]
    public float c=1f;
    void Start()
    {
        mesh=new Mesh();
        vertices=new Vector3[8];
        triangles=new int[36];
        vertices[0]=new Vector3(0,0,0);
        vertices[1]=new Vector3(0,c,0);
        vertices[2]=new Vector3(c,c,0);
        vertices[3]=new Vector3(c,0,0);
        vertices[4]=new Vector3(0,0,c);
        vertices[5]=new Vector3(0,c,c);
        vertices[6]=new Vector3(c,c,c);
        vertices[7]=new Vector3(c,0,c);
        triangles[0]=0;
        triangles[1]=1;
        triangles[2]=2;
        triangles[3]=2;
        triangles[4]=3;
        triangles[5]=0;
        triangles[6]=5;
        triangles[7]=4;
        triangles[8]=7;
        triangles[9]=7;
        triangles[10]=6;
        triangles[11]=5;
        triangles[12]=0;
        triangles[13]=4;
        triangles[14]=1;
        triangles[15]=1;
        triangles[16]=4;
        triangles[17]=5;
        triangles[18]=3;
        triangles[19]=2;
        triangles[20]=6;
        triangles[21]=3;
        triangles[22]=6;
        triangles[23]=7;
        triangles[24]=1;
        triangles[25]=5;
        triangles[26]=2;
        triangles[27]=5;
        triangles[28]=6;
        triangles[29]=2;
        triangles[30]=0;
        triangles[31]=3;
        triangles[32]=7;
        triangles[33]=0;
        triangles[34]=7;
        triangles[35]=4;
        Centre();
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        gameObject.GetComponent<MeshFilter>().mesh=mesh;
    }
    void Update()
    {
        
    }
    public void Centre(){
        if(vertices==null||vertices.Length==0){
            return;
        }
        Vector3 centre=Vector3.zero;
        for(int i=0; i<vertices.Length; i++){
            centre+=vertices[i];
        }
        centre=centre/vertices.Length;
        for(int i=0; i<vertices.Length; i++){
            vertices[i]-=centre;
        }
    }
}
