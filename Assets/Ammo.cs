using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Projectiles
{
    BasicProjectile,
    BasicBlocker
}

public struct AmmoCounts
{
    public Projectiles ammo;
    public int count;
}

public class Ammo : MonoBehaviour
{
    //Enummeration for all the differnt types of projectiles
    //Can be added to as development continues
   
    public Projectiles SwitchAmmo(Projectiles type)
    {
        return type;
    }

}
