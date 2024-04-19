using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float remaining = 600f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTimer());
    }

      private IEnumerator UpdateTimer()
    {
        while (remaining > 0)
        {
            // Decrement the remaining time
            remaining -= 1f;

            // Update the timer display
            UpdateTimerDisplay();

            // Wait for the next frame
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimerDisplay() {
         // Convert the remaining time to minutes and seconds
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);

        // Update the UI text to display the timer
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(timerText.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
