using Photon.Pun;
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
        if (GameManager.Instance.FirstTime)
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
            // canvasCamera.target = GameManager.Instance.playerpos; canvasCamera.enabled = true;
        }
    }
}
