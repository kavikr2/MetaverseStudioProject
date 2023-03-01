using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;

public class SM_StaffCharSelection : MonoBehaviour
{
    public HorizontalSelector mySelector;
    public SM_CamController canvasCamera;
    public MiniCamFollow miniMapCam;
   
    public GameObject playerDisplayname;


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
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            // int characterPrefabIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            GameObject pp = PhotonNetwork.Instantiate(GameManager.Instance.characterSelected, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            GameObject tp = Instantiate(playerDisplayname); tp.transform.SetParent(pp.transform.Find("NameHolder"), false);

            canvasCamera.target = pp.transform; miniMapCam.Player = pp.transform;
            canvasCamera.enabled = true; miniMapCam.enabled = true;

            Destroy(gameObject);
        }
    }
}
