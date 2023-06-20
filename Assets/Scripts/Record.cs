using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Record : MonoBehaviour
{
    private RecordPlayer player;
    [SerializeField] private AudioClip song;

    private void Start()
    {
        player = RecordPlayer.i;        
    }
    public void StartRecord()
    {
        player.speaker.clip = song;
        player.speaker.time = 0;
        player.speaker.Play();
    }
}
