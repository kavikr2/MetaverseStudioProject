using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class UIDemo : MonoBehaviour
{
    public NotificationManager SnapshotNotification;
    public GameObject SnapCamPreview;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SnapshotNotification.OpenNotification();
            SnapCamPreview.SetActive(true);
            gameObject.SetActive(false);
        }
      
    }
    private void OnTriggerExit(Collider other)
    {
            gameObject.SetActive(true);
            SnapCamPreview.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
