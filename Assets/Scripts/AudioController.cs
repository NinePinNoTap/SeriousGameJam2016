using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	//Audio Source object
	AudioSource audioObject;

	//Start method to assign audio emitter and play startup sound
	void Start() {

		audioObject = GetComponent<AudioSource>();
	}

public void PlaySound(AudioClip audioClip){
		audioObject.clip = audioClip;
		audioObject.Play();
		//audio.Play(44100);
	}
}