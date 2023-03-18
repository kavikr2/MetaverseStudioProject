using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SM_ChatManager : MonoBehaviour, IChatClientListener
{
    [SerializeField]
    GameObject joinChatButtonObject;

    ChatClient chatClient;
    bool isConnected;

    [SerializeField]
    string username;

    [SerializeField] GameObject chatPanel;
    string privateReceiver = "";
    string currentChat;
    [SerializeField] TMP_InputField chatField;
    [SerializeField] Text chatDisplay;
    
    
    public void UsernameOnValueChange(string valueIn)
    {
        username = PhotonNetwork.NickName;
    }

    public void ChatConnectOnClick()
    {
        
    }
    
    public void DebugReturn(DebugLevel level, string message)
    {
        
    }

    public void OnChatStateChange(ChatState state)
    {
 
    }

    public void OnConnected()
    {
        joinChatButtonObject.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for(int i = 0; i<senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);
            chatDisplay.text += "\n " + msgs;
            Debug.Log(msgs);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
       
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
        
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }

    public void SubmitPublicChatOnClick()
    {
        if(privateReceiver == "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }
    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    // Start is called before the first frame update
    void Start()
    {
        username = PhotonNetwork.NickName;
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connecting!");
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            chatClient.Service();
        }

        if(chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }
    }
}
