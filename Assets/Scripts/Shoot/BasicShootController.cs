using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootController : MonoBehaviour {
	
	public float slope = 0f;

	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y + slope);
		this.transform.position = nextPosition;
		if(this.transform.position.x > 30f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Enemy"){
			Destroy(collider.gameObject);
			Destroy(this.gameObject);
		}
	}
}
