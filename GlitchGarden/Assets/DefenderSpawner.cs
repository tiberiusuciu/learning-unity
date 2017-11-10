using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour {

	public Camera myCamera;
	private GameObject parent;
	private StarDisplay starDisplay;

	// Use this for initialization
	void Start () {
		parent = GameObject.Find ("Defenders");
		starDisplay = GameObject.FindObjectOfType<StarDisplay>();
		if (!parent) {
			parent = new GameObject("Defenders");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown () {
		if (Button.selectedDefender) {
			if(!(SnapToGrid(CalculateWorldPointOfMouseClick()).y >= 6 ||  SnapToGrid(CalculateWorldPointOfMouseClick()).y <= 0)){
				int defenderCost = Button.selectedDefender.GetComponent<Defender>().starCost;
				if (starDisplay.UseStars(defenderCost) == StarDisplay.Status.SUCCESS) {
					SpawnDefender ();
				}
				else {
					Debug.Log ("Insufficient stars to spawn");
				}
			}
		}
	}

	void SpawnDefender ()
	{
		GameObject obj = Instantiate (Button.selectedDefender, SnapToGrid (CalculateWorldPointOfMouseClick ()), Quaternion.identity) as GameObject;
		obj.transform.parent = parent.transform;
	}
	
	Vector2 SnapToGrid (Vector2 rawWorldPos) {
		
		float newX = Mathf.RoundToInt(rawWorldPos.x);
		float newY = Mathf.RoundToInt(rawWorldPos.y);
		
		return new Vector2(newX, newY);
	}
	
	Vector2 CalculateWorldPointOfMouseClick () {
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;
		
		Vector3 weirdTriplet = new Vector3 (mouseX, mouseY, distanceFromCamera);
		Vector2 worldPos = myCamera.ScreenToWorldPoint(weirdTriplet);
		
		return worldPos;
	}
}
