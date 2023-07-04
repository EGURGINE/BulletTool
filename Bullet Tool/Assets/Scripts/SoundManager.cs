using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }


    public void PlaySound(AudioClip clip)
    {
        AudioSource audio = new GameObject().AddComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();

        Destroy(audio.gameObject,audio.clip.length);
    }



}
