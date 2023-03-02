using Photon.Pun.Demo.Cockpit;
using System.Collections;
using TMPro;
using UnityEngine;
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

    IEnumerator SendToForum(string name,string email, string feedback)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.669787835", name);
        form.AddField("entry.2063508337", email);
        form.AddField("entry.1541678250", feedback);
        byte[] rawData = form.data;
        WWW send = new WWW(ForumURL, rawData);
        Debug.Log("Thank you for Playing");
        StartCoroutine(ExitApp());
        yield return send;
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
