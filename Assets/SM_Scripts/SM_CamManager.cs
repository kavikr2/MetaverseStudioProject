using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_CamManager : MonoBehaviour
{
#region instance
    public static SM_CamManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
#endregion

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("CamTarget");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                this.target = player.transform;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
