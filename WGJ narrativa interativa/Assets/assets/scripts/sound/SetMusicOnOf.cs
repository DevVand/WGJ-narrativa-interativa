using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class SetMusicOnOf : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] float volume = 1;
    [SerializeField] float time = 2;
    void Start()
    {
    }

    public void setVolume(float to) { volume = to; }
    public void setTime(float to) { volume = to; }
    public void setTrack(float to, float time) { source.volumeTransition(to, time); }
    public void setTrack() { source.volumeTransition(volume, time); }
    
}
