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

	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D>();
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
		if (player1) {
			x = Input.GetAxis ("Horizontal");
			y = Input.GetAxis ("Vertical");
		} else {
			x = Input.GetAxis ("Horizontal2");
			y = Input.GetAxis ("Vertical2");
		}
		rigidbody.velocity = new Vector2(speed * x, speed * y);
		if (laser) {
			if (Input.GetKeyDown (KeyCode.F)) {
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				lastShoot = Time.time;
			} else if(Input.GetKeyUp(KeyCode.F)){
				Destroy(shootObject);
			}
		} 
		else if(explode){
			if (Input.GetKeyDown (KeyCode.F) && !exist) {
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				lastShoot = Time.time;
				exist = true;
			}
			else if(Input.GetKeyDown (KeyCode.F) && shootObject){
				shootObject.GetComponent<ExplodeShoot>().Explode();
			}
		}
		else {
			if (Input.GetKey (KeyCode.F) && Time.time - lastShoot > fireRate) {
				Instantiate (shoot, this.transform.position, this.transform.rotation);
				lastShoot = Time.time;
			}
		}
	}
}
