using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int MAX_HEALTH;

    //Remote procedure call. This allows another player to run these scripts, such as if they deal damage
    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0) 
        {
            // add message "You Died!"
            //GUI.Label(new Rect(10, 10, 100, 20), "You Died!");
            // add particle effects to show damage taken
            Destroy(gameObject);
        }
    }
    public void Heal(int _health)
    {
        health += _health;
        if (health > MAX_HEALTH)
        {
            health = MAX_HEALTH;
        }
    }

    public void OnGUI()
    {
        if (health >= 0)
        {
            GUI.Label(new Rect(50, 50, 100, 20), health.ToString() + " HP");
        }
    }
}
