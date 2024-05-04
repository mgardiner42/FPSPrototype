using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    // Specific projectile properties
    public GameObject basicprojectile;
    public GameObject basicblocker;
    public GameObject healprojectile;
    public GameObject molotov;

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
    public TextMeshProUGUI weaponText;

    //Projectile speeds
    public float VelocityProj = 20;

    // sound effect
    private AudioSource audioSource; // the paintball sound effect

    //[SerializeField]
    public AudioClip basicShotSound;


    /* Paintball gun shot.wav by Michaelvelo --
     * https://freesound.org/s/366835/ --
     * License: Attribution NonCommercial 3.0 */

    public AudioClip barrierSound;

    /* Bonk - [Rpg] 1 by colorsCrimsonTears -- 
     * https://freesound.org/s/641894/ -- 
     * License: Creative Commons 0 */


    public AudioClip flame;
    /*
     * Fast Fire Flare Whoosh 1(6lrs).wav by newlocknew -- 
     * https://freesound.org/s/614724/ -- 
     * License: Attribution NonCommercial 4.0
     */

    public AudioClip healing;
    /*
     * Healing 5 (Crystalline Respite) by SilverIllusionist -- 
     * https://freesound.org/s/654071/ -- 
     * License: Attribution 4.0
     */

    // Array of weapons
    string[] weapons = {"Gun", "Blocker", "HealGun", "Molotov" };
    int currentWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        blaster.transform.SetParent(camera1.GetComponent<Transform>());
        charges = GetComponent<Ammo>();

        //TODO Setting Up Ammo System
        ammoVals = new List<AmmoCounts>();
        charges.charge = 20;

        //TODO: as more weapons are added, creating method to auto set up all types

        AmmoCounts basicAmmo = new AmmoCounts {ammo = Projectiles.BasicProjectile, chargeCount = 1};
        AmmoCounts blockerAmmo = new AmmoCounts { ammo = Projectiles.BasicBlocker, chargeCount = 3 };
        AmmoCounts healingAmmo = new AmmoCounts { ammo = Projectiles.HealProjectile, chargeCount = 4 };
        AmmoCounts MolAmmo = new AmmoCounts { ammo = Projectiles.Molotov, chargeCount = 7 }; 

        //Ammo to begin the Round with
        ammoNum = 0;
        ammoVals.Add(basicAmmo);
        ammoVals.Add(blockerAmmo);
        ammoVals.Add(healingAmmo);
        ammoVals.Add(MolAmmo);
        currAmmo = Projectiles.BasicProjectile;

        // Setting Up Sound
        audioSource = GetComponent<AudioSource>();

        weaponText.text = weapons[0].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UserInput();
    }

    private IEnumerator DestroyProjectile(float remaining, GameObject projectile)
    {
        while(remaining > 0)
        {
            remaining -= 1f;
            yield return new WaitForSeconds(1f);
        }

        if (projectile != null)
        {
            PhotonNetwork.Destroy(projectile);
        }
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
       

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ammoNum = 0;
            currentWeapon = 0;  
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ammoNum = 1;
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ammoNum = 2;
            currentWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ammoNum = 3;
            currentWeapon = 3;
        }

        currAmmo = ammoVals[ammoNum].ammo;
        weaponText.text = weapons[currentWeapon].ToString();

    }

    //Method to shoot projectiles -- Switches the guntype depending on the current Ammo selected
    private void Shoot(Projectiles type)
    {

        switch (type) {

            //Case for Basic Projectile
            case Projectiles.BasicProjectile:
                // play sound for primary fire
                audioSource.clip = basicShotSound;
                audioSource.Play();

                fireProjectile(basicprojectile.name);
                break;

            case Projectiles.BasicBlocker:
                // play sound for blocker
                audioSource.clip = barrierSound;
                audioSource.Play();
                
                //Cast ray to find exact hit position
                ray = camera1.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

                
                targetPoint = ray.GetPoint(5); //5 is arbitrary distance
             

                direction = targetPoint - camera1.transform.position;
                GameObject blocker_new = PhotonNetwork.Instantiate(basicblocker.name, GameObject.Find("AttackPoint").transform.position, GameObject.Find("AttackPoint").transform.rotation);
                blocker_new.GetComponent<BasicBlocker>().direction = direction;

                //blocker_new.GetComponent<Rigidbody>().AddForce(GameObject.Find("AttackPoint").transform.forward*5, ForceMode.Impulse);

                StartCoroutine(DestroyProjectile(5, blocker_new));
                break;
            case Projectiles.HealProjectile:
                audioSource.clip = healing;
                audioSource.Play();

                fireProjectile(healprojectile.name);
                break;

            case Projectiles.Molotov:
                audioSource.clip = flame;
                audioSource.Play();

                //Will Change behavior, need to tweak velocity
                fireProjectile(molotov.name);
                break;

            default:
            
                break;
        }
    }

    private void fireProjectile(string proj)
    {
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

        GameObject projectile_new = PhotonNetwork.Instantiate(proj, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);
        direction = targetPoint - camera1.transform.position;
        //GameObject projectile_new = Instantiate(basicprojectile, GameObject.Find("AttackPoint").transform.position, Quaternion.identity);

        //Grabs forward vector of the attack point and shoots projectile in that direction
        projectile_new.GetComponent<Rigidbody>().AddForce(camera1.transform.forward * VelocityProj, ForceMode.Impulse);

        //Destorys projectiles after a set period of time
        StartCoroutine(DestroyProjectile(10, projectile_new));
    }
}
