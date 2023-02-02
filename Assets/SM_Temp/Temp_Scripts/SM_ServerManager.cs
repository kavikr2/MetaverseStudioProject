using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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
    public InputField LoginUser;
    public InputField LoginPass;

    [Header("Register Page")]
    public InputField RegUser;
    public InputField RegEmail;
    public InputField RegPass;

    AccountTable Accounts;
    string AccountsURL = "https://drive.google.com/uc?export=download&id=10YFKvBGL4OWGMTzXH2YfdV-c5xpPWK_E";

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
            Accounts = JsonUtility.FromJson<AccountTable>(request.downloadHandler.text);
            
            if(LoginUser.text == Accounts.Name && LoginPass.text == Accounts.Password)
            {
                Debug.Log("Success");
            } 
            else { Debug.Log("Login works but you are dumb"); }

        }
    }

}

public struct AccountTable
{
    public string Name;
    public string Email;
    public string Password;
}