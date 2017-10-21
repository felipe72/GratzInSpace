using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rigidbody;
	public float speed = 8f;
	public GameObject shoot;
	
	void Start () {
		rigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		rigidbody.velocity = new Vector2(speed * x, speed * y);
		if(Input.GetKeyDown(KeyCode.F)){
			Instantiate(shoot, this.transform.position, this.transform.rotation);
		}
	}
}
