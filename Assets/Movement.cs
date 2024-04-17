using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float maxVelocityChange = 10f;
    public float jumpForce = 5f;
    public float friction = 2f;

    public LayerMask groundMask;
    private Vector2 input;
    private Rigidbody rb;
    private Health health;
    bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        //Jumping. Doesnt stop normal movement, cause thats fun
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        grounded = true;
    }

    //Detects if a projectile is hit, may need to move to different script
    //Need to integrate with spawn/despawn functions, otherwise number of bodies multiply
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "damage_projectile")
        {
            health.TakeDamage(25);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        Vector3 nullVelocity = new Vector3(0, 0, 0);
        targetVelocity = transform.TransformDirection(targetVelocity);
        nullVelocity = transform.TransformDirection(nullVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        Vector3 velocityChange = new Vector3();

        if (input.magnitude > 0.5f)
        {
            //accelerate the player in a certain direction
            velocityChange = targetVelocity - velocity;            
            
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
        }
        else 
        {
            //If we arent using keys and touching the ground, apply friction
            if (grounded) 
            {
                velocityChange = nullVelocity - velocity;
                velocityChange = velocityChange.normalized * friction;
                velocityChange.y = 0;
            }
        }

        return velocityChange;
    }
}
