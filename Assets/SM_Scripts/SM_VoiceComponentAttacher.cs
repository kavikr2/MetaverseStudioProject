using Photon.Voice.PUN;
using Photon.Voice.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SM_VoiceComponentAttacher : MonoBehaviour
{
    public PunVoiceClient playerSpeaker;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //transform.SetParent(player.transform, false);
        playerSpeaker.GetComponent<PunVoiceClient>().SpeakerPrefab = player;
        Debug.Log(playerSpeaker.SpeakerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
