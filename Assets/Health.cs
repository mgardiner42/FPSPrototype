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

    //Remote procedure call. This allows another player to run these scripts, such as if they deal damage
    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0) 
        {
            if (isLocalPlayer)
            {
                RespawnPlayer();
            }
            // add message "You Died!"
            // GUI.Label(new Rect(50, 50, 100, 20), "You Died!");
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

    //Is this old? If so, need to delete now that we have a proper HUD
    //public void OnGUI()
    //{
    //   if (health > 0)
    //    {
    //        GUI.Label(new Rect(50, 50, 100, 20), health.ToString() + " HP");
    //    }
    //}

    public void RespawnPlayer() {
        if(GetComponent<PlayerFlag>().hasFlag){
            GetComponent<PlayerFlag>().dropFlag();
        }
        transform.position = spawnpoint;
        health = MAX_HEALTH;
    }
}
