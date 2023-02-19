using Michsky.UI.ModernUIPack;
using UnityEngine;

public class SM_StaffCharSelection : MonoBehaviour
{
    public HorizontalSelector mySelector;
    public GameObject Staff1Object;
    public GameObject Staff2Object;

    public GameObject StaffChar1Object;
    public GameObject StaffChar2Object;

    public GameObject CharacterSelectionPanelObject;
    public SM_CamController camController;

    int characterNo;


    public void SelectCharacter()
    {
        switch (characterNo)
        {
            case 0:
                Staff1Object.SetActive(true);
                Staff2Object.SetActive(false);

                StaffChar1Object.SetActive(true);
                StaffChar2Object.SetActive(false);
                break;

                case 1:
                Staff2Object.SetActive(true);
                Staff1Object.SetActive(false);

                StaffChar2Object.SetActive(true);
                StaffChar1Object.SetActive(false);
                break ;

        }
        camController.enabled = true;
        CharacterSelectionPanelObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        camController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        characterNo = mySelector.index;
        switch (characterNo)
        {
            case 0:
                StaffChar1Object.SetActive(true);
                StaffChar2Object.SetActive(false);
                break;

            case 1:
                StaffChar2Object.SetActive(true);
                StaffChar1Object.SetActive(false);
                break;
        }
    }
}
