using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	//Audio Source object
	AudioSource audio;

	//Start method to assign audio emitter and play startup sound
	void Start() {

		audio = GetComponent<AudioSource>();
	}

public void PlaySound(AudioClip audioClip){
		audio.clip = audioClip;
		audio.Play();
		//audio.Play(44100);
	}
}