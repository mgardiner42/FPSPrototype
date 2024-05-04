using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int MAX_HEALTH;

    public bool isLocalPlayer;

    public Vector3 spawnpoint;

    public TextMeshProUGUI healthText;

    public GameObject gun;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public GameObject gameFlag;

      /* https://en.wikipedia.org/wiki/File:Wilhelm_Scream.ogg
       * This file is made available under the Creative Commons CC0 
       * 1.0 Universal Public Domain Dedication. */

    private void Start(){
        gameFlag = GameObject.FindWithTag("Game Flag");
    }

    //Remote procedure call. This allows another player to run these scripts, such as if they deal damage
    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        //check for negative damage
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }


        //check for death
        if (health <= 0) 
        {
            if (isLocalPlayer)
            {
                audioSource.clip = deathSound;
                audioSource.Play();
                RespawnPlayer();
            }
            // add message "You Died!"
            // add particle effects to show damage taken
           
        }
        // update HUD
        healthText.text = health.ToString();
    }
    public void Heal(int _health)
    {

        health += _health;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }

        //Update HUD
        healthText.text = health.ToString();
    }

    [PunRPC]
    public void RespawnPlayer() {
        Vector3 deathPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if(GetComponent<PlayerFlag>().hasFlag){
            GetComponent<PlayerFlag>().GetComponent<PhotonView>().RPC("dropFlag", RpcTarget.All);
        }
        transform.position = spawnpoint;
        health = MAX_HEALTH;
        gun.GetComponent<Ammo>().charge = gun.GetComponent<Ammo>().MAX_COUNT;
     
        if (deathPos.y < -5){
            gameFlag.transform.position = GameObject.Find("Room Manager").GetComponent<RoomManager>().flagSpawn.position;
        } else {
            gameFlag.transform.position = transform.position;
        }
    }
}
