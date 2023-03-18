using Michsky.UI.ModernUIPack;
using Photon.Pun;
using POpusCodec.Enums;
using System.Collections;
using UnityEngine;

public class SM_PlayerMovement : MonoBehaviour
{
    bool isCollideWithPointer = false;

    //MovementControls
    [Header("Movement Related")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    bool grounded;

    Animator animator;
    PhotonView myView;

    //Snapshots Notifications
    GameObject SnapshotNotificationObject;
    GameObject SnapSaveNotificationObject;

    //Arrows Paths Gameobjects
    GameObject MaritimeRoomPathObject;
    GameObject AviationRoomPathObject;
    GameObject HealthcareRoomPathObject;
    GameObject EducationRoomPathObject;

    //Notifications on Completing Arrows Paths
    GameObject MaritimeRoomNotificationObject;
    GameObject HealthcareRoomNotificationObject;
    GameObject AviationRoomNotificationObject;
    GameObject EducationRoomNotificationObject;

    //Check if entered room or not
    bool enteredMaritimeRoom = false;
    bool enteredAviationRoom = false;
    bool enteredHealthcareRoom = false;
    bool enteredEducationeRoom = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        myView = transform.GetComponent<PhotonView>();
        animator = GetComponentInChildren<Animator>();


        //Don't want to use find tag.. Will find good alternative for this
        MaritimeRoomPathObject = GameObject.FindGameObjectWithTag("MaritimeRoomPath");
        AviationRoomPathObject = GameObject.FindGameObjectWithTag("AviationRoomPath");
        HealthcareRoomPathObject = GameObject.FindGameObjectWithTag("HealthcareRoomPath");
        EducationRoomPathObject = GameObject.FindGameObjectWithTag("EducationRoomPath");

        MaritimeRoomNotificationObject = GameObject.FindGameObjectWithTag("RoomEnter1");
        AviationRoomNotificationObject = GameObject.FindGameObjectWithTag("RoomEnter2");
        HealthcareRoomNotificationObject = GameObject.FindGameObjectWithTag("RoomEnter3");
        EducationRoomNotificationObject = GameObject.FindGameObjectWithTag("RoomEnter4");

        SnapshotNotificationObject = GameObject.FindGameObjectWithTag("SnapShotNotification");
        SnapSaveNotificationObject = GameObject.FindGameObjectWithTag("SnapSaveNotification");

    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * Data.moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * Data.moveSpeed * 10f * Data.airMultiplier, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        //Actual player movement happens because of this
        if (myView.IsMine)
        {
            MovePlayer();
        }
    }
   
        
    
    void Update()
    {
        if (myView.IsMine)
        {
            //check ground
            grounded = Physics.Raycast(transform.position, Vector3.down, Data.playerHeight * 0.5f + 0.3f, whatIsGround);

            MyInput();
            SpeedControl();

            if (Input.GetKeyDown(KeyCode.Space) && isCollideWithPointer)
            {
                //Snapshot Camera Open, Notification Open, Sound Player, Main Camera Active
                SM_SnapshotManager.snapManager.snapCam.CallTakeSnapshot();
                SnapSaveNotificationObject.GetComponent<NotificationManager>().OpenNotification();
                SM_SnapshotManager.snapManager.snapCam.PlaySound();
                SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(false);
                SM_SnapshotManager.snapManager.MainCamera.SetActive(true);
                isCollideWithPointer = false;
            }

            //Animations
            //Movement Animations
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            //Wave Animation
            if (Input.GetKey(KeyCode.C))
            {
                animator.SetBool("isWaving", true);
            }
            else
            {
                animator.SetBool("isWaving", false);

            }


            //Formal Bow Animation
            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("isFormalBow", true);
            }
            else
            {
                animator.SetBool("isFormalBow", false);
            }

            //arrows set to false if entered all rooms
            if (enteredAviationRoom && enteredEducationeRoom && enteredHealthcareRoom && enteredMaritimeRoom)
            {
                MaritimeRoomPathObject.SetActive(false);
                AviationRoomPathObject.SetActive(false);
                HealthcareRoomPathObject.SetActive(false);
                EducationRoomPathObject.SetActive(false);
            }

        }

    }

    public void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > Data.moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * Data.moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // setting other arrows invisible on spawn
        if (other.CompareTag("Hallway") && myView.IsMine)
        {
            AviationRoomPathObject.SetActive(false);
            HealthcareRoomPathObject.SetActive(false);
            EducationRoomPathObject.SetActive(false);
        }
        
        //Enable Snapshot Camera and disabling Main Camera
        if (other.CompareTag("Pointer") && myView.IsMine)
        {
            isCollideWithPointer = true;
            SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(true);
            SM_SnapshotManager.snapManager.MainCamera.SetActive(false);
            SnapshotNotificationObject.GetComponent<NotificationManager>().OpenNotification();
        }
        
        //Arrows set to false for Maritime Room, Open Notification
        else if(other.CompareTag("MaritimeRoom") && myView.IsMine)
        {
            enteredMaritimeRoom = true;
            MaritimeRoomNotificationObject.GetComponent<NotificationManager>().OpenNotification();

            MaritimeRoomPathObject.SetActive(false);
            AviationRoomPathObject.SetActive(true);
            HealthcareRoomPathObject.SetActive(false);
            EducationRoomPathObject.SetActive(false);
        }

        //Arrows set to false for Aviation Room, Open Notification
        else if (other.CompareTag("AviationRoom") && myView.IsMine)
        {
            enteredAviationRoom = true;
            AviationRoomNotificationObject.GetComponent<NotificationManager>().OpenNotification();

            MaritimeRoomPathObject.SetActive(false);
            AviationRoomPathObject.SetActive(false);
            HealthcareRoomPathObject.SetActive(true);
            EducationRoomPathObject.SetActive(false);
        }

        //Arrows set to false for Healthcare Room, Open Notification
        else if (other.CompareTag("HealthcareRoom") && myView.IsMine)
        {
            enteredHealthcareRoom = true;
            HealthcareRoomNotificationObject.GetComponent<NotificationManager>().OpenNotification();

            MaritimeRoomPathObject.SetActive(false);
            AviationRoomPathObject.SetActive(false);
            HealthcareRoomPathObject.SetActive(false);
            EducationRoomPathObject.SetActive(true);
        }

        //Arrows set to false for Education Room, Open Notification
        else if (other.CompareTag("EducationRoom") && myView.IsMine)
        {
            enteredEducationeRoom = true;
            EducationRoomNotificationObject.GetComponent<NotificationManager>().OpenNotification();

            MaritimeRoomPathObject.SetActive(false);
            AviationRoomPathObject.SetActive(false);
            HealthcareRoomPathObject.SetActive(false);
            EducationRoomPathObject.SetActive(false);
        }
    }
    

   
}
