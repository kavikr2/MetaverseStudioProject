using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Michsky.UI.ModernUIPack;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using SimpleJSON;

public class SM_ServerManager : MonoBehaviour
{
    #region Instance 
    public static SM_ServerManager Serverconnect { get; private set; }

    private void Awake()
    {
        if (Serverconnect != null && Serverconnect != this)
        {
            Destroy(this);
        }
        else
        {
            Serverconnect = this;
        }
    }
    #endregion

    [Header("Login Page")]
    public TMP_InputField loginUser;
    public TMP_InputField loginPass;
    public TMP_InputField nicknameField;
    public Button nicknameButton;
    public Button playButton;

    [Header("Register Page")]
    public TMP_InputField RegUser;
    public TMP_InputField RegEmail;
    public TMP_InputField RegPass;

    [Header("Notifications")]
    public ModalWindowManager LoggedScreen;
    public NotificationManager SuccessNotify;
    public NotificationManager FailureNotify;
    public NotificationManager UsernameNotify;

    public GameObject RoomListContent;
    public GameObject RoomListEntryPrefab;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;

    AccountTable Accounts;
    string JsonDepreciatedURL = "https://drive.google.com/uc?export=download&id=10YFKvBGL4OWGMTzXH2YfdV-c5xpPWK_E";
    string AccountsURL = "https://sheets.googleapis.com/v4/spreadsheets/1fPiZE0_L-vgQytUzXH9tw_jOp_zQxYMD6joIIA8Yens/values/Sheet1?key=AIzaSyDcGDo4WpnQFPvbwTxuETtR-jHWlhHLmCE";

    public void Start()
    {
        SetInteractablility();

        if (PhotonNetwork.InRoom)
        {
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

            for (int i = 0; i < players.Length; i++)
            {
                Debug.Log(players[i].NickName);
            }
        }
    }

    #region GDrive Tools

    // Download image file
    void GetTexture(string url)
    {
        StartCoroutine(Texture(url));
    }

    // Download JSON file
    void GetJson(string url)
    {
        StartCoroutine(JSON(url));
    }

    // Download audio file
    void GetAudio(string url)
    {
        StartCoroutine(AudioClip(url));
    }

    public void Login(){
        StartCoroutine(LoginCheck());
    }

    IEnumerator JSON(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            string json = webRequest.downloadHandler.text;
            // Do something with the JSON data
            Debug.Log(json);
        }
    }

    IEnumerator Texture(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            // Do something with the texture
            Debug.Log(texture.name);
        }
    }

    IEnumerator AudioClip(string url)
    {
        UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(webRequest);
            // Do something with the audio clip
            Debug.Log(audioClip.name);
        }
    }

    #endregion

    IEnumerator LoginCheck()
    {
        UnityWebRequest request = UnityWebRequest.Get(AccountsURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            JSONNode data = SimpleJSON.JSON.Parse(json);
            bool bruh = false;

            List<string> columns = new List<string>();

            foreach (JSONNode value in data["values"][0].AsArray)
            {
                string columnName = value.Value;
                columns.Add(columnName);
            }

            List<Dictionary<string, string>> rowsData = new List<Dictionary<string, string>>();

            for (int i = 1; i < data["values"].Count; i++)
            {
                JSONNode row = data["values"][i];
                Dictionary<string, string> rowData = new Dictionary<string, string>();

                for (int j = 0; j < row.Count; j++)
                {
                    string columnName = columns[j];
                    string value = row[j].Value;
                    rowData[columnName] = value;
                }

                rowsData.Add(rowData);
            }

            foreach (Dictionary<string, string> rowData in rowsData)
            {
                string name = rowData["Name"];
                string email = rowData["Email"];
                string password = rowData["Password"];

                if (loginUser.text == email && loginPass.text == password)
                {
                    SuccessNotify.OpenNotification();

                    if (GameManager.Instance != null)
                        GameManager.Instance.SetName(name);

                    SceneChanger(false);
                    bruh = true;
                }
            }
            if (!bruh) { FailureNotify.OpenNotification(); }
        }
    }

    public void SetInteractablility()
    {
        nicknameButton.interactable = false;

        nicknameField.onValueChanged.AddListener(delegate
        {
            nicknameButton.interactable = !string.IsNullOrEmpty(nicknameField.text);

        });
    }

    public void SetNickName() => GameManager.Instance.SetName(nicknameField.text);

    public void Quit() => Application.Quit();

    public void SceneChanger(bool tt)
    {
        GameManager.Instance.EnterMetaverse(true);
        GameManager.Instance.isClientLogin = tt;
    }
}

public struct AccountTable
{
    public string Name;
    public string Email;
    public string Password;
}

