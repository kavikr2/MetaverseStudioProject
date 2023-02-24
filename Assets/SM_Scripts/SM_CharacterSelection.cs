using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;

public class SM_CharacterSelection : MonoBehaviour
{
    public HorizontalSelector mySelector;
    public SM_CamController canvasCamera;
    public MiniCamFollow miniMapCam;
    public GameObject playerDisplayname;

    [Header("Spawner")]
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform[] spawnPoints;

    [Header("UI")]
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;

    public GameObject CharacterSelectionPanel;
    
    int characterNo;

    // Start is called before the first frame update
    void Start()
    {

    }

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

        GameManager.Instance.characterSelected = characterNo;
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
            // int characterPrefabIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            PhotonNetwork.Instantiate(characterPrefabs[GameManager.Instance.characterSelected].name, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            Instantiate(playerDisplayname);
            canvasCamera.enabled = true;
            miniMapCam.enabled = true;
            //Destroy(CanvasCamera);
            Destroy(gameObject);
        }
    }
}
