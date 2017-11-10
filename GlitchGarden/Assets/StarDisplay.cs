using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class StarDisplay : MonoBehaviour {
	
	private Text text;
	private int stars = 100;
	public enum Status {SUCCESS, FAILURE};
	
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		UpdateDisplay();
	}
	
	private void UpdateDisplay () {
		if (text) {
			text.text = stars.ToString();
		}
	}
	
	public void AddStars (int amount) {
		stars += amount;
		UpdateDisplay();
	}
	
	public Status UseStars (int amount) {
		if (stars >= amount) {
			stars -= amount;
			UpdateDisplay();
			return Status.SUCCESS;
		}
		return Status.FAILURE;
	}
}
