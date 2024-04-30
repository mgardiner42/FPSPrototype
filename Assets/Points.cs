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
    public TextMeshProUGUI endGameText;
    public int secondsToScore;
    GameObject gameFlag;

    public int scoreToWin = 100;

    private bool isActive = true;

    void Start()
    {
        gameFlag = GameObject.FindWithTag("Game Flag");

        // start a coroutine that consistently checks if the player holds the flag
        StartCoroutine(AddPoints());

    }

    private IEnumerator AddPoints(){
        while (isActive){
            // add points and update the HUD
            if(GetComponent<PlayerFlag>().hasFlag){
                myPoints += 1;
                pointText.text = myPoints.ToString();
            } else if(gameFlag.transform.parent != null){
                enemyPoints += 1;
                enemyPointText.text = enemyPoints.ToString();
            }

            //check for win condition
            if (myPoints >= scoreToWin)
            {
                endGameText.text = "YOU WIN";
                isActive = false;
            }
            if (enemyPoints >= scoreToWin) 
            {
                endGameText.text = "YOU LOSE";
                isActive = false;
            }

            // add a point every secondsToScore
            yield return new WaitForSeconds(secondsToScore);
        }
    }
 
}
