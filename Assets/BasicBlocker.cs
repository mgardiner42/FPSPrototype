using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBlocker : MonoBehaviour
{
    Transform scale;
    int nextUpdate;
    public GameObject basicBlocker;

    //TODO Fix Model of Blocker

    void Start()
    {
        scale = basicBlocker.GetComponentInChildren<Transform>();
        nextUpdate = 0;
    }

    
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            GrowProjectile();
        }

    }

    //Growing the Projectile
    void GrowProjectile()
    {
        if (scale.localScale.z < 1) // TODO Might need to play with Y value to scale for full body of model
        {
            //Growing each projectile on each frame until it hits its max size
            scale.localScale += new Vector3(.1f, .1f, .1f);
        }
    }
}
