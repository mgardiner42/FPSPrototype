using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Specific projectile properties
    public GameObject projectile;
    public float power, vertForce;

    bool shooting, readyToShoot, reloading;
    public Camera camera;

    // TODO Figure out how to attach the AttackPoint position into this script, then instantiate the projectile


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }


    //Control Method for shooting the gun
    private void UserInput()
    {
       shooting = Input.GetKeyDown(KeyCode.Mouse0);
       if (shooting)
        {
            shoot();
        } 
    }

    //Method to shoot projectiles -- Initial demo test
    private void shoot()
    {
        //Cast ray to find exact hit position
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        //checking if anything is hit
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //75 is arbitrary distance
        }

        Vector3 direction = targetPoint - camera.transform.position;


        projectile = Instantiate(projectile, camera.transform.position, Quaternion.identity);
        projectile.transform.forward = direction.normalized;
        projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * 5, ForceMode.Impulse);
        projectile.GetComponent<Rigidbody>().AddForce(camera.transform.up * 5, ForceMode.Impulse);
    }
}
