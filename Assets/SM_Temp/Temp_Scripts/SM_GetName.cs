using UnityEngine;
using TMPro;

public class SM_GetName : MonoBehaviour
{
    public TMP_Text m_Text;
    private void Start()
    {
        m_Text.SetText(GameManager.Instance.playerName);
    }
}
