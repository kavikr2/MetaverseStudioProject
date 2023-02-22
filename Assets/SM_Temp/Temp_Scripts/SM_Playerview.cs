using UnityEngine;
using UnityEngine.UI;
public class SM_Playerview : MonoBehaviour
{
    public Text name;
    public GameObject gameObject;
    private void Start()
    {
        name.text = gameObject.name;
    }
}
