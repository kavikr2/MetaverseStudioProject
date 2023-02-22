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

        Debug.Log("Loading pls wait ...");
        StartCoroutine(WaitForConnection());
    }

    IEnumerator WaitForConnection()
    {
        while (!connectedToServer)
        {
            yield return null;
        }

        if(thisScene == Scenes.SM_MetaverseScene) { EnterMetaverse(true); }
        else { StartCoroutine(PhotonSceneChanger(thisScene)); }
    }

    IEnumerator WaitForJoinRoom(string scene)
    {
        while (!connectedToRoom)
        {
           yield return null;
        }

        Debug.Log("Joined :" + scene.ToString());
    }

    IEnumerator PhotonSceneChanger(Scenes scene)
    {
        if (scene == Scenes.SM_LoginScene) {
            TypedLobby lobby = new TypedLobby(scene.ToString(), LobbyType.Default);
            PhotonNetwork.JoinLobby(lobby);
            Debug.Log("Welcome to KaviVerse");
        }
        else if(scene == Scenes.SM_MetaverseScene) 
        {
            connectedToServer = false; PhotonNetwork.LeaveRoom();

            while (!connectedToServer)
            {
                yield return null;
            }

            PhotonNetwork.RejoinRoom(scene.ToString());
        }
        else if(scene == Scenes.SM_Minigame1 || scene == Scenes.SM_Minigame2)
        {
            connectedToServer = false; PhotonNetwork.LeaveRoom();

            while (!connectedToServer)
            {
                yield return null;
            }
            RoomOptions roomOptions = new RoomOptions();
            PhotonNetwork.JoinOrCreateRoom(scene.ToString(), roomOptions, TypedLobby.Default);
            StartCoroutine(WaitForJoinRoom(scene.ToString()));
        }
    }

    public override void OnConnectedToMaster()
    {
        connectedToServer = true;
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

    public void EnterMetaverse(bool FirstTime)
    {
        thisScene = Scenes.SM_MetaverseScene;

        if (FirstTime){
            PhotonNetwork.LoadLevel(thisScene.ToString());
            RoomOptions roomOptions = new RoomOptions();
            PhotonNetwork.JoinOrCreateRoom(thisScene.ToString(), roomOptions, TypedLobby.Default);
            StartCoroutine(WaitForJoinRoom(thisScene.ToString()));
        }
        else {
            PhotonNetwork.LoadLevel(thisScene.ToString());
            StartCoroutine(PhotonSceneChanger(thisScene));
        }
    }

    public void SceneChanger(Scenes scene)
    {
        thisScene = scene;
        PhotonNetwork.LoadLevel(thisScene.ToString());
        StartCoroutine(PhotonSceneChanger(thisScene));
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
    SM_Minigame1,
    SM_Minigame2
}