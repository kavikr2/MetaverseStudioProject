using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Instance
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public bool isClientLogin;
    private bool connectedToRoom = false;
    private bool connectedToLobby = false;
    private bool connectedToServer = false;

    [HideInInspector]
    public string playerName = "";

    [HideInInspector]
    public Characters characterSelected;
    public Scenes thisScene = 0;

    private void Start()
    {

        PhotonNetwork.GameVersion = "0.01"; 
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Lobby Member";

        StartCoroutine(WaitForConnection(thisScene));

    }

    IEnumerator WaitForConnection(Scenes scenes)
    {
        while (!connectedToServer)
        {
            yield return null;
        }

        Debug.Log("Connected to Photon Server!");

        RoomOptions roomOptions = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(scenes.ToString(), roomOptions, TypedLobby.Default);
        StartCoroutine(WaitForJoinRoom());
    }

    IEnumerator WaitForJoinRoom()
    {
        while (!connectedToRoom)
        {
            yield return null;
        }

        Debug.Log("Joined Room " + PhotonNetwork.CurrentRoom.Name);
    }

    IEnumerator WaitForJoinLobby()
    {
        while (!connectedToLobby)
        {
            yield return null;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
        connectedToServer = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!");
        connectedToLobby = true;
    }

    public override void OnJoinedRoom()
    {
        connectedToRoom= true;
    }

    public void SetName(string name)
    {
        playerName = name;
        PhotonNetwork.NickName = name;
    }

    public void SceneChanger(Scenes scene)
    {
        thisScene= scene;
        PhotonNetwork.LoadLevel(scene.ToString());
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnLevelFinishedLoading(int level)
    {
        Debug.Log(level);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected because " + cause.ToString());
    }

}

public enum Scenes
{
    SM_LoginScene,
    SM_MetaverseScene,
    Minigame1,
    Minigame2
}

public enum Characters
{
    Zero,
    One,
    Two
}