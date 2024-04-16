using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum Projectiles
{
    BasicProjectile,
    BasicBlocker
}

public struct AmmoCounts
{
    public Projectiles ammo;
    public int chargeCount;
    
}

public class Ammo : MonoBehaviour
{
    //Enummeration for all the differnt types of projectiles
    //Can be added to as development continues
    int MAX_COUNT = 20;
    int timeUpdate = 2;
    public int charge;
    public TextMeshProUGUI ammoText;

    private void Start()
    {
        charge = MAX_COUNT;
    }

    private void Update()
    {

        //Adds one count to the Charge meter every two seconds
        if(Time.time >= timeUpdate)
        {
            timeUpdate = Mathf.FloorToInt(Time.time)+2;
            if (charge < MAX_COUNT)
            {
                charge++;
                ammoText.text = charge.ToString();
            }
        }
    }

    public Projectiles SwitchAmmo(Projectiles type)
    {
        return type;
    }

}
