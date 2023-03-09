using Cinemachine;
using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;

public class SM_StaffCharSelection : MonoBehaviour
{
    public HorizontalSelector mySelector;
    public CinemachineFreeLook canvasCamera;
    public MiniCamFollow miniMapCam;

    public SM_ThirdPersonCam _thirdPersonCam;
   
    public GameObject playerDisplayname;
    public SM_MinigameManager minigameManager;


    [Header("Spawner")]
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    public GameObject StaffChar1Object;
    public GameObject StaffChar2Object;

    public GameObject CharacterSelectionPanelObject;
    //public SM_CamController camController;

    int characterNo;

    public void SelectCharacter()
    {
        switch (characterNo)
        {
            case 0:
                StaffChar1Object.SetActive(true);
                break;

                case 1:
                StaffChar2Object.SetActive(true);
                break ;
        }

        GameManager.Instance.characterSelected = characterPrefabs[characterNo].name;
        SpawnPlayer();
        CharacterSelectionPanelObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        characterNo = mySelector.index;
        switch (characterNo)
        {
            case 0:
                StaffChar1Object.SetActive(true);
                StaffChar2Object.SetActive(false);
                break;

            case 1:
                StaffChar2Object.SetActive(true);
                StaffChar1Object.SetActive(false);
                break;
        }
    }
    public void SpawnPlayer()
    {
        if (GameManager.Instance.connectedToServer)
        {
            //PhotonNetwork.Instantiate("VoiceManager", transform.position, transform.rotation);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            // int characterPrefabIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            GameObject pp = PhotonNetwork.Instantiate(GameManager.Instance.characterSelected, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            GameManager.Instance.view = pp.GetComponent<PhotonView>();
            minigameManager.PlayerCamera = canvasCamera.gameObject; 
            minigameManager.Player = pp;

            canvasCamera.LookAt = pp.transform; 
            canvasCamera.Follow = pp.transform; 
            miniMapCam.Player = pp.transform;
            canvasCamera.enabled = true; miniMapCam.enabled = true;
            _thirdPersonCam.enabled = true;
            _thirdPersonCam.player = pp.transform;
            _thirdPersonCam.playerObj = pp.transform.Find("StaffObject");
            _thirdPersonCam.orientation = pp.transform.Find("Orientation");
            _thirdPersonCam.rb = pp.GetComponentInChildren<Rigidbody>();
            Destroy(gameObject);
        }
    }
}
