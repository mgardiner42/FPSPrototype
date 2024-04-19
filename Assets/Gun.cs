using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Specific projectile properties
    public GameObject basicprojectile;
    public GameObject basicblocker;

    bool shooting, readyToShoot, reloading;
    public Camera camera1;

    //Ammo variables
    Ammo charges;
    public List<AmmoCounts> ammoVals;
    public Projectiles currAmmo;
    int ammoNum;
    public GameObject blaster;

    //Vars for shooting projectiles
    Ray ray;
    RaycastHit hit;
    Vector3 targetPoint;
    Vector3 direction;

    //HUD Vars
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        blaster.transform.SetParent(camera1.GetComponent<Transform>());
        charges = GetComponent<Ammo>();

        //TODO Setting Up Ammo System
        ammoVals = new List<AmmoCounts>();
        charges.charge = 20;

        //TODO: as more weapons are added, creating method to auto set up all types

        AmmoCounts p = new AmmoCounts{ammo = Projectiles.BasicProjectile, chargeCount = 2};
        AmmoCounts b = new AmmoCounts { ammo = Projectiles.BasicBlocker, chargeCount = 3 };

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


    
    private void UserInput()
    {
        //Control Method for shooting the gun
        shooting = Input.GetKeyDown(KeyCode.Mouse0);
       if (shooting)
        {
            //Checking to see if there is enough charge to fire the current weapon equipped
            if (charges.charge - ammoVals[ammoNum].chargeCount >= 0)
            {
                charges.charge -= ammoVals[ammoNum].chargeCount; //Make sure the charge amount is spent
                ammoText.text = charges.charge.ToString();
                Shoot(currAmmo);
            }
        } 

       //switching ammo
       if(Input.GetKeyDown(KeyCode.Q))
        {
            if(ammoNum >= ammoVals.Count-1)
            {
                ammoNum = 0;
            }
            else
            {
                ammoNum += 1;
            }
            currAmmo = ammoVals[ammoNum].ammo;
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

                Vector3 direction = targetPoint - camera1.transform.position;

                GameObject projectile_new = PhotonNetwork.Instantiate(basicprojectile.name, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);
                direction = targetPoint - camera1.transform.position;
                //GameObject projectile_new = Instantiate(basicprojectile, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);

                //Grabs forward vector of the attack point and shoots projectile in that direction
                projectile_new.GetComponent<Rigidbody>().AddForce(camera1.transform.forward * 5, ForceMode.Impulse);

                //Destorys projectiles after a set period of time
                Destroy(projectile_new, 10);
                break;

            case Projectiles.BasicBlocker: //TODO: Fix the rotation of the blocker when it spawns in
                
                //Cast ray to find exact hit position
                ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                
                targetPoint = ray.GetPoint(5); //5 is arbitrary distance
             

                direction = targetPoint - camera1.transform.position;
                GameObject blocker_new = Instantiate(basicblocker, GameObject.Find("AttackPoint").transform.position, GameObject.Find("AttackPoint").transform.rotation);
                blocker_new.GetComponent<BasicBlocker>().direction = direction;

                //blocker_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("AttackPoint").transform.forward*5, ForceMode.Impulse);

                Destroy(blocker_new, 20);
                break;
            default:
                break;
        }
    }
}
