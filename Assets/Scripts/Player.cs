using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float _speed = 6.0f;
    public float _gravity = -9.81f;
    public float _jumb = 1.0f;

    public float normalFraction = 1.0f;
    public float winterFraction = 0.2f;

    CharacterController controller;
    Vector3 velocity;
    bool isGraounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }
    void Update()
    {
        //isGraounded = controller.isGraounded;
        if (isGraounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        Vector3 move = transform.right * movex + transform.forward * movez; 
        controller.Move(move * _speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGraounded)
        {
            velocity.y = Mathf.Sqrt(_jumb *-2f *_gravity);

        }
        velocity.y += _gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
    public void ApplySliding(float friction)
    {
        controller.slopeLimit = 45.0f * friction;

    }
    // Update is called once per frame
    

}

