using UnityEngine;

public class SM_LikeButton : MonoBehaviour
{
    public GameObject button;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(button != null)
                button.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject) {
                Debug.Log("liked");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (button != null)
            button.SetActive(false);
    }
}
