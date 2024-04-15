using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Specific projectile properties
    public GameObject basicprojectile;
    public float power, vertForce;

    bool shooting, readyToShoot, reloading;
    public Camera camera1; 
    public Dictionary<Projectiles, int> ammoVals;
    public Projectiles currAmmo;


    // Start is called before the first frame update
    void Start()
    {
        camera1 = GameObject.Find("blasterB").GetComponentInParent<Camera>();

        //TODO Setting Up Ammo System
        ammoVals = new Dictionary<Projectiles, int>();
   

        //Ammo to begin the Round with
        ammoVals.Add(Projectiles.BasicProjectile, 10);
        currAmmo = Projectiles.BasicProjectile;
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
            Shoot(currAmmo);
        } 
    }

    //Method to shoot projectiles -- Switches the guntype depending on the current Ammo selected
    private void Shoot(Projectiles type)
    {

        switch (type) {

            //Case for Basic Projectile
            case Projectiles.BasicProjectile:
                //Cast ray to find exact hit position
                Ray ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                Vector3 targetPoint;

                //checking if anything is hit
                if (Physics.Raycast(ray, out hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.GetPoint(75); //75 is arbitrary distance
                }

                Vector3 direction = targetPoint - camera1.transform.position;

                GameObject projectile_new = PhotonNetwork.Instantiate(basicprojectile.name, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);

                //Grabs forward vector of the attack point and shoots projectile in that direction
                projectile_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("AttackPoint").transform.forward * 5, ForceMode.Impulse);

                //Destorys projectiles after a set period of time
                Destroy(projectile_new, 10);
                break;
            default:
                break;
        }
    }
}
