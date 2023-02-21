using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviourPunCallbacks
{
    public string roomName = "M_MetaverseScene";
    public GameObject playerview;

    private void Start()
    {
        StartCoroutine(LoadPlayer());
    }

    IEnumerator LoadPlayer()
    {
        if (!GameManager.Instance.connectedToServer)
        {
            yield return null;
        }
        for (int i = 0; i < PhotonNetwork.CountOfPlayers; i++)
        {
            GameObject edit = Instantiate(playerview, Vector3.zero, Quaternion.identity, GameObject.Find("playerscontent").transform);
            edit.GetComponent<SM_Playerview>();
            edit.name = GameManager.Instance.playerName;
            Debug.Log("Player nickname: " + GameManager.Instance.playerName);
        }
    }
}

