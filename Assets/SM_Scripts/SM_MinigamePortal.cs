using UnityEngine;

public class SM_MinigamePortal : MonoBehaviour
{
    public bool Metaverse = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(Metaverse)
        {
            GameManager.Instance.playerpos = collision.gameObject.transform;
            GameManager.Instance.FirstTime = false;
            GameManager.Instance.SceneChanger(Scenes.SM_Minigame1);
            Metaverse= true;
        }
    }

    public void EnterMetaverseBack()
    {
        GameManager.Instance.EnterMetaverse(false);
    }
}
