using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;

public class SM_RoomVote : MonoBehaviour
{
    public GameObject likebtn;
    public Room Room;
    public SM_LeaderBoard leaderBoard;
    bool hallway = false; bool inRoom = false;
    void Start()
    {
        if(Room == Room.Hallway) { hallway = true; }
        AnimatedIconHandler animate = likebtn.GetComponent<AnimatedIconHandler>();
        animate.playType = AnimatedIconHandler.PlayType.MadhanMade;
        Button button = likebtn.GetComponent<Button>();
        button.onClick.AddListener(updateLeaderBoard);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            likebtn?.SetActive(!hallway);
            if(!hallway) { inRoom = true; }
        }
    }
    private void OnTriggerExit(Collider other){ inRoom = false; }

    public void updateLeaderBoard()
    {
        if (inRoom)
        {
            switch (Room)
            {
                case Room.Aviation:
                    leaderBoard.Aviation++;
                    break;
                case Room.Maritime:
                    leaderBoard.Maritime++;
                    break;
                case Room.Healthcare:
                    leaderBoard.Healthcare++;
                    break;
                case Room.Education:
                    leaderBoard.Education++;
                    break;
            }
        }
    }
}

    public enum Room
{
    Aviation,
    Maritime,
    Healthcare,
    Education,
    Hallway
}