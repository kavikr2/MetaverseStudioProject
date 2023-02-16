using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Michsky.UI;

public class SM_CharacterSelection : MonoBehaviour
{
    public HorizontalSelector mySelector;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

   

    //UI Part
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
   
    public GameObject CharacterSelectionPanel;
    public SM_CamController CamController;


    int characterNo;
    // Start is called before the first frame update
    void Start()
    {
        CamController.enabled = false;
    }

    public void SelectCharacter()
    {
        switch (characterNo)
        {
            case 0:
                Player1.SetActive(true);
                Player2.SetActive(false);
                Player3.SetActive(false);

                Character1.SetActive(true);
                Character2.SetActive(false);
                Character3.SetActive(false);
                break;

            case 1:
                Player2.SetActive(true);
                Player1.SetActive(false);
                Player3.SetActive(false);

                Character2.SetActive(true);
                Character1.SetActive(false);
                Character3.SetActive(false);
                break;

            case 2:
                Player3.SetActive(true);
                Player1.SetActive(false);
                Player2.SetActive(false);

                Character3.SetActive(true);
                Character1.SetActive(false);
                Character2.SetActive(false);
                break;
        }

        CharacterSelectionPanel.SetActive(false);
        CamController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        characterNo = mySelector.index;

        switch (characterNo)
        {
            case 0:
                Character1.SetActive(true);
                Character2.SetActive(false);
                Character3.SetActive(false);
                break;

            case 1:
                Character2.SetActive(true);
                Character1.SetActive(false);
                Character3.SetActive(false);
                break;

            case 2:
                Character3.SetActive(true);
                Character1.SetActive(false);
                Character2.SetActive(false);
                break;
        }
    }
}
