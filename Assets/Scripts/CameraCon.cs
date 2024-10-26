using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Transform camTarget;
    public float pos = .02f;
    public float rot = .01f;
  

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, pos); 
        transform.rotation = Quaternion.Lerp(transform.rotation,camTarget.rotation, rot);   
    }
}
