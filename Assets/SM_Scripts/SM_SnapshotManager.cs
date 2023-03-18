using UnityEngine;


public class SM_SnapshotManager : MonoBehaviour
{
    //Making in singleton
    public static SM_SnapshotManager snapManager;

    //All Cameras in Scene
    public SM_SnapshotCamera snapCam;
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
        //Setting Preview cam gameobject inactive in start
        SnapPreviewCamObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
