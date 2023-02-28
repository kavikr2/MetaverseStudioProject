using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviourPunCallbacks
{
    public string roomName = "M_MetaverseScene";
    public GameObject playerview;
    GameObject playerscontent;

    //int playerCount = 0;
    private void Start()
    { playerscontent = GameObject.Find("playerscontent"); ButtonStart(); }
    private void Update()
    {
        //if(playerCount != PhotonNetwork.CountOfPlayers)
        //{
        //    playerCount = PhotonNetwork.CountOfPlayers;

        //}
    }

    public void ButtonStart()
    {
        StartCoroutine(LoadPlayer());
    }

    void CleanList()
    {
        for (var i = playerscontent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(playerscontent.transform.GetChild(i));
        }
    }
    IEnumerator LoadPlayer()
    {
        while(!PhotonNetwork.InLobby)
        {
            yield return null;
        }
        //Debug.Log(PhotonNetwork.CountOfPlayers);
        //Debug.Log(PhotonNetwork.PlayerList);
        //Debug.Log(PhotonNetwork.PlayerListOthers);
        CleanList();
        for (int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            GameObject edit = Instantiate(playerview, playerscontent.transform);
            edit.GetComponent<SM_Playerview>();
            edit.name = GameManager.Instance.playerName;
            //Debug.Log("Player nickname: " + GameManager.Instance.playerName);
        }
    }
}

