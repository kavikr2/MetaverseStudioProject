using UnityEngine;
using TMPro;
using Photon.Pun;

public class SM_GetName : MonoBehaviour
{
    public PhotonView view;
    public TMP_Text m_Text;
    public SM_LazyBool m_LazyBool;
    private void Start()
    {
        m_Text.SetText(view.Owner.NickName); 
    }

    void LateUpdate()
    {
        if (m_LazyBool.lazyBool)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                Camera.main.transform.rotation * Vector3.up);
        }
    }
}
