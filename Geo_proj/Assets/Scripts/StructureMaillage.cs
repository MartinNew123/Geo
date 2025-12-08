using UnityEngine;
using System;
using System.IO;

public class StructureMaillage : MonoBehaviour
{
    public TextAsset meshText;
    private int nbS;
    private int nbF;
    private int nbA;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector3[] normF;
    private Vector3[] normS;
    private Mesh mesh;
    void Start()
    {
        if(meshText!=null){
            ReadData();
            Centre();
            NormalSize();
            CalculNormTri();
            CalculNormSom();
            mesh=new Mesh();
            mesh.vertices=vertices;
            mesh.triangles=triangles;
            if(normS!=null&&normS.Length==vertices.Length){
                mesh.normals=normS;
            }
            else{
                mesh.RecalculateNormals();
            }
            mesh.RecalculateBounds();
            MeshFilter mf=gameObject.GetComponent<MeshFilter>();
            if(mf!=null){
                mf.mesh=mesh;
            }
            ExportFile("export.off");
        }
    }
    void Update()
    {
        
    }
    public void ReadData(){
        using (StringReader reader=new StringReader(meshText.text)){
            string line;
            string[] splitLine;
            if((line=reader.ReadLine())!=null){
                if(line!="OFF"){
                    return;
                }
            }
            else{
                return;
            }
            if((line=reader.ReadLine())!=null){
                splitLine=line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                nbS=int.Parse(splitLine[0]);
                nbF=int.Parse(splitLine[1]);
                nbA=int.Parse(splitLine[2]);
                vertices=new Vector3[nbS];
                triangles=new int[nbF*3];
            }
            else{
                return;
            }
            for(int i=0; i<nbS; i++){
                if((line=reader.ReadLine())!=null){
                    splitLine=line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    float x=float.Parse(splitLine[0]);
                    float y=float.Parse(splitLine[1]);
                    float z=float.Parse(splitLine[2]);
                    vertices[i]=new Vector3(x,y,z);
                }
                else{
                    return;
                }
            }
            for(int i=0; i<nbF; i++){
                if((line=reader.ReadLine())!=null){
                    splitLine=line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if(int.Parse(splitLine[0])!=3){
                        return;
                    }
                    int t1=int.Parse(splitLine[1]);
                    int t2=int.Parse(splitLine[2]);
                    int t3=int.Parse(splitLine[3]);
                    triangles[i*3]=t1;
                    triangles[i*3+1]=t2;
                    triangles[i*3+2]=t3;
                }
                else{
                    return;
                }
            }
        }
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
    public void NormalSize(){
        if(vertices==null||vertices.Length==0){
            return;
        }
        float max=0f;
        for(int i=0; i<vertices.Length; i++){
            float x=Mathf.Abs(vertices[i].x);
            float y=Mathf.Abs(vertices[i].y);
            float z=Mathf.Abs(vertices[i].z);
            if(x>max){
                max=x;
            }
            if(y>max){
                max=y;
            }
            if(z>max){
                max=z;
            }
        }
        if(max<=0f){
            return;
        }
        for(int i=0; i<vertices.Length; i++){
            vertices[i]/=max;
        }
    }
    public void CalculNormTri(){
        if(triangles==null||vertices==null){
            return;
        }
        normF=new Vector3[nbF];
        for(int i=0; i<nbF; i++){
            int t1=triangles[i*3];
            int t2=triangles[i*3+1];
            int t3=triangles[i*3+2];
            Vector3 v1=vertices[t1];
            Vector3 v2=vertices[t2];
            Vector3 v3=vertices[t3];
            Vector3 a1=v2-v1;
            Vector3 a2=v3-v1;
            Vector3 n=Vector3.Cross(a1, a2);
            n=n.normalized;
            normF[i]=n;
        }
    }
    public void CalculNormSom(){
        if(normF==null||normF.Length==0){
            return;
        }
        normS=new Vector3[nbS];
        for(int i=0; i<nbF; i++){
            Vector3 n=normF[i];
            int t1=triangles[i*3];
            int t2=triangles[i*3+1];
            int t3=triangles[i*3+2];
            normS[t1]+=n;
            normS[t2]+=n;
            normS[t3]+=n;
        }
        for(int i=0; i<nbS; i++){
            normS[i]=normS[i].normalized;
        }
    }
    public void ExportFile(string fileName){
        if(vertices==null||vertices.Length==0||triangles==null||triangles.Length==0){
            return;
        }
        string folder=Path.Combine(Application.dataPath, "Exports");
        string path=Path.Combine(folder, fileName);
        using (StreamWriter writer=new StreamWriter(path, false)){
            writer.WriteLine("OFF");
            writer.WriteLine(nbS+" "+nbF+" "+0);
            for (int i=0; i<nbS; i++){
                Vector3 v=vertices[i];
                writer.WriteLine(v.x+" "+v.y+" "+v.z);
            }
            for (int i=0; i<nbF; i++){
                int t1=triangles[i*3];
                int t2=triangles[i*3+1];
                int t3=triangles[i*3+2];
                writer.WriteLine("3 "+t1+" "+t2+" "+t3);
            }
        }
    }
}
