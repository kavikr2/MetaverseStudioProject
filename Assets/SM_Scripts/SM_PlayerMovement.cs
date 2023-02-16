using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_PlayerMovement : MonoBehaviour
{
    public SM_SnapshotCamera snapCam;

    float speed = 0f;
    Animator animator;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Animations
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.D))
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            snapCam.CallTakeSnapshot();
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
