using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ArrowAndBow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform ShootPoint;
    public float arrowSpeed;
    private StarterAssetsInputs _input;
    // Start is called before the first frame update

    void ShootArrow()
    {


        if (_input.isShooting && !_input.sprint)
        {
            GameObject Arrow = Instantiate(arrowPrefab, ShootPoint.position, Quaternion.identity);
            Rigidbody rb = Arrow.GetComponent<Rigidbody>();
            rb.velocity = ShootPoint.forward
                * arrowSpeed;

        }
       

    }
}
