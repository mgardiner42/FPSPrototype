using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Specific projectile properties
    public GameObject basicprojectile;
    public GameObject basicblocker;
    public float power, vertForce;

    bool shooting, readyToShoot, reloading;
    public Camera camera1; 
    public List<AmmoCounts> ammoVals;
    public Projectiles currAmmo;
    int ammoNum;

    //Vars for shooting projectiles
    Ray ray;
    RaycastHit hit;
    Vector3 targetPoint;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        camera1 = GameObject.Find("blasterB").GetComponentInParent<Camera>();

        //TODO Setting Up Ammo System
        ammoVals = new List<AmmoCounts>();

        AmmoCounts p = new AmmoCounts{ammo = Projectiles.BasicProjectile, count = 10};
        AmmoCounts b = new AmmoCounts { ammo = Projectiles.BasicBlocker, count = 3 };

        //Ammo to begin the Round with
        ammoNum = 0;
        ammoVals.Add(p);
        ammoVals.Add(b);
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

       //switching ammo
       if(Input.GetKeyDown(KeyCode.Space))
        {
            currAmmo = ammoVals[ammoNum].ammo;
            ammoNum += 1;
            if(ammoNum >= ammoVals.Count)
            {
                ammoNum = 0;
            }
        }
    }

    //Method to shoot projectiles -- Switches the guntype depending on the current Ammo selected
    private void Shoot(Projectiles type)
    {

        switch (type) {

            //Case for Basic Projectile
            case Projectiles.BasicProjectile:

                //Cast ray to find exact hit position
                ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                //checking if anything is hit
                if (Physics.Raycast(ray, out hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.GetPoint(75); //75 is arbitrary distance
                }

<<<<<<< Updated upstream
                Vector3 direction = targetPoint - camera1.transform.position;

                GameObject projectile_new = PhotonNetwork.Instantiate(basicprojectile.name, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);
=======
                direction = targetPoint - camera1.transform.position;
                GameObject projectile_new = Instantiate(basicprojectile, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);
>>>>>>> Stashed changes

                //Grabs forward vector of the attack point and shoots projectile in that direction
                projectile_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("AttackPoint").transform.forward * 5, ForceMode.Impulse);

                //Destorys projectiles after a set period of time
                Destroy(projectile_new, 10);
                break;

            case Projectiles.BasicBlocker: //TODO: Write Script for the basic blocker
                
                //Cast ray to find exact hit position
                ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                //checking if anything is hit
                if (Physics.Raycast(ray, out hit))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    targetPoint = ray.GetPoint(75); //75 is arbitrary distance
                }

                direction = targetPoint - camera1.transform.position;
                GameObject blocker_new = Instantiate(basicblocker, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);

                blocker_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("AttackPoint").transform.forward*5, ForceMode.Impulse);

                Destroy(blocker_new, 20);
                break;
            default:
                break;
        }
    }
}
