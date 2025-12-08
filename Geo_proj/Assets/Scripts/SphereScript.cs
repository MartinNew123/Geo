using UnityEngine;

public class SphereScript : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    [Min(1)]
    public int r=1;
    [Min(3)]
    public int nbM=3;
    [Min(2)]
    public int nbP=2;
    private float angleM;
    private float angleIncrementM;
    private float angleP;
    private float angleIncrementP;
    void Start()
    {
        mesh=new Mesh();
        vertices=new Vector3[nbM*nbP+2];
        triangles=new int[nbM*((nbP-1)*2+2)*3];
        angleM=0;
        angleIncrementM=360f/nbM;
        angleP=0;
        angleIncrementP=360f/(nbP*2+2);
        vertices[0]=new Vector3(0,0,-r);
        vertices[1]=new Vector3(0,0,r);
        int ifCounter=0;
        if(nbP%2!=0){
            for(int i=2; i<nbM+2; i++){
                vertices[i]=new Vector3(r*Mathf.Cos(angleM*Mathf.Deg2Rad),r*Mathf.Sin(angleM*Mathf.Deg2Rad),0);
                angleM+=angleIncrementM;
            }
            angleM=0;
            ifCounter=1;
            angleP+=angleIncrementP;
        }
        else{
            angleP+=angleIncrementP/2f;
        }
        for(int j=ifCounter; j<nbP/2f; j++){
            for(int i=0; i<nbM; i++){
                vertices[2+i+nbM*j]=new Vector3((r*Mathf.Cos(angleM*Mathf.Deg2Rad))*Mathf.Cos(angleP*Mathf.Deg2Rad),(r*Mathf.Sin(angleM*Mathf.Deg2Rad))*Mathf.Cos(angleP*Mathf.Deg2Rad),-r*Mathf.Sin(angleP*Mathf.Deg2Rad));
                angleM+=angleIncrementM;
            }
            angleM=0;
            angleP+=angleIncrementP;
        }
        angleP-=(angleIncrementP*(nbP/2));
        for(int j=ifCounter; j<nbP/2f; j++){
            for(int i=0; i<nbM; i++){
                vertices[2+i+nbM*(j+(nbP/2))]=new Vector3((r*Mathf.Cos(angleM*Mathf.Deg2Rad))*Mathf.Cos(angleP*Mathf.Deg2Rad),(r*Mathf.Sin(angleM*Mathf.Deg2Rad))*Mathf.Cos(angleP*Mathf.Deg2Rad),r*Mathf.Sin(angleP*Mathf.Deg2Rad));
                angleM+=angleIncrementM;
            }
            angleM=0;
            angleP+=angleIncrementP;
        }
        int triangleCounter=0;
        int skipLoop=0;
        if(ifCounter==0){
            skipLoop=1;
        }
        for(int j=0; j<nbP/2f-1; j++){
            for(int i=0; i<nbM-1; i++){
                triangles[triangleCounter]=2+i+nbM*j;
                triangles[triangleCounter+1]=2+i+nbM*j+nbM;
                triangles[triangleCounter+2]=2+i+nbM*j+1;
                triangles[triangleCounter+3]=2+i+nbM*j+1;
                triangles[triangleCounter+4]=2+i+nbM*j+nbM;
                triangles[triangleCounter+5]=2+i+nbM*j+nbM+1;
                triangleCounter+=6;
            }
            triangles[triangleCounter]=2+(nbM-1)+nbM*j;
            triangles[triangleCounter+1]=2+(nbM-1)+nbM*j+nbM;
            triangles[triangleCounter+2]=2+nbM*j;
            triangles[triangleCounter+3]=2+nbM*j;
            triangles[triangleCounter+4]=2+(nbM-1)+nbM*j+nbM;
            triangles[triangleCounter+5]=2+nbM*j+nbM;
            triangleCounter+=6;
        }
        for(int i=0; i<nbM-1; i++){
            triangles[triangleCounter]=2+i+nbM*(nbP/2-skipLoop-1)+nbM;
            triangles[triangleCounter+1]=0;
            triangles[triangleCounter+2]=2+i+nbM*(nbP/2-skipLoop-1)+nbM+1;
            triangleCounter+=3;
        }
        triangles[triangleCounter]=2+(nbM-1)+nbM*(nbP/2-skipLoop-1)+nbM;
        triangles[triangleCounter+1]=0;
        triangles[triangleCounter+2]=2+nbM*(nbP/2-skipLoop-1)+nbM;
        triangleCounter+=3;
        for(int i=0; i<nbM-1; i++){
            triangles[triangleCounter]=2+i+1;
            triangles[triangleCounter+1]=2+i+nbM*(nbP/2-skipLoop)+nbM;
            triangles[triangleCounter+2]=2+i;
            triangles[triangleCounter+3]=2+i+nbM*(nbP/2-skipLoop)+nbM+1;
            triangles[triangleCounter+4]=2+i+nbM*(nbP/2-skipLoop)+nbM;
            triangles[triangleCounter+5]=2+i+1;
            triangleCounter+=6;
        }
        triangles[triangleCounter]=2;
        triangles[triangleCounter+1]=2+(nbM-1)+nbM*(nbP/2-skipLoop)+nbM;
        triangles[triangleCounter+2]=2+(nbM-1);
        triangles[triangleCounter+3]=2+nbM*(nbP/2-skipLoop)+nbM;
        triangles[triangleCounter+4]=2+(nbM-1)+nbM*(nbP/2-skipLoop)+nbM;
        triangles[triangleCounter+5]=2;
        triangleCounter+=6;
        for(int j=nbP/2+ifCounter; j<nbP-1; j++){
            for(int i=0; i<nbM-1; i++){
                triangles[triangleCounter]=2+i+nbM*j+1;
                triangles[triangleCounter+1]=2+i+nbM*j+nbM;
                triangles[triangleCounter+2]=2+i+nbM*j;
                triangles[triangleCounter+3]=2+i+nbM*j+nbM+1;
                triangles[triangleCounter+4]=2+i+nbM*j+nbM;
                triangles[triangleCounter+5]=2+i+nbM*j+1;
                triangleCounter+=6;
            }
            triangles[triangleCounter]=2+nbM*j;
            triangles[triangleCounter+1]=2+(nbM-1)+nbM*j+nbM;
            triangles[triangleCounter+2]=2+(nbM-1)+nbM*j;
            triangles[triangleCounter+3]=2+nbM*j+nbM;
            triangles[triangleCounter+4]=2+(nbM-1)+nbM*j+nbM;
            triangles[triangleCounter+5]=2+nbM*j;
            triangleCounter+=6;
        }
        for(int i=0; i<nbM-1; i++){
            triangles[triangleCounter]=2+i+nbM*(nbP-1)+1;
            triangles[triangleCounter+1]=1;
            triangles[triangleCounter+2]=2+i+nbM*(nbP-1);
            triangleCounter+=3;
        }
        triangles[triangleCounter]=2+nbM*(nbP-1);
        triangles[triangleCounter+1]=1;
        triangles[triangleCounter+2]=2+(nbM-1)+nbM*(nbP-1);
        triangleCounter+=3;
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        gameObject.GetComponent<MeshFilter>().mesh=mesh;
    }
    void Update()
    {
        
    }
}
