using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject projectile;
	public float projectileSpeed = 10;
	public float health = 150;
	public float shotsPerSeconds;
	public int scoreValue = 150;
	public AudioClip fireSound;
	public AudioClip deathSound;
	
	private ScoreKeeper scoreKeeper;

	void Start() {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update() {
		float probability = shotsPerSeconds * Time.deltaTime;
		if (Random.value < probability) {
			Fire();
		}
	}
	
	void Fire() {
		Vector3 start = transform.position + new Vector3(0f, -1f, 0f);
		GameObject missile = Instantiate(projectile, start, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0, -projectileSpeed);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log(col);
		Projectile missile = col.gameObject.GetComponent<Projectile>();
		if (missile) {
			health -= missile.GetDamage();
			missile.Hit();
			if (health <= 0) {
				Die();
			}
		}
	}
	void Die() {
		AudioSource.PlayClipAtPoint(deathSound, transform.position);
		Destroy(gameObject);
		scoreKeeper.Score(scoreValue);
	}
}
