using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;

public class SM_PlaceholderUI : MonoBehaviour
{
    public NotificationManager SnapshotNotification;
    public GameObject snapCamPreview;
    PhotonView myView;
    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myView.IsMine)
        {
            if (other.CompareTag("Player"))
            {
                SnapshotNotification.OpenNotification();
                gameObject.SetActive(false);
                snapCamPreview.SetActive(true);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
