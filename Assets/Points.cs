using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour
{
    public int points;
    public TextMeshProUGUI pointText;
    public int secondsToScore;

    void Start()
    {
        // start a coroutine that consistently checks if the player holds the flag
        StartCoroutine(AddPoints());
    }

    private IEnumerator AddPoints(){
        while (true){
            // add points and update the HUD
            if(GetComponent<PlayerFlag>().hasFlag){
                points += 1;
                pointText.text = points.ToString();
            }

            // add a point every secondsToScore
            yield return new WaitForSeconds(secondsToScore);
        }
    }
 
}
