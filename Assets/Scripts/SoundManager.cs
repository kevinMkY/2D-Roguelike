using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource exfAudio;
	public AudioSource bgMusicAudio;
	public static SoundManager instance = null;

	float highVolume = 1.05f;
	float lowVolume = 0.95f;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	public void PlaySingle (AudioClip clip) {
		exfAudio.clip = clip;
		exfAudio.Play ();
	}

	public void PlayRandomClip (params AudioClip[] clip) {
		int randomIndex = Random.Range(0, clip.Length);
		float volume = Random.Range (lowVolume, highVolume);

		exfAudio.pitch = volume;
		exfAudio.clip = clip[randomIndex];
		exfAudio.Play ();
	}
}
