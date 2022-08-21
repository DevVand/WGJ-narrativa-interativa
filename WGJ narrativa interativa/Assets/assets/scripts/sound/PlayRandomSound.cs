using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] AudioClip[] sounds;
    [SerializeField] bool oneShot = true;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void Play() {
        //source.pitch = Random.Range(pitchMinus, pitchPlus);
        if (oneShot)
            source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        else
        {
            source.clip = sounds[Random.Range(0, sounds.Length)];
            source.Play();
        }
    }
    public void PlayAt(int index)
    {
        if (oneShot)
            source.PlayOneShot(sounds[index]);
        else
        {
            source.clip = sounds[index];
            source.Play();
        } 
    }
    public bool IsPlaying() { return source.isPlaying; }
}

