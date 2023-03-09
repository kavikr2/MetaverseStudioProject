using UnityEngine;

public class SM_MinigamePortal : MonoBehaviour
{
    public SM_MinigameManager manager;
    public Minigames game;

    public void OnCollisionEnter(Collision collision)
    {
        manager.EnterMiniGame((int)game);
    }

}
public enum Minigames
{
    MathGame,
    QuizGame
}