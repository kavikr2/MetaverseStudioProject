using UnityEngine;

public class SM_SnapTag : MonoBehaviour {
    public string tag = "NameHolder";
    void Start()
    {
        GameObject player = GameObject.FindWithTag(tag);
        transform.SetParent(player.transform, false);
    }
}
