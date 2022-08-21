using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;

public class MusicOnOffManager : MonoBehaviour
{
    public int index = 0;
    [SerializeField] List<Track> tracks;
    [SerializeField] int i = 0;
    [SerializeField] float time = 2;
    public void turnOn(int i, float time) { tracks[i].track.volumeTransition(tracks[i].volume, time); }
    public void turnOn(float time) { tracks[i].track.volumeTransition(tracks[i].volume, time); }
    public void turnOn() { tracks[i].track.volumeTransition(tracks[i].volume, time); }
    public void turnOn(int i) { tracks[i].track.volumeTransition(tracks[i].volume, time); }
    public void turnOff(int i, float time) { tracks[i].track.volumeTransition(0, time); }
    public void turnOff() { tracks[i].track.volumeTransition(0, time); }
    public void turnOff(int i) { tracks[i].track.volumeTransition(0, time); }

    public void turnAllOff(float time)
    {
        foreach (Track track in tracks)
        {
            track.track.volumeTransition(0, time);
        }
    }
    public void turnAllOff()
    {
        foreach (Track track in tracks)
        {
            track.track.volumeTransition(0, time);
        } 
    }
}

[System.Serializable]
public class Track
{
    public AudioSource track;
    public float volume = 0;
}
