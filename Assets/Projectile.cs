using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Specific projectile properties
    public GameObject projectile;
    public float power, vertForce;

    bool shooting, readyToShoot, reloading;
    public Camera camera1; 

    // TODO Figure out how to attach the AttackPoint position into this script, then instantiate the projectile


    // Start is called before the first frame update
    void Start()
    {
     camera1 = GameObject.Find("blasterB").GetComponentInParent<Camera>();
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
            Shoot();
        } 
    }

    //Method to shoot projectiles -- Initial demo test
    private void Shoot()
    {
        //Cast ray to find exact hit position
        Ray ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
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

        Vector3 direction = targetPoint - camera1.transform.position;


        GameObject projectile_new = Instantiate(projectile, GameObject.Find("blasterB").GetComponentInParent<Camera>().transform.position, Quaternion.identity);
        projectile_new.transform.forward = direction.normalized;
        projectile_new.GetComponent<Rigidbody>().AddForce(direction.normalized * 5, ForceMode.Impulse);
        projectile_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("blasterB").GetComponentInParent<Camera>().transform.up * 5, ForceMode.Impulse);

        //Destorys projectiles after a set period of time
        Destroy(projectile_new, 10);
    }
}
