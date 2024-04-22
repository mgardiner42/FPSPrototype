using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Flag : MonoBehaviour
{
    Renderer[] childRenderers;
    private void Start(){
        // access mesh renders of each prefab
        childRenderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerEnter(Collider collision){
        if (collision.transform.tag == "Player" && transform.parent == null){
            transform.SetParent(collision.transform);
            foreach(Renderer childRenderer in childRenderers){
                // disable renderers when the flag is grabbed
                childRenderer.enabled = false;
            }
            collision.GetComponent<PlayerFlag>().grabFlag();
        }
    }
}
