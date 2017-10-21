using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D rigidbody;
	public float speed = 8f;
	public GameObject shoot;
	public float fireRate = 0.2f;
	private float lastShoot = 0f;

	public GameObject[] shoots;

	public bool laser = false;
	public bool explode = false;
	public bool exist = false;
	GameObject shootObject;

	public bool player1;
	GameManager gameManager;
	SpriteRenderer sr;
	bool invul = true;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		rigidbody = this.GetComponent<Rigidbody2D>();
		gameManager = FindObjectOfType<GameManager> ();
		if (gameManager) {
			gameManager.started = true;

			if (player1 && !gameManager.player1) {
				Destroy (gameObject);
			} else if (!player1 && !gameManager.player2) {
				Destroy (gameObject);
			}

			if (player1) {
				sr.color = gameManager.player1Color;
			} else {
				sr.color = gameManager.player2Color;
			}
		}

		StartCoroutine (CantDie());
	}
	
	IEnumerator CantDie(){
		yield return new WaitForSeconds (1f);

		invul = false;

	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			shoot = shoots [0];
			laser = false;
			explode = false;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			shoot = shoots [1];
			laser = false;
			explode = false;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			shoot = shoots [2];
			laser = true;
			explode = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			shoot = shoots [3];
			laser = false;
			explode = true;
		}

		float x = 0;
		float y = 0;
		if (!player1) {
			x = Input.GetAxis ("Horizontal");
			y = Input.GetAxis ("Vertical");
		} else {
			x = Input.GetAxis ("Horizontal2");
			y = Input.GetAxis ("Vertical2");
		}
		Vector2 vec = Vector2.zero;
		if (new Vector2 (x, y).magnitude > 1) {
			vec = new Vector2 (x, y).normalized;
		} else {
			vec = new Vector2 (x, y);
		}
		rigidbody.velocity = vec * speed;

		if (!player1) {
			Shoot (KeyCode.K);
		} else {
			Shoot (KeyCode.F);
		}

	}

	void Shoot(KeyCode key){
		if (laser) {
			if (Input.GetKeyDown (key)) {
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				shootObject.GetComponent<Laser> ().Load (this);
				lastShoot = Time.time;
			} else if(Input.GetKeyUp(key)){
				Destroy(shootObject);
			}
		} 
		else if(explode){
			if (Input.GetKeyDown (key) && !exist) {
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				shootObject.GetComponent<ExplodeShoot> ().Load (this);
				lastShoot = Time.time;
				exist = true;
			}
			else if(Input.GetKeyDown (key) && shootObject){
				shootObject.GetComponent<ExplodeShoot>().Explode();
			}
		}
		else {
			if (Input.GetKey (key) && Time.time - lastShoot > fireRate) {
				Instantiate (shoot, this.transform.position, this.transform.rotation);
				lastShoot = Time.time;
			}
		}
	}

	public void Die(){
		if (!invul) {
			Destroy (gameObject);
		}
	}

	void OnDestroy(){
		foreach (var x in FindObjectsOfType<Laser>()) {
			if (x.player == this) {
				Destroy (x.gameObject);
			}
		}
	}
}
