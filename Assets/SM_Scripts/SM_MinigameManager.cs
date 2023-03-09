using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_MinigameManager : MonoBehaviour
{
    [Header("Minigames")]
    public GameObject[] Minigame;
    public GameObject[] MinigameExitBtn;

    [Header("PlayerData")]
    public GameObject Player;
    public GameObject PlayerCamera;
    public GameObject MainEventSystem;
    public GameObject MiniMap;

    public void EnterMiniGame(int game)
    {
        MinigameExitBtn[game].SetActive(true); Minigame[game].SetActive(true);
        StartCoroutine(SetPlayerData(false));
    }

    public void EndMinigame(int game)
    {
        MinigameExitBtn[game].SetActive(false); Minigame[game].SetActive(false);
        StartCoroutine(SetPlayerData(true));
    }

    IEnumerator SetPlayerData(bool active)
    {
        MainEventSystem.SetActive(active); MiniMap.SetActive(active);
        //if (active)
        //    yield return new WaitForSeconds(5f);

        PlayerCamera.SetActive(active); Player.GetComponent<SM_PlayerMovement>().enabled = active; Player.GetComponent<SM_LazyBool>().lazyBool = active;
        yield return null;
    }
}
