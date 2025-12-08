using UnityEngine;

public class ConeScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    [Min(2)]
    public int h=2;
    [Min(1)]
    public int r=1;
    [Min(3)]
    public int nbM=3;
    [Min(1)]
    public int hT=1;
    public bool tronque=true;
    private float angle;
    private float angleIncrement;
    void Start()
    {
        mesh=new Mesh();
        if(tronque==false){
            vertices=new Vector3[nbM+2];
            triangles=new int[nbM*6];
            angle=0;
            angleIncrement=360f/nbM;
            vertices[0]=new Vector3(0,0,0);
            vertices[1]=new Vector3(0,0,h);
            for(int i=2; i<nbM+2; i++){
                vertices[i]=new Vector3(r*Mathf.Cos(angle*Mathf.Deg2Rad),r*Mathf.Sin(angle*Mathf.Deg2Rad),0);
                angle+=angleIncrement;
            }
            int triangleCounter=0;
            for(int i=2; i<nbM+1; i++){
                triangles[triangleCounter]=i;
                triangles[triangleCounter+1]=0;
                triangles[triangleCounter+2]=i+1;
                triangles[triangleCounter+3]=i+1;
                triangles[triangleCounter+4]=1;
                triangles[triangleCounter+5]=i;
                triangleCounter+=6;
            }
            triangles[triangleCounter]=nbM+1;
            triangles[triangleCounter+1]=0;
            triangles[triangleCounter+2]=2;
            triangles[triangleCounter+3]=2;
            triangles[triangleCounter+4]=1;
            triangles[triangleCounter+5]=nbM+1;
        }
        else{
            vertices=new Vector3[nbM*2+2];
            triangles=new int[nbM*12];
            angle=0;
            angleIncrement=360f/nbM;
            if(hT>=h){
                hT=h-1;
            }
            float rT=(r*(h-hT))/(float)h;
            vertices[0]=new Vector3(0,0,hT);
            vertices[1]=new Vector3(0,0,0);
            for(int i=2; i<nbM*2+2; i+=2){
                vertices[i]=new Vector3(rT*Mathf.Cos(angle*Mathf.Deg2Rad),rT*Mathf.Sin(angle*Mathf.Deg2Rad),hT);
                vertices[i+1]=new Vector3(r*Mathf.Cos(angle*Mathf.Deg2Rad),r*Mathf.Sin(angle*Mathf.Deg2Rad),0);
                angle+=angleIncrement;
            }
            int triangleCounter=0;
            for(int i=2; i<nbM*2; i+=2){
                triangles[triangleCounter]=i;
                triangles[triangleCounter+1]=i+1;
                triangles[triangleCounter+2]=i+2;
                triangles[triangleCounter+3]=i+2;
                triangles[triangleCounter+4]=i+1;
                triangles[triangleCounter+5]=i+3;
                triangles[triangleCounter+6]=i+1;
                triangles[triangleCounter+7]=1;
                triangles[triangleCounter+8]=i+3;
                triangles[triangleCounter+9]=i+2;
                triangles[triangleCounter+10]=0;
                triangles[triangleCounter+11]=i;
                triangleCounter+=12;
            }
            triangles[triangleCounter]=nbM*2;
            triangles[triangleCounter+1]=nbM*2+1;
            triangles[triangleCounter+2]=2;
            triangles[triangleCounter+3]=2;
            triangles[triangleCounter+4]=nbM*2+1;
            triangles[triangleCounter+5]=3;
            triangles[triangleCounter+6]=nbM*2+1;
            triangles[triangleCounter+7]=1;
            triangles[triangleCounter+8]=3;
            triangles[triangleCounter+9]=2;
            triangles[triangleCounter+10]=0;
            triangles[triangleCounter+11]=nbM*2;
        }
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        gameObject.GetComponent<MeshFilter>().mesh=mesh;
    }
    void Update()
    {
        
    }
}
