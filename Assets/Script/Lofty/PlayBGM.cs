using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public SoundManager.SoundName soundName;
    private SoundManager soundManager;
    [SerializeField] private bool isPlay;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        if (isPlay)
        {
            SoundManager.instace.Play(soundName);
        }
        
    }

    public void ChangeSong(SoundManager.SoundName sound)
    {
        SoundManager.instace.Stop(soundName);
        SoundManager.instace.Play(sound);
        soundName = sound;
    }
    
    
}
