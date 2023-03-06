using UnityEngine;
using TMPro;
using Photon.Pun;

public class SM_GetName : MonoBehaviour
{
    public PhotonView view;
    public TMP_Text m_Text;
    private void Start()
    {
        view.RPC("GetName", RpcTarget.All);
    }

    [PunRPC]
    void GetName()
    {
        m_Text.SetText(GameManager.Instance.playerName);
    }
}
