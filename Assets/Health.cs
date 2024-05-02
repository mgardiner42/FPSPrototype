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

    private AudioSource audioSource;
    public AudioClip deathSound;

      /* https://en.wikipedia.org/wiki/File:Wilhelm_Scream.ogg
       * This file is made available under the Creative Commons CC0 
       * 1.0 Universal Public Domain Dedication. */

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
                RespawnPlayer();
            }
            // add message "You Died!"
            // add particle effects to show damage taken
            audioSource.clip = deathSound;
            audioSource.Play();
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

    //Is this old? If so, need to delete now that we have a proper HUD
    //public void OnGUI()
    //{
    //   if (health > 0)
    //    {
    //        GUI.Label(new Rect(50, 50, 100, 20), health.ToString() + " HP");
    //    }
    //}

    [PunRPC]
    public void RespawnPlayer() {
        if(GetComponent<PlayerFlag>().hasFlag){
            GetComponent<PlayerFlag>().GetComponent<PhotonView>().RPC("dropFlag", RpcTarget.All);
        }
        transform.position = spawnpoint;
        health = MAX_HEALTH;
        gun.GetComponent<Ammo>().charge = gun.GetComponent<Ammo>().MAX_COUNT;
    }
}
