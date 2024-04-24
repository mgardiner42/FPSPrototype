using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Points : MonoBehaviour
{
    public int myPoints;
    public int enemyPoints;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI enemyPointText;
    public int secondsToScore;
    GameObject gameFlag;

    void Start()
    {
        gameFlag = GameObject.FindWithTag("Game Flag");

        // start a coroutine that consistently checks if the player holds the flag
        StartCoroutine(AddPoints());

    }

    private IEnumerator AddPoints(){
        while (true){
            // add points and update the HUD
            if(GetComponent<PlayerFlag>().hasFlag){
                myPoints += 1;
                pointText.text = myPoints.ToString();
            } else if(gameFlag.transform.parent != null){
                enemyPoints += 1;
                enemyPointText.text = enemyPoints.ToString();
            }

            // add a point every secondsToScore
            yield return new WaitForSeconds(secondsToScore);
        }
    }
 
}
