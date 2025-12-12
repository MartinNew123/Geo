using UnityEngine;

public class ToolScript : MonoBehaviour
{
    public EnumSpa script;
    private Vector3 pos;
    private float radius;
    void Start()
    {
        pos=transform.position;
        radius=transform.localScale.x/2f;
        script.sphereCenters[0]=pos;
        script.sphereRad[0]=radius;
        script.DeleteCubes();
        script.SphereVolume();
    }
    void Update()
    {
        if(pos!=transform.position||radius!=transform.localScale.x/2f){
            pos=transform.position;
            radius=transform.localScale.x/2f;
            script.sphereCenters[0]=pos;
            script.sphereRad[0]=radius;
            script.SphereVolume();
        }
    }
}
