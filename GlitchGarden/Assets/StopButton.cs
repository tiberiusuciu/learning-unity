using UnityEngine;
using System.Collections;

public class StopButton : MonoBehaviour {

	private LevelManager levelmanager;

	void OnMouseDown() {
		levelmanager = GameObject.FindObjectOfType<LevelManager>();
		levelmanager.LoadLevel("01a Start");
	}
}
