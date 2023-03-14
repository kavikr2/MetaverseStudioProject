using Michsky.UI.ModernUIPack;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SM_RoomVote : MonoBehaviour
{

    public GameObject likebtn;
    public Room Room;
    public SM_LeaderBoard leaderBoard;
    PhotonView myview;
    bool hallway = false; bool inRoom = false;

    void Start()
    {
        AnimatedIconHandler animate = likebtn.GetComponent<AnimatedIconHandler>();
        animate.playType = AnimatedIconHandler.PlayType.MadhanMade;
        Button button = likebtn.GetComponent<Button>();
        button.onClick.AddListener(updateLeaderBoard);
        if (Room == Room.Hallway) { hallway = true; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myview = other.GetComponent<PhotonView>();
            if (myview.IsMine)
            {
                likebtn?.SetActive(!hallway);
                if (!hallway) { inRoom = true; }
            }

        }
    }
    private void OnTriggerExit(Collider other){ inRoom = false; }

    public void updateLeaderBoard()
    {
        if (inRoom)
        {
            leaderBoard.EnditPls();
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