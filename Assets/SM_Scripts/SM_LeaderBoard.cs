using TMPro;
using UnityEngine;

public class SM_LeaderBoard : MonoBehaviour
{
    public int Aviation,
    Maritime,
    Healthcare,
    Education;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        SetText();
    }

    public void SetText()
    {
        text.SetText("Aviation :" + Aviation.ToString() + "<br>" + "Maritime :" + Maritime.ToString() + "<br>" + "Healthcare :" + Healthcare.ToString() + "<br>" + "Education :" + Education.ToString() + "<br>");
    }
}
