using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UIElements;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remaining;

    private void Update(){
        // Convert the remaining time to minutes and seconds
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);

        // Update the UI text to display the timer
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void startTime(){
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remaining > 0)
        {
            if(PhotonNetwork.IsMasterClient){
                GetComponent<PhotonView>().RPC("syncTime", RpcTarget.All, remaining);
            }

            // Decrement the remaining time
            remaining -= 1f;
            // Wait for the next frame
            yield return new WaitForSeconds(1f);
        }
    }

    [PunRPC]
    private void syncTime(float time){
        if(!PhotonNetwork.IsMasterClient){
            Debug.Log(remaining);
            Debug.Log(timerText.text);
        }
        remaining = time;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(remaining);
        }
        else
        {
            remaining = (float)stream.ReceiveNext();
        }
    }
}
