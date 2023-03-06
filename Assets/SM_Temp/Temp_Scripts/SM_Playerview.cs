using UnityEngine;
using UnityEngine.UI;
public class SM_Playerview : MonoBehaviour
{
    public new Text name;
    public new GameObject gameObject;
    private void Start()
    {
        name.text = gameObject.name;
    }
}
