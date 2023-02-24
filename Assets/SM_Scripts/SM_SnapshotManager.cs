using UnityEngine;


public class SM_SnapshotManager : MonoBehaviour
{
    public static SM_SnapshotManager snapManager;

    public SM_SnapshotCamera snapCam;
    //public GameObject SnapshotPreviewObject;
    public GameObject SnapPreviewCamObject;
    public GameObject MainCamera;

    private void Awake()
    {
        if (snapManager == null)
        {
            snapManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        SnapPreviewCamObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
