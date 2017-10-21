using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	bool isDead = false;

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			Vector2 nextPosition = new Vector2 (this.transform.position.x - 0.05f, this.transform.position.y);
			this.transform.position = nextPosition;
			if (this.transform.position.x < -30f) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Die(){
		isDead = true;
		Destroy (gameObject, .2f);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			Debug.Log("COLIDIIIIIIIIU COM O PLAYER!");
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.Die ();
		}
	}
}
