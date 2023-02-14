using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_PanelSelectorOnLogin : MonoBehaviour
{
    public GameObject ClientCharacterSelectorPanel;
    public GameObject StaffCharacterSelectorPanel;
    // Start is called before the first frame update
    void Start()
    {
        ClientCharacterSelectorPanel.SetActive(false);
        StaffCharacterSelectorPanel.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
