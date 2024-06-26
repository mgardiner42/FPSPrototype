using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class PlayerFlag : MonoBehaviour
{
    public bool hasFlag;
    public GameObject personalFlag;
    Renderer[] flagRenderers;
    GameObject gameFlag;
    
    private void Start(){
        // Variable to store the mesh renders of each child object of the flag
        flagRenderers = personalFlag.GetComponentsInChildren<Renderer>();
        gameFlag = GameObject.FindWithTag("Game Flag");
    }
    private void Update(){
        if(hasFlag && gameFlag != null && gameFlag.transform.parent != transform){
            hasFlag = false;

            foreach(Renderer r in flagRenderers){
                r.enabled = false;
            }
        }
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

        // Null check for the flag
        while (gameFlag == null){
            gameFlag = GameObject.FindWithTag("Game Flag");
        }

        // detatch the flag from the its current parent (the player holding the flag)
        gameFlag.transform.SetParent(null);

        // enable the renderers to make the flag visible again
        foreach(Renderer r in gameFlag.GetComponentsInChildren<Renderer>()){
            r.enabled = true;
        }

        // If client owns the flag then destroy and respawn the flag
        if(gameFlag.GetComponent<PhotonView>().IsMine){
            PhotonNetwork.Destroy(gameFlag);
            GameObject.Find("Room Manager").GetComponent<RoomManager>().SpawnFlag();
        }
    }
}
