using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_RoomVote : MonoBehaviour
{
    public GameObject likebtn;
    public bool hallway = false;
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        likebtn?.SetActive(!hallway);
    }
}
