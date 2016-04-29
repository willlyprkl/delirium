using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

    public AudioSource playsound;
    public AudioSource playsound2;
    public AudioSource playsound3;
    public AudioSource playsound4;
    public AudioSource taustasound;

    public AudioClip tausta;

    void Awake() {
        DontDestroyOnLoad(this);

        taustasound.clip = tausta;

        taustasound.loop = true;
        taustasound.Play();


    }

    public void PlaySound(AudioClip sound) {
        playsound.clip = sound;

        playsound.Play();
    }

    public void PlaySound2(AudioClip sound) {
        playsound3.clip = sound;

        playsound3.Play();
    }

    public void PlaySoundX(params AudioClip[] sounds) {
        int rand = Random.Range(0, sounds.Length);

        playsound.clip = sounds[rand];

        playsound.Play();
    }

    public void PlaySoundY(params AudioClip[] sounds) {
        int rand = Random.Range(0, sounds.Length);

        playsound2.clip = sounds[rand];

        playsound2.Play();
    }

    public void PlaySoundZ(params AudioClip[] sounds) {
        int rand = Random.Range(0, sounds.Length);

        playsound3.clip = sounds[rand];

        playsound3.Play();
    }

    public void LiikeSound(AudioClip sound) {
        float randPitch = Random.Range(0.95f, 1.05f);

        playsound4.pitch = randPitch;

        playsound4.clip = sound;

        playsound4.Play();
    }
	

}
