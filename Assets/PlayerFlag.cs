using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFlag : MonoBehaviour
{
    public bool hasFlag;
    public GameObject personalFlag;
    Renderer[] flagRenderers;
    Transform gameFlag;
    
    private void Start(){
        // Variable to store the mesh renders of each child object of the flag
        flagRenderers = personalFlag.GetComponentsInChildren<Renderer>();
    }
    [PunRPC]
    public void grabFlag(){
        hasFlag = true;
        // enable each renderer to make the flag visible when player holds the flag
        foreach(Renderer r in flagRenderers){
            r.enabled = true;
        }
    }

    [PunRPC]
    public void dropFlag(){
        hasFlag = false;
        // disables the flag on the players back to show they no longer carry the flag
        foreach(Renderer r in flagRenderers){
            r.enabled = false;
        }
        
        foreach(Transform child in transform){
            if (child.CompareTag("Game Flag")){
                gameFlag = child.gameObject.transform;
            }
        }
        // detatch the flag from the its current parent (the player holding the flag)
        gameFlag.SetParent(null);
        // enable the renderers to make the flag visible again
        foreach(Renderer r in gameFlag.GetComponentsInChildren<Renderer>()){
            r.enabled = true;
        }
    }
}
