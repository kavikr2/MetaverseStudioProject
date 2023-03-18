using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_MinigameManager : MonoBehaviour
{
    [Header("Minigames")]
    public GameObject[] MinigameObjects;
    public GameObject[] MinigameExitBtnObjects;

    [Header("PlayerData")]
    public GameObject PlayerObject;
    public GameObject PlayerCameraObject;
    public GameObject PlayerCanvasObject;
    public GameObject MainCameraObject;
    public GameObject MainEventSystemObject;
    public GameObject MiniMapObject;

    public void EnterMiniGame(int game)
    {
        MinigameExitBtnObjects[game].SetActive(true); MinigameObjects[game].SetActive(true);
        StartCoroutine(SetPlayerData(false));
    }

    public void EndMinigame(int game)
    {
        MinigameExitBtnObjects[game].SetActive(false); MinigameObjects[game].SetActive(false);
        StartCoroutine(SetPlayerData(true));
    }

    IEnumerator SetPlayerData(bool active)
    {
        MainEventSystemObject.SetActive(active); MiniMapObject.SetActive(active);

        PlayerCameraObject.SetActive(active); PlayerCanvasObject.SetActive(active); MainCameraObject.SetActive(active);
        PlayerObject.GetComponent<SM_PlayerMovement>().enabled = active; PlayerObject.GetComponentInChildren<SM_GetName>().enabled = active;
        yield return null;
    }
}
