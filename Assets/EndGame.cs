using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public GameObject winner;

    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.Disconnect();
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winner = GameObject.Find("Winner");
        winnerText.text = winner.GetComponent<TextMeshProUGUI>().text;
    }
}
