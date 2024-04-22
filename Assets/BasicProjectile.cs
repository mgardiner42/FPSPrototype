using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public GameObject basic_proj;
    Transform scale;
    int nextUpdate;
    float growthRate = 0.1f;
    float maxGrowth = 1.5f;
    float initialGrowth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        scale = basic_proj.GetComponentInChildren<Transform>();
        nextUpdate = 0;
        GrowProjectile(initialGrowth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            // Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            GrowProjectile(growthRate);
        }


    }

    void GrowProjectile(float growth)
    {
        if (scale.localScale.x < maxGrowth)
        {
            //Growing each projectile on each frame until it hits its max size
            scale.localScale += new Vector3(growth, growth, growth);
        }
    }

    //Detects if a projectile is hit, may need to move to different script
    [PunRPC]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            if (GetComponent<PhotonView>().IsMine)
            {
                collision.gameObject.GetComponent<Health>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 25);
                PhotonNetwork.Destroy(basic_proj);
            }
        }
    }
}
