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

        // start a coroutine that consistently checks if the player holds the flag
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
            GetComponent<PhotonView>().RPC("endGame", RpcTarget.All, PhotonNetwork.NickName);
        } 
    }

    private IEnumerator AddPoints(){
        while (isActive){
            while (gameFlag == null){
                gameFlag = GameObject.FindWithTag("Game Flag");
            }
            // add points and update the HUD
            if(GetComponent<PlayerFlag>().hasFlag){
                myPoints += 1;
            } else if(gameFlag.transform.parent != null){
                enemyPoints += 1;
            }

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

      public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(myPoints);
            stream.SendNext(enemyPoints);
        }
        else
        {
            enemyPoints = (int)stream.ReceiveNext();
            myPoints = (int)stream.ReceiveNext();
        }
    }

    [PunRPC]
    private void endGame(string winner){
        GameObject Winner = new GameObject();
        GameObject PrevScene = new GameObject();

        PrevScene.name = "PrevScene";
        PrevScene.AddComponent<Text>();
        PrevScene.GetComponent<Text>().text = SceneManager.GetActiveScene().name;

        Winner.name = "Winner";
        Winner.AddComponent<TextMeshProUGUI>();
        Winner.GetComponent<TextMeshProUGUI>().text = winner;
        // if (myPoints == 100){
        //     Winner.GetComponent<TextMeshProUGUI>().text = new string (PhotonNetwork.NickName + " Wins!");
        // } else {
        //     foreach (var player in PhotonNetwork.PlayerListOthers){
        //         if (player.NickName != PhotonNetwork.NickName){
        //             Winner.GetComponent<TextMeshProUGUI>().text = new string (player.NickName + " Wins!");
        //         }
        //     }
        // }
        DontDestroyOnLoad(Winner);
        DontDestroyOnLoad(PrevScene);
        SceneManager.LoadScene("EndGame");
    }
}
