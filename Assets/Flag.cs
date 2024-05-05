using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Renderer[] childRenderers;
    private AudioSource audioSource;
    public  AudioClip shing;


    private void Start(){
        // access mesh renders of each prefab
        childRenderers = GetComponentsInChildren<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    [PunRPC]
    private void OnTriggerEnter(Collider collision){
        // If the flag has collided with player
        if (collision.transform.tag == "Player" && transform.parent == null){
            audioSource.clip = shing;
            audioSource.Play();

            transform.SetParent(collision.transform);
            foreach(Renderer childRenderer in childRenderers){
                // disable renderers when the flag is grabbed
                childRenderer.enabled = false;
            }
            // move the flag off map to prevent the player grabbing the flag multiple times
            transform.position = new Vector3(0, 100, 0);
            collision.GetComponent<PlayerFlag>().GetComponent<PhotonView>().RPC("grabFlag", RpcTarget.All);
        }
    }
}
