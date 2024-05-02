using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Molotov : MonoBehaviour
{

    public GameObject molotov;
    public GameObject aoe; //stands for area of effect
    public GameObject particles;
    public float damageRadius = 100;
    public int damage = 10;
    bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collided == false)
        {
            RotateBottle();
        }
    }

    private void RotateBottle()
    {
        molotov.transform.Rotate(new Vector3(molotov.transform.rotation.x + 1, molotov.transform.rotation.y +1, molotov.transform.rotation.z +1)); 
    }

    [PunRPC]
    private void OnCollisionEnter(Collision collision)
    {
        if (collided == false)
        {
            //molotov.GetComponent<Rigidbody>().isKinematic = true;
            float scale = damageRadius * 100;
            aoe.transform.localScale = new Vector3(aoe.transform.localScale.x * scale, aoe.transform.localScale.y * scale, aoe.transform.localScale.z * scale);
            collided = true;
            molotov.GetComponent<MeshRenderer>().enabled = false;
            particles.GetComponent<ParticleSystem>().enableEmission = true;

            //StartCoroutine(MolotovExplosion());
            //PhotonNetwork.Destroy(molotov);
        }
    }

    IEnumerator MolotovExplosion()
    {
        
        float scale = damageRadius * 10;
        aoe.transform.localScale += new Vector3(aoe.transform.localScale.x * scale, aoe.transform.localScale.y * scale, aoe.transform.localScale.z * scale);
        PhotonNetwork.Destroy(molotov);
        yield return null;
    }
}
