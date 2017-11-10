using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public GameObject projectile, gun;
	private GameObject projectileParent;
	private Animator animator;
	private Spawner myLaneSpawner;
	
	void Update() {
		if (IsAttackerAheadInLane()) {
			animator.SetBool("isAttacking", true);
		}
		else {
			animator.SetBool("isAttacking", false);
		}
	}
	
	void SetMyLaneSpawner() {
		Spawner[] spawners = GameObject.FindObjectsOfType<Spawner>();
		foreach(Spawner thisSpawner in spawners) {
			if (thisSpawner.transform.position.y == this.transform.position.y) {
				myLaneSpawner = thisSpawner;
				return;
			}
		}
		Debug.LogError("No Spawner found for " + name);
	}
	
	void Start () {
		animator = GameObject.FindObjectOfType<Animator>();
		
		// Creates a parent if necessary
		projectileParent = GameObject.Find ("Projectiles");
		if (!projectileParent) {
			projectileParent = new GameObject("Projectiles");
		}
		
		SetMyLaneSpawner();
		print (myLaneSpawner);
	}
	
	private void Fire() {
		GameObject newProjectile = Instantiate (projectile) as GameObject;
		newProjectile.transform.parent = projectileParent.transform;
		newProjectile.transform.position = gun.transform.position;
	}
	
	private bool IsAttackerAheadInLane (){
		// Exit if no attakers in lane
		if (myLaneSpawner.transform.childCount <= 0) {
			return false;
		}
		
		// If there are attackers, are they ahead?
		foreach (Transform child in myLaneSpawner.transform) {
			if (child.transform.position.x >= this.transform.position.x) {
				return true;
			}
		}
		return false;
	}
}
