using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ArrowAndBow : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10);    
    }

    private void OnTriggerEnter(Collider other)
    {
        
            Destroy(transform.GetComponent<Rigidbody>());
        
    }


}
