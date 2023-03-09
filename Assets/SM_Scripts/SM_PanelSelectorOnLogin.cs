using Photon.Pun;
using System.Collections;
using UnityEngine;

public class SM_PanelSelectorOnLogin : MonoBehaviour
{
    public SM_CamController canvasCamera;
    public GameObject ClientCharacterSelectorPanel;
    public GameObject StaffCharacterSelectorPanel;
    // Start is called before the first frame update
    void Start()
    {
        ClientCharacterSelectorPanel.SetActive(false);
        StaffCharacterSelectorPanel.SetActive(false);
        if (GameManager.Instance.firstTime)
        {
            if (GameManager.Instance.isClientLogin)
            {
                ClientCharacterSelectorPanel.SetActive(true);
                StaffCharacterSelectorPanel.SetActive(false);
            }
            else
            {
                StaffCharacterSelectorPanel.SetActive(true);
                ClientCharacterSelectorPanel.SetActive(false);
            }
        }
        else
        {
            StartCoroutine(canvasControl());
        }
    }

    IEnumerator canvasControl()
    {
        while (!GameManager.Instance.firstTime)
        {
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        Debug.Log("tried");
        Debug.Log(PhotonNetwork.CurrentRoom);

        Vector3 pu = new Vector3(0.39f, 2.97f, 12.2f);
        GameObject pp = PhotonNetwork.Instantiate(GameManager.Instance.characterSelected, pu, Quaternion.identity);
        GameManager.Instance.view = pp.GetComponent<PhotonView>();
        canvasCamera.target = GameManager.Instance.playerpos; canvasCamera.enabled = true;
        Debug.Log(pp.name);
    }
}
