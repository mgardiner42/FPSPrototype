using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {

        [SerializeField]
        private byte maxPlayersPerRoom = 2;

       // This client's version number. Users are separated from each other by gameVersion
        string gameVersion = "1";

        public void Start()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }
            
            //Resetting the cursor
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        void Awake()
        {
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        //Calls to launch different scenes or quit game
        public void LaunchGreenMap()
        {
            SceneManager.LoadScene("GreenMap");
        }

        public void LaunchShoeBox(){
            SceneManager.LoadScene("ShoeBox");
        }
        
        public void LaunchCity(){
            SceneManager.LoadScene("CityMap");
        }

        public void LaunchControls()
        {
            SceneManager.LoadScene("ControlScreen");
        }

        public void LaunchMenu()
        {
            SceneManager.LoadScene("Launcher");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
