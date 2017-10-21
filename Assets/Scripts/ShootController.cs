using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {
	
	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y);
		this.transform.position = nextPosition;
		if(this.transform.position.x > 30f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log(collider.gameObject.tag);
		if(collider.gameObject.tag == "Enemy"){
			Destroy(collider.gameObject);
			Destroy(this.gameObject);
		}
	}
}
