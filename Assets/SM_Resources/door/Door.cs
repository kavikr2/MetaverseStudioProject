using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen", false);
    }
}
