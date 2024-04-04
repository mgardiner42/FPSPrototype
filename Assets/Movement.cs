using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float maxVelocityChange = 10f;

    private Vector2 input;
    private Rigidbody rb;
    private Health health;

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
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;

            return velocityChange;
        }

        return new Vector3();
    }
}
