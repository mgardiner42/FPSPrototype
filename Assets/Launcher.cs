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

        void Awake()
        {
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }


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

        public void Quit()
        {
            Application.Quit();
        }
    }
}
