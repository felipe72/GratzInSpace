using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootController : MonoBehaviour {
	
	public float slope = 0f;

	public float timeLife;
	public float speed;

	void Start(){
		StartCoroutine (destroyAfterSeconds ());
	}

	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + speed, this.transform.position.y + slope);
		this.transform.position = nextPosition;
	}

	IEnumerator destroyAfterSeconds(){
		yield return new WaitForSeconds (timeLife);

		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Enemy"){
			EnemyController enemy = collider.gameObject.GetComponent<EnemyController> ();
			enemy.Die ();
			Destroy(this.gameObject);
		}
		PlayerController player = collider.gameObject.GetComponent<PlayerController>();
		if(collider.gameObject.tag == "Player" && !player.isActive){
			player.setRestoreLife();
		}
	}
}
