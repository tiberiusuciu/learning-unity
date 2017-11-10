using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour {

	public GameObject defenderPrefab;
	public static GameObject selectedDefender;

	private Button[] buttonArray;
	private Text costText;
	// Use this for initialization
	void Start () {
		buttonArray = GameObject.FindObjectsOfType<Button>();
		foreach (Button thisButton in buttonArray) {
			thisButton.GetComponent<SpriteRenderer>().color = Color.gray;
		}
		costText = GetComponentInChildren<Text>();
		if (!costText) {
			Debug.LogWarning(name + " has no cost text!");
		}
		else {
			costText.text = defenderPrefab.GetComponent<Defender>().starCost.ToString();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		foreach (Button thisButton in buttonArray) {
			thisButton.GetComponent<SpriteRenderer>().color = Color.gray;
		}
		GetComponent<SpriteRenderer>().color = Color.white;
		selectedDefender = defenderPrefab;
	}
}
