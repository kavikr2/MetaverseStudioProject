using UnityEngine;
using Photon.Pun;
using static UnityEngine.GraphicsBuffer;

public class MiniCamFollow : MonoBehaviour
{
   
    public Transform Player;
    public Vector3 offset;
    public float posX;
    public float posY;
    public float posZ;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("CamTarget");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                this.Player = player.transform;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + offset;
        Vector3 rot = new Vector3(90, Player.eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(rot);
    }
}
