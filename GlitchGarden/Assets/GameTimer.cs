using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

	public float levelSeconds;
	
	private Slider slider;
	private AudioSource audioSource;
	private bool isEndOfLevel;
	private LevelManager levelManager;
	private GameObject winLabel;
	
	// Use this for initialization
	void Start () {
		isEndOfLevel = false;
		slider = GameObject.FindObjectOfType<Slider>();
		audioSource = GetComponent<AudioSource>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		winLabel = GameObject.Find("YouWin");
		if (!winLabel) {
			Debug.LogWarning("No win text found!");
		}
		winLabel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = Time.timeSinceLevelLoad / levelSeconds;
		
		
		if (Time.timeSinceLevelLoad >= levelSeconds && !isEndOfLevel) {
			HandleWinCondition ();
		}
	}

	void HandleWinCondition ()
	{
		DestroyAllTaggedObjects();
		DeactivateSpawners();
		isEndOfLevel = true;
		audioSource.Play ();
		winLabel.SetActive (true);
		Invoke ("LoadNextLevel", audioSource.clip.length);
	}
	
	// Destroy all objects with tag name DestroyOnWin
	void DestroyAllTaggedObjects() {
		GameObject[] obj = GameObject.FindGameObjectsWithTag("DestroyOnWin");
		foreach (GameObject myobj in obj) {
			Destroy (myobj.gameObject);
		}
	}
	
	void DeactivateSpawners() {
		Spawner[] array = GameObject.FindObjectsOfType<Spawner>();
		foreach (Spawner spawner in array) {
			spawner.active = false;
		}
	}
	
	private void LoadNextLevel() {
		levelManager.LoadNextLevel();
	}
}
