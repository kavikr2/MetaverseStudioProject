using UnityEngine;
using TMPro;
using Photon.Pun;

public class SM_GetName : MonoBehaviour
{
    public PhotonView view;
    public TMP_Text m_Text;
    private void Start()
    {
        m_Text.SetText(view.Owner.NickName); 
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}
