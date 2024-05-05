using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AreaOfEffect : MonoBehaviour

    //Generalized script for any Area of Effect Weapon
{
    bool inArea = false; //Used to keep track of players entering and exiting the area, so as to prevent double damage
    public int damage;
    GameObject player;
    public GameObject projectile;

    void Start()
    {
        if(projectile.gameObject.name == "Molotov")
        {
            damage = projectile.GetComponent<Molotov>().damage;
        }
        
    }
    
   [PunRPC]
   IEnumerator TakeEffect(int damage)
    {
        while (inArea)
        {
            player.GetComponent<Health>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            yield return new WaitForSeconds(1f); //deals damage every second
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {

            inArea = true;
            player = other.gameObject;
            StartCoroutine(TakeEffect(damage));
        }

    }

    private void OnTriggerExit(Collider other)
    {
       if(other.tag == "Player")
        {
            inArea = false;
        }


    }
}
