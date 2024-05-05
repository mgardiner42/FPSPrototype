using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace Com.MyCompany.MyGame
{
 
    // Player name input field. Let the user input his name, will appear above the player in the game.
    
    //[RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        

        // Store the PlayerPref Key to avoid typos
        const string playerNamePrefKey = "PlayerName";

        
        //MonoBehaviour method called on GameObject by Unity during initialization phase.
        
        void Start()
        {

            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        

       

       
        // Sets the name of the player, and save it in the PlayerPrefs for future sessions.
        // Feature still in development, couldn't get it to consistently work
        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

    }
}
