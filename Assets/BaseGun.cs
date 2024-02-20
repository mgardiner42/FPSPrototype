using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public Camera camera;
    public int damage;
    public float fireDelay;

    public float nextFireTime;
    void Update() 
    {
        //Decrement firerate timer
        nextFireTime -= Time.deltaTime;

        //if its the next time to fire, fire
        if (Input.GetButton("Fire1") && nextFireTime < 0)
        {
            PrimaryAttack();
            nextFireTime = fireDelay;
        }
    }

    //TODO: wewrite so its not hitscan. 
    //Hitscan is the easist for an MVP since projectiles require their own code. 
    void PrimaryAttack() 
    {
        //Cast a ray from the player camera
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hitscan; 

        if (Physics.Raycast(ray.origin, ray.direction, out hitscan)) 
        {
            //check if it hits an object with health
            if (hitscan.transform.gameObject.GetComponent<Health>())
            {
                //Damage the object. Uses Photon "Remote procedure call" to get another player to take damage
                hitscan.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
