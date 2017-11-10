using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] attackerPrefabArray;
	public bool active;
	
	// Update is called once per frame
	void Update () {
		if (active) {
			foreach (GameObject thisAttacker in attackerPrefabArray) {
				if (IsTimeToSpawn(thisAttacker)) {
					Spawn (thisAttacker);
				}
			}
		}
	}
	
	void Start() {
		active = true;
	}
	
	void Spawn (GameObject myGameObject) {
		GameObject myAttacker = Instantiate (myGameObject) as GameObject;
		myAttacker.transform.parent = transform;
		myAttacker.transform.position = transform.position;
	}
	
	bool IsTimeToSpawn(GameObject attackerGameObject) {
		Attacker attacker = attackerGameObject.GetComponent<Attacker>();
		float meanSpawnDelay = attacker.seenEverySeconds;
		float spawnsPerSecond = 1 / meanSpawnDelay;
		
		if (Time.deltaTime > meanSpawnDelay) {
			Debug.Log("Spawn rate tapped by framerate");
		}
		
		float threshold = spawnsPerSecond * Time.deltaTime / 5;
		return Random.value < threshold;
	}
}
