using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        // start a coroutine that constantly checks if the player holds the flag
        if (PhotonNetwork.IsMasterClient){
            StartCoroutine(AddPoints());
        }

    }
    private void Update(){
        pointText.text = myPoints.ToString();
        enemyPointText.text = enemyPoints.ToString();

        //check for win condition
        if (myPoints == scoreToWin){
            isActive = false;
            // GetComponent<PhotonView>().RPC("endGame", RpcTarget.All, PhotonNetwork.NickName);
            endGameText.text = "YOU WIN!";
            StartCoroutine(wait());
        }
        if (enemyPoints == scoreToWin){
            isActive = false;
            endGameText.text = "YOU LOSE!";
            StartCoroutine(wait());
        }
    }
    // wait at the end then load the main menu
    private IEnumerator wait(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Launcher");
    }

    // Every second check if the player has the flag or if the flag belongs to another player
    private IEnumerator AddPoints(){
        while (isActive){
            // Null check for flag in case the flag is dropped
            while (gameFlag == null){
                gameFlag = GameObject.FindWithTag("Game Flag");
            }
            
            // If current client has flag
            if(GetComponent<PlayerFlag>().hasFlag){
                myPoints += 1;
            }
            // if another player has flag 
            else if(gameFlag.transform.parent != null){
                enemyPoints += 1;
            }

            // Master client will send their score to ensure other player's score is in sync with theirs
            if(PhotonNetwork.IsMasterClient){
                GetComponent<PhotonView>().RPC("syncPoints", RpcTarget.All, myPoints, enemyPoints);
            }

            // add a point every secondsToScore
            yield return new WaitForSeconds(secondsToScore);
        }
    }

    [PunRPC]
    private void syncPoints(int me, int enemy){
        myPoints = me;
        enemyPoints = enemy;
    }

    // Uses photon's i/o stream to read information sent over the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // If player is sending 
        if (stream.IsWriting)
        {
            stream.SendNext(myPoints);
            stream.SendNext(enemyPoints);
        }
        // If player is receiving
        else
        {
            // Master client sends their score first, then what they have as your score
            enemyPoints = (int)stream.ReceiveNext();
            myPoints = (int)stream.ReceiveNext();
        }
    }
}
