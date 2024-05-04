using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerSetup : MonoBehaviour
{
    public Movement movement;

    public GameObject camera;
    public GameObject hud;
    public GameObject weaponThirdPerson;
    public TextMeshProUGUI crosshair;
    public AudioSource audioSource;
    public GameObject _text;
    public GameObject player;
    public void IsLocalPlayer()
    {
        // IF TESTING YOU CAN ENABLE MOVEMENT HERE WITHOUT WAITING FOR TWO PLAYERS
        // movement.enabled = true;
        camera.SetActive(true);
        hud.SetActive(true);
        weaponThirdPerson.SetActive(false);
        PhotonNetwork.Instantiate("PlayerName", player.transform.position, Quaternion.identity);
        _text.GetComponentInChildren<TextMeshProUGUI>().text = PhotonNetwork.NickName;
        StartCoroutine(Countdown());
    }

    // Coroutine that waits for two players then displays a countdown signifying the start of a game
     private IEnumerator Countdown(){
        yield return new WaitUntil(() => PhotonNetwork.PlayerList.Length > 1);

        int countdown = 3;

        while (countdown > 0){
            crosshair.text = countdown.ToString();
            countdown--;
            yield return new WaitForSeconds(1f);
        }
        // put the crosshair back and enable movement
        crosshair.text = "+";
        movement.enabled = true;
        audioSource.Play();
    }
}
