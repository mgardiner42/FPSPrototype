using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public GameObject basic_proj;
    Transform scale;
    int nextUpdate;
    public float growthRate = 0.1f;
    public float maxGrowth = 1.5f;
    public float initialGrowth = 0f;
    public int damage = 25;
    public float gracePeriod = 0.5f;
    float lifespan = 0f;

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

        lifespan += Time.deltaTime;
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
                //Dont deal damage to the local player
                if (lifespan >= gracePeriod || collision.gameObject.GetComponent<Health>().isLocalPlayer == false || damage <= 0)
                {
                    collision.gameObject.GetComponent<Health>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
                    PhotonNetwork.Destroy(basic_proj);
                }
            }
        }
    }
}
