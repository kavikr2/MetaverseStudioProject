using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SM_ChatFieldEnabler : MonoBehaviour
{
    public bool chatFieldEnabled;
    public Button chatFieldButton;
    public GameObject chatFieldObject;

    int no;
    // Start is called before the first frame update
    void Start()
    {
        chatFieldEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        chatFieldButton.onClick.AddListener(onButtonClick);

        if (no % 2 == 0)
        {
            chatFieldEnabled = false;
        }
        else
        {
            chatFieldEnabled = true;
        }


        if (chatFieldEnabled)
        {
            chatFieldObject.SetActive(true);
        }
        else
        {
            chatFieldObject.SetActive(false);
        }
    }

    void onButtonClick()
    {
        no++;
    }
}
