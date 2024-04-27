using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceDamage : MonoBehaviour
{
    public int damage = 10000;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            if (GetComponent<PhotonView>().IsMine)
            {
                collision.gameObject.GetComponent<Health>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
