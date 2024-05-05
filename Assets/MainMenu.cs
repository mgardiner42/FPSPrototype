using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //First Test of switching scenes
    public void PlayGame () {
        SceneManager.LoadScene("SampleScene");
    }
}
