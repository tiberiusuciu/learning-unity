using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake() {
		DontDestroyOnLoad (gameObject);
		Debug.Log("Don't destroy me! " + name);
	}

	// Use this for initialization
	void Start () {
		audioSource = this.GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefsManager.GetMasterVolume();
	}
	
	// Update is called once per frame
	void OnLevelWasLoaded(int level) {
		AudioClip clip = levelMusicChangeArray[level];
		if (clip){
			audioSource.clip = clip;
			audioSource.loop = true;
			audioSource.Play();
		}
	}
	
	public void ChangeVolume (float volume) {
		audioSource.volume = volume;
	}
}
