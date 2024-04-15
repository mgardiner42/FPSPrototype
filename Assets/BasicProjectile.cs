using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public GameObject basic_proj;
    Transform scale;
    int nextUpdate;

    // Start is called before the first frame update
    void Start()
    {
        scale = basic_proj.GetComponentInChildren<Transform>();
        nextUpdate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            // Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            GrowProjectile();
        }


    }

    void GrowProjectile()
    {
        if (scale.localScale.x < 1.5)
        {
            //Growing each projectile on each frame until it hits its max size
            scale.localScale += new Vector3(.1f, .1f, .1f);
        }
    }
}
