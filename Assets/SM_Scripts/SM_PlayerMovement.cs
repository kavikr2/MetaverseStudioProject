using Michsky.UI.ModernUIPack;
using Photon.Pun;
using POpusCodec.Enums;
using UnityEngine;

public class SM_PlayerMovement : MonoBehaviour
{
    bool isCollideWithPointer = false;
    public float moveSpeed;
    [HideInInspector] public float walkSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    public float airMultiplier;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;


    float speed;
    Animator animator;



    //GameObject snapCam;
    //GameObject snapCamPreview;

    NotificationManager SnapshotNotification;
    PhotonView myView;

    void Start()
    {


        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        SnapshotNotification = FindObjectOfType<NotificationManager>();
        myView = transform.GetComponent<PhotonView>();
        //snapCam = GameObject.FindGameObjectWithTag("SnapshotCam");
        //snapCamPreview = GameObject.FindGameObjectWithTag("SnapshotPreview");

        animator = GetComponentInChildren<Animator>();
    }
    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        if (myView.IsMine)
        {

            MovePlayer();
        }
    }
    void Update()
    {


        if (myView.IsMine)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

            MyInput();
            SpeedControl();

            if (Input.GetKeyDown(KeyCode.Space) && isCollideWithPointer)
            {

                //SM_SnapshotManager.snapManager.snapCam.CallTakeSnapshot();
                SM_SnapshotManager.snapManager.snapCam.PlaySound();
                SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(false);
                SM_SnapshotManager.snapManager.MainCamera.SetActive(true);
                isCollideWithPointer = false;
            }

            //Animations
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            if (Input.GetKey(KeyCode.C))
            {
                animator.SetBool("isWaving", true);
            }
            else
            {
                animator.SetBool("isWaving", false);

            }

            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetBool("isFormalBow", true);
            }
            else
            {
                animator.SetBool("isFormalBow", false);
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
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pointer") && myView.IsMine)
        {
            isCollideWithPointer = true;
            SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(true);
            SM_SnapshotManager.snapManager.MainCamera.SetActive(false);
            SnapshotNotification.OpenNotification();
        }
        
    }

   
}
