using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerList : MonoBehaviourPunCallbacks
{
    public string roomName = "M_MetaverseScene";
    public GameObject playerview;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
            foreach (Photon.Realtime.Player player in players)
            {
                GameObject room = Instantiate(playerview,Vector3.zero, Quaternion.identity, GameObject.Find("playerscontent").transform);
                room.GetComponent<SM_Playerview>().name.text = player.NickName;
                Debug.Log("Player nickname: " + player.NickName);
            }
        }
        else
        {
            Debug.Log("Log in through LoadingScene for playerlist to work");
        }
    }
}

