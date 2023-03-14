using Photon.Pun;
using UnityEngine;

public class SM_MinigamePortal : MonoBehaviour
{
    public SM_MinigameManager manager;
    public Minigames game;
    PhotonView _view;

    public void OnCollisionEnter(Collision collision)
    {
        _view = collision.gameObject.GetComponent<PhotonView>();
        if(_view.IsMine)
            manager.EnterMiniGame((int)game);
    }

}
public enum Minigames
{
    MathGame,
    QuizGame
}