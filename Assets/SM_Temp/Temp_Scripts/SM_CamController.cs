using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SM_CamController : MonoBehaviour
{
    
    Vector3 offset = Vector3.zero;
    
    public Transform target; //for read target position
    public float posX;
    public float posY;
    public float posZ;


    public float turnSpeed = 4.0f;
 
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("CamTarget");
        foreach(GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                this.target = player.transform;
                offset = new Vector3(target.position.x + posX, target.position.y + posY, target.position.z + posZ);
            }
           
        }
    }
    void LateUpdate()
    {
        //UpdatePosition();
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }

}
