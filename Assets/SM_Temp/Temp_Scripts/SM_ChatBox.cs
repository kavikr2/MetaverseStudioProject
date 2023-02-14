using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SM_ChatBox : MonoBehaviour
{
    public TMP_InputField textInput;
    public Text chatText;

    public Button SendTextBtn;

    // Start is called before the first frame update
    void Start()
    {
        SendTextBtn.onClick.AddListener(onSendTextBtn);
    }
    

    void onSendTextBtn()
    {
        chatText.text += "\n" + textInput.text;
        textInput.text = "";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            onSendTextBtn();
        }
    }
}
