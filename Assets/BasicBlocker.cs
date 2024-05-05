using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlocker : MonoBehaviour
{
    Transform scale;
    int nextUpdate;
    public GameObject basicBlocker;
    public Vector3 direction;


    void Start()
    {
        scale = basicBlocker.GetComponentInChildren<Transform>();
        nextUpdate = 0;

        if (direction != null)
        {
            MoveProjectile();
        }
    }

    
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.deltaTime) + 1;
            GrowProjectile();

        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //This seems flimsy but works, may need to redo
        if(collision.gameObject.tag == "damage_projectile" || collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false); //Make sure to use the physics engine if one of these tags collides
        }
        else
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    //OLD CODE - behavior of the blocker has changed but leaving this here for future development efforts
    //Growing the Projectile
    void GrowProjectile()
    {
        if (scale.localScale.z < 1) // TODO Might need to play with Y value to scale for full body of model
        {
            //Growing each projectile on each frame until it hits its max size
            scale.localScale += new Vector3(.1f, .1f, .1f);
        }
    }

    void MoveProjectile()
    {
        Vector3 endPos = transform.position + direction;
        transform.position = Vector3.Lerp(transform.position, endPos, 100);
    }
}
