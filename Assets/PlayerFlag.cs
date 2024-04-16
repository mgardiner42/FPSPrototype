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

    // TODO: Figure out how to drop the flag when killed and make it visible
    // TODO: Test with multiple players. (Can both players see and grab the flag)

}
