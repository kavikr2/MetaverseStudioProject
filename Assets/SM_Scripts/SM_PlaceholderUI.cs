using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;

public class SM_PlaceholderUI : MonoBehaviour
{
    public NotificationManager SnapshotNotification;
    public GameObject snapCamPreview;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SnapshotNotification.OpenNotification();
            gameObject.SetActive(false);
            snapCamPreview.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
