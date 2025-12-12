using UnityEngine;

public class EnumSpa : MonoBehaviour
{
    [Min(1)]
    public int precision=20;
    public Material mat;
    public Vector3[] sphereCenters;
    public float[] sphereRad;
    public bool intersection=false;
    private int[,,] potentiels;
    public int potentielsLimit=0;
    void Start()
    {
        SphereVolume();
    }
    void Update()
    {
        
    }
    public void SphereVolume(){
        if(sphereCenters==null||sphereRad==null||sphereCenters.Length==0||sphereRad.Length==0||sphereCenters.Length!=sphereRad.Length){
            return;
        }
        if(potentiels!=null&&potentiels.GetLength(0)!=precision){
            DeleteCubes();
        }
        if(potentiels==null||potentiels.GetLength(0)!=precision){
            potentiels=new int[precision, precision, precision];
        }
        float side=transform.localScale.x;
        float cubeSide=side/precision;
        float[] radSq=new float[sphereRad.Length];
        for(int i=0; i<sphereRad.Length; i++){
            radSq[i]=sphereRad[i]*sphereRad[i];
        }
        Vector3 min=transform.position-new Vector3(side, side, side)/2f;
        for(int i=0; i<precision; i++){
            for(int j=0; j<precision; j++){
                for(int k=0; k<precision; k++){
                    Vector3 cubeCenter=new Vector3(min.x+(i+0.5f)*cubeSide, min.y+(j+0.5f)*cubeSide, min.z+(k+0.5f)*cubeSide);
                    bool intersec=false;
                    if(!intersection){
                        for(int l=0; l<sphereCenters.Length; l++){
                            if(Section(sphereCenters[l], radSq[l], cubeCenter, cubeSide/2f)){
                                intersec=true;
                                break;
                            }
                        }
                    }
                    else{
                        int intersecNb=0;
                        for(int l=0; l<sphereCenters.Length; l++){
                            if(Section(sphereCenters[l], radSq[l], cubeCenter, cubeSide/2f)){
                                intersecNb++;
                                if(intersecNb>=2){
                                    intersec=true;
                                    break;
                                }
                            }
                        }
                    }
                    if(!intersec){
                        continue;
                    }
                    if(potentiels[i, j, k]<potentielsLimit){
                        potentiels[i, j, k]++;
                        continue;
                    }
                    if(potentiels[i, j, k]>potentielsLimit){
                        continue;
                    }
                    GameObject newCube=new GameObject();
                    newCube.transform.SetParent(transform);
                    newCube.transform.position=cubeCenter;
                    newCube.transform.rotation=Quaternion.identity;
                    MeshFilter mf=newCube.AddComponent<MeshFilter>();
                    MeshRenderer mr=newCube.AddComponent<MeshRenderer>();
                    mr.material=mat;
                    CubeScript cubeScript=newCube.AddComponent<CubeScript>();
                    cubeScript.c=cubeSide;
                    potentiels[i, j, k]++;
                }
            }
        }
    }
    public bool Section(Vector3 sphereCenter, float radiusSq, Vector3 cubeCenter, float halfCubeSide){
        float minX=cubeCenter.x-halfCubeSide;
        float maxX=cubeCenter.x+halfCubeSide;
        float minY=cubeCenter.y-halfCubeSide;
        float maxY=cubeCenter.y+halfCubeSide;
        float minZ=cubeCenter.z-halfCubeSide;
        float maxZ=cubeCenter.z+halfCubeSide;
        float distSq=0f;
        if(sphereCenter.x<minX){
            float temp=minX-sphereCenter.x;
            distSq+=temp*temp;
        }
        else if(sphereCenter.x>maxX){
            float temp=sphereCenter.x-maxX;
            distSq+=temp*temp;
        }
        if(sphereCenter.y<minY){
            float temp=minY-sphereCenter.y;
            distSq+=temp*temp;
        }
        else if(sphereCenter.y>maxY){
            float temp=sphereCenter.y-maxY;
            distSq+=temp*temp;
        }
        if(sphereCenter.z<minZ){
            float temp=minZ-sphereCenter.z;
            distSq+=temp*temp;
        }
        else if(sphereCenter.z>maxZ){
            float temp=sphereCenter.z-maxZ;
            distSq+=temp*temp;
        }
        return distSq<=radiusSq;
    }
    public void DeleteCubes(){
        potentiels=new int[precision, precision, precision];
        for(int i=0; i<transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
