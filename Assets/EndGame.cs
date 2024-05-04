using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;


public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public GameObject winner;
    public GameObject PrevScene;
    private string prevSceneName;

    // Start is called before the first frame update
    private void Start()
    {
        PrevScene = GameObject.Find("PrevScene");
        prevSceneName = PrevScene.GetComponent<Text>().text;
        
        PhotonNetwork.Disconnect();
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winner = GameObject.Find("Winner");
        winnerText.text = winner.GetComponent<TextMeshProUGUI>().text;
    }

    public void LaunchLauncher(){
        SceneManager.LoadScene("Launcher");
    }

    public void PlayAgain(){
        SceneManager.LoadScene(prevSceneName);
    }
}
