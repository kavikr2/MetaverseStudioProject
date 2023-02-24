using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Michsky.UI.ModernUIPack;

public class SM_PlayerMovement : MonoBehaviour
{
    float speed = 0f;
    Animator animator;
    Rigidbody rb;

    //GameObject snapCam;
    //GameObject snapCamPreview;

    NotificationManager SnapshotNotification;
    PhotonView myView;
    
    void Start()
    {
        SnapshotNotification = FindObjectOfType<NotificationManager>();
        myView = transform.GetComponent<PhotonView>();
        //snapCam = GameObject.FindGameObjectWithTag("SnapshotCam");
        //snapCamPreview = GameObject.FindGameObjectWithTag("SnapshotPreview");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (myView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SM_SnapshotManager.snapManager.snapCam.CallTakeSnapshot();
                SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(false);
                SM_SnapshotManager.snapManager.MainCamera.SetActive(true);
            }

            //Animations
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
            


            //Movement 
            if (speed > 0) { speed -= 0.1f; }
            if (speed < 0) { speed += 0.1f; }
            if (Input.GetKey(KeyCode.W) && speed < 4)
            {
                speed += 0.5f;
            }
            if (Input.GetKey(KeyCode.S) && speed < -2) { speed -= 0.5f; }

            if (Input.GetKey(KeyCode.A)) { transform.Rotate(0, -2, 0); }
            if (Input.GetKey(KeyCode.D)) { transform.Rotate(0, 2, 0); }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pointer") && myView.IsMine)
        {
            SM_SnapshotManager.snapManager.SnapPreviewCamObject.SetActive(true);
            SM_SnapshotManager.snapManager.MainCamera.SetActive(false);
            SnapshotNotification.OpenNotification();
        }
    }

}
