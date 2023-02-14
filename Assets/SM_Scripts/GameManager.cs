using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public bool isClientLogin;
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

    [HideInInspector]
    public string playerName = "";

    [HideInInspector]
    public Characters characterSelected;
    public Scenes scenes = 0;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Lobby Guy";

        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() 
    {
        SceneManager.LoadScene(scenes.ToString());

        RoomOptions roomOptions = new RoomOptions();

        foreach (Scenes room in Enum.GetValues(typeof(Scenes)))
        {
            PhotonNetwork.CreateRoom(room.ToString(), roomOptions, TypedLobby.Default);
        }
    }

    public void SetName(string name)
    {
        playerName = name;
        PhotonNetwork.NickName = name;
    }

    public void SceneChanger(int scene)
    {
        SceneManager.LoadScene(Enum.GetName(typeof(Scenes), scene));
        PhotonNetwork.JoinRoom(Enum.GetName(typeof(Scenes), scene));
    }

    private void Update()
    {
        
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