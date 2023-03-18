using Photon.Pun;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class SM_SnapshotCamera : MonoBehaviour
{
    public AudioSource SnapshotSoundAudioSource;
    Camera snapCam;

    private void Awake()
    {
        snapCam = GetComponent<Camera>();
        if (snapCam.targetTexture == null)
        {
            snapCam.targetTexture = new RenderTexture(Data.resWidth, Data.resHeight, 24);
        }
        else
        {
            Data.resWidth = snapCam.targetTexture.width;
            Data.resHeight = snapCam.targetTexture.height;
        }
        snapCam.gameObject.SetActive(false);
    }

    public void CallTakeSnapshot()
    {
         snapCam.gameObject.SetActive(true);
    }
    public void PlaySound()
    {
        SnapshotSoundAudioSource.Play();
    }

    void LateUpdate()
    {
            if (snapCam.gameObject.activeInHierarchy)
            {
                Texture2D snapshot = new Texture2D(Data.resWidth, Data.resHeight, TextureFormat.RGB24, false);
                snapCam.Render();
                RenderTexture.active = snapCam.targetTexture;
                snapshot.ReadPixels(new Rect(0, 0, Data.resWidth, Data.resHeight), 0, 0);
                byte[] bytes = snapshot.EncodeToPNG();
                string fileName = SnapshotName();
                System.IO.File.WriteAllBytes(fileName, bytes);
                Debug.Log("Snapshot Taken!");
                snapCam.gameObject.SetActive(false);
            }
    }

    string SnapshotName()
    {
        return string.Format("{0}/Snapshots/snap_{1}X{2}_{3}.png",
           Application.streamingAssetsPath,
           Data.resWidth,
           Data.resHeight,
           System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

    }
}
