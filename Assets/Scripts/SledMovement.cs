using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SledMovement : MonoBehaviour
{
    public float speed = 7f;
    Rigidbody Rigidbody;
    public bool isSleding = true;
    public GameObject sled;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = sled.GetComponent<Rigidbody>(); 

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)){
           
            isSleding =!isSleding;
            sled.SetActive(isSleding);
        }
        if (isSleding)
        {
            Vector3 Forward = transform.forward * speed * Time.deltaTime;
           Rigidbody.AddForce(Forward, ForceMode.Impulse);
        }


    }
}