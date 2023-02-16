using UnityEngine;

public class SM_CamController : MonoBehaviour
{
    Vector3 offset = Vector3.zero;
    public Transform target; //for read target position
    public float posX = -2;
    public float posY = -1;
    public float posZ = -15;

    public float turnSpeed = 4.0f;

    void Awake()
    {
        
    }
    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerTarget");
        foreach (GameObject player in players)
        {
            this.target = player.transform;
            offset = new Vector3(target.position.x + posX, target.position.y + posY, target.position.z + posZ);
        }
    }
    private void Update()
    {

    }
    void LateUpdate()
    {

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);

    }
    //private Vector3 offset;
    ////-----Publics variables-----\\
    //[Header("Variables")]
    //public Transform player;
    //[Space]
    //[Header("Position")]
    //public float camPosX;
    //public float camPosY;
    //public float camPosZ;
    //[Space]
    //[Header("Rotation")]
    //public float camRotationX;
    //public float camRotationY;
    //public float camRotationZ;
    //[Space]
    //[Range(0f, 10f)]
    //public float turnSpeed;
    ////-----Privates functions-----\\
    //private void Start()
    //{
    //    offset = new Vector3(player.position.x + camPosX, player.position.y + camPosY, player.position.z + camPosZ);
    //    transform.rotation = Quaternion.Euler(camRotationX, camRotationY, camRotationZ);
    //}
    //private void Update()
    //{
    //    offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
    //    transform.position = player.position + offset;
    //    transform.LookAt(player.position);
    //}
}
