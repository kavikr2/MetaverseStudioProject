using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

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
    [HideInInspector]
    public bool connectedToServer = false;

    [HideInInspector]
    public string playerName;

    [HideInInspector]
    public int characterSelected = 0;
    public Scenes thisScene = 0;

    public SM_SnapshotCamera snapCam;

    private void Start()
    {
        PhotonNetwork.GameVersion = "0.01";
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = "Lobby Member";
        playerName = "Kavi";

        StartCoroutine(WaitForConnection());
    }

    IEnumerator WaitForConnection()
    {
        while (!connectedToServer)
        {
            yield return null;
        }

        Debug.Log("Connected to Photon Server!");

        if(thisScene== 0) {
            TypedLobby lobby = new TypedLobby(thisScene.ToString(), LobbyType.Default);
            PhotonNetwork.JoinLobby(lobby);
            StartCoroutine(WaitForJoinLobby());
        } else {
            RoomOptions roomOptions = new RoomOptions();
            PhotonNetwork.JoinOrCreateRoom(thisScene.ToString(), roomOptions, TypedLobby.Default);
            StartCoroutine(WaitForJoinRoom(thisScene.ToString()));
        }
    }

    IEnumerator WaitForJoinRoom(string scene)
    {
        while (!connectedToRoom)
        {
           yield return null;
        }

        Debug.Log("Joined :" + scene.ToString());
    }

    IEnumerator WaitForJoinLobby()
    {
        while (!connectedToLobby)
        {
            yield return null;
        }

        Debug.Log("Joined :" +thisScene.ToString());
    }

    public override void OnConnectedToMaster()
    {
        connectedToServer = true;
    }

    public override void OnJoinedLobby()
    {
        connectedToLobby = true;
    }

    public override void OnCreatedRoom()
    {
        connectedToRoom = true;
    }

    public void SetName(string name)
    {
        playerName = name;
        PhotonNetwork.NickName = name;
    }

    public void SceneChanger(Scenes scene)
    {
        thisScene = scene;
        PhotonNetwork.LoadLevel(scene.ToString());

        RoomOptions roomOptions = new RoomOptions();

        PhotonNetwork.JoinOrCreateRoom(thisScene.ToString(), roomOptions, TypedLobby.Default);
        StartCoroutine(WaitForJoinRoom(thisScene.ToString()));
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
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