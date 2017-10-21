using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y);
		this.transform.position = nextPosition;
		if(this.transform.position.x < -30f){
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			Destroy(collider.gameObject);
		}
	}
}
