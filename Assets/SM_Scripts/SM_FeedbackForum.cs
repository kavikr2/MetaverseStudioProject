using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SM_FeedbackForum : MonoBehaviour
{
    public TextMeshProUGUI NameField;
    public TextMeshProUGUI EmailField;
    public TMP_InputField FeedbackField;

    public Button SubmitBtn;

    [SerializeField] private string ForumURL = "https://docs.google.com/forms/u/1/d/e/1FAIpQLScifyse3-0BEr8JyTsuxW32rv7DG6smESZk6jc4qTtsuE6L9w/formResponse";

    public void Submit()
    {
        SetInteractablility();
        StartCoroutine(SendToForum(NameField.text, EmailField.text, FeedbackField.text));
    }
    public void ExitAnywayBtn() => StartCoroutine(ExitApp());

    IEnumerator SendToForum(string name,string email, string feedback)
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("entry.669787835", name),
            new MultipartFormDataSection("entry.2063508337", email),
            new MultipartFormDataSection("entry.1541678250", feedback)
        };

        UnityWebRequest www = UnityWebRequest.Post(ForumURL, formData);
        StartCoroutine(ExitApp());
        yield return www.SendWebRequest();
    }

    public void SetInteractablility()
    {
        SubmitBtn.interactable = false;

        FeedbackField.onValueChanged.AddListener(delegate
        {
            SubmitBtn.interactable = !string.IsNullOrEmpty(FeedbackField.text);

        });
    }

    IEnumerator ExitApp()
    {
        yield return new WaitForSeconds(2);
        Application.Quit();
    }
}
