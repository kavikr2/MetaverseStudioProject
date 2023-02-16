using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_CamController : MonoBehaviour
{
    //public Transform camTarget;
    //public float pLerp = 0.02f;
    //public float rLerp = 0.01f;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);
    //    transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rLerp);
    //}

    Vector3 offset = Vector3.zero;
    //Vector3 velocity = Vector3.zero;
    //public float distance = 10f;
    //float currentDistance;
    //float minDistance = 1f;
    //float maxDistance = 2f;
    //public float height = 8f;
    //public float targetHeadHeight = 7f;
    //float smooth = 0.3f;

    public Transform target; //for read target position
    public float posX;
    public float posY;
    public float posZ;


    public float turnSpeed = 4.0f;
    //public Transform player;

    

    void Awake()
    {
        //offset = new Vector3(target.position.x, (target.position.y + height), (target.position.z - distance));
        offset = new Vector3(target.position.x + posX, target.position.y + posY , target.position.z + posZ);
        //transform.position = offset;
    }
    //void Update()
    //{
    //    LookAtTarget();
    //}
    void LateUpdate()
    {
        //UpdatePosition();
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
    //void LookAtTarget()
    //{
    //    Vector3 relativePos = target.position - transform.position;
    //    Vector3 y = new Vector3(0, targetHeadHeight, 0);
    //    Quaternion newRotation = Quaternion.LookRotation(relativePos + y);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10f * Time.deltaTime);
    //}

    //void UpdatePosition()
    //{
    //    currentDistance = Vector3.Distance(transform.position, target.position);
    //    if (currentDistance < minDistance)
    //    {
    //        currentDistance = minDistance;
    //    }
    //    else if (currentDistance > maxDistance)
    //    {
    //        currentDistance = maxDistance;
    //    }
    //    distance = currentDistance;
    //    offset = new Vector3(target.position.x, (target.position.y + height), (target.position.z - distance));
    //    transform.position = Vector3.SmoothDamp(transform.position, offset, ref velocity, smooth);
    //}
}
