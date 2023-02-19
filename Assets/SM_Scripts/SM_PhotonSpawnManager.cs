using UnityEngine;
using Photon.Pun;
using System.Collections;

public class SM_PhotonSpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject[] characterPrefabs; 
    [SerializeField] private Transform[] spawnPoints; 

    public void Start()
    {
        if (GameManager.Instance.connectedToServer)
        {
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            // int characterPrefabIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

            PhotonNetwork.Instantiate(characterPrefabs[GameManager.Instance.characterSelected].name, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
        }
    }
}
