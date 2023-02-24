using UnityEngine;

public class SM_CharacterRotation : MonoBehaviour
{
    float rotSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up* rotSpeed * Time.deltaTime);
    }
}
