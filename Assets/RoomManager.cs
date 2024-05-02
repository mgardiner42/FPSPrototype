using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;


    public GameObject player;
    [Space]
    public Transform spawnPoint;

    public Transform flagSpawn;
    public GameObject flag;
    //public Vector3 spawnA;
    //public Vector3 spawnB;

    public Transform spawnPointA;
    public Transform spawnPointB;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("In Lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("In Room");

        //Spawn the player
        SpawnPlayer();
        if (PhotonNetwork.IsMasterClient){
            SpawnFlag();
        }
    }

    public void SpawnPlayer() 
    {
        // First player goes spawnA, second to spawnB
        if (PhotonNetwork.PlayerList.Length < 2){
            spawnPoint.position = spawnPointA.position;
        } else {
            spawnPoint.position = spawnPointB.position;
        }
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<Health>().spawnpoint = spawnPoint.position;

        //Tie player controls to this player
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
    }

    public void SpawnFlag() {
        PhotonNetwork.Instantiate(flag.name, flagSpawn.position, Quaternion.identity);
    }
}
