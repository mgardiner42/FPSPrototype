using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlag : MonoBehaviour
{
    public bool hasFlag;
    public GameObject personalFlag;
    Renderer[] flagRenderers;
    private void Start(){
        flagRenderers = personalFlag.GetComponentsInChildren<Renderer>();
    }
    public void grabFlag(){
        hasFlag = true;
        foreach(Renderer r in flagRenderers){
            r.enabled = true;
        }
    }

    public void dropFlag(){
        hasFlag = false;
        foreach(Renderer r in flagRenderers){
            r.enabled = false;
        }
        Transform gameFlag = transform.Find("Flag");
        gameFlag.SetParent(null);
        foreach(Renderer r in gameFlag.GetComponentsInChildren<Renderer>()){
            r.enabled = true;
        }
    }

    // TODO: Figure out how to drop the flag when killed and make it visible
    // TODO: Test with multiple players. (Can both players see and grab the flag)

}
