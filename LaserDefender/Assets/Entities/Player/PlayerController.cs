using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
	public GameObject projectile;
	public float projectileSpeed;
	public float firingRate;
	public float health = 250;
	
	public AudioClip fireSound;
	
	private float padding = 1f;
	private float xmin;
	private float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftfMost = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
		xmin = leftfMost.x + padding;
		xmax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			// this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
			this.transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
		}
		
		// Restrict the player to the gamespace
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
		// Shooting lasers
		if (Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("Fire");
		}
	}
	
	void Fire() {
		Vector3 offset = new Vector3(0f, 1f, 0f);
		GameObject beam = Instantiate(projectile, this.transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0f, projectileSpeed, 0f);
		AudioSource.PlayClipAtPoint(fireSound, transform.position);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
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
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
		Destroy(gameObject);
	}
}
