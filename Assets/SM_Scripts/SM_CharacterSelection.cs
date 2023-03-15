using Cinemachine;
using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;

public class SM_CharacterSelection : MonoBehaviour
{
    [Header("References")]
    public HorizontalSelector mySelector;
    public CinemachineFreeLook canvasCamera;
    public MiniCamFollow miniMapCam;
    public GameObject playerDisplayname;
    public SM_MinigameManager minigameManager;
    public SM_LeaderBoard leaderBoard;
    public GameObject CharacterSelectionPanel;
    public SM_ThirdPersonCam _thirdPersonCam;

    public NotificationManager WelcomeNotification;

    [Header("Spawner")]
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    [Header("UI")]
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;

    
    int characterNo;

    public void SelectCharacter()
    {
        switch (characterNo)
        {
            case 0:
                Character1.SetActive(true);
                break;

            case 1:
                Character2.SetActive(true);
                break;

            case 2:
                Character3.SetActive(true);
                break;
        }

        GameManager.Instance.characterSelected = characterPrefabs[characterNo].name;
        SpawnPlayer();
        CharacterSelectionPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        characterNo = mySelector.index;

        switch (characterNo)
        {
            case 0:
                Character1.SetActive(true);
                Character2.SetActive(false);
                Character3.SetActive(false);
                break;

            case 1:
                Character2.SetActive(true);
                Character1.SetActive(false);
                Character3.SetActive(false);
                break;

            case 2:
                Character3.SetActive(true);
                Character1.SetActive(false);
                Character2.SetActive(false);
                break;
        }
    }

    public void SpawnPlayer()
    {
        if (GameManager.Instance.connectedToServer)
        {
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            int characterPrefabIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            GameObject pp = PhotonNetwork.Instantiate(GameManager.Instance.characterSelected, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            GameManager.Instance.view = pp.GetComponent<PhotonView>();
            minigameManager.PlayerCamera = canvasCamera.gameObject; minigameManager.Player = pp;

            canvasCamera.LookAt = pp.transform.Find("CamTarget");
            canvasCamera.Follow = pp.transform.Find("CamTarget");
            miniMapCam.Player = pp.transform;
            canvasCamera.enabled = true; 
            miniMapCam.enabled = true;
            _thirdPersonCam.enabled = true;
            _thirdPersonCam.player = pp.transform;
            _thirdPersonCam.playerObj = pp.transform.Find("PlayerObject");
            _thirdPersonCam.orientation = pp.transform.Find("Orientation");
            _thirdPersonCam.rb = pp.GetComponentInChildren<Rigidbody>();
            leaderBoard.view = pp.GetComponent <PhotonView>();
            Destroy(gameObject);
            WelcomeNotification.OpenNotification();
        }
    }
}
