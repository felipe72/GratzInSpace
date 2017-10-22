using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShootController : MonoBehaviour {
	
	public float slope = 0f;
	PlayerController player;

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
		if(collider.gameObject.tag == "EnemyStasis"){
			EnemyController enemy = collider.gameObject.GetComponent<EnemyController> ();
			enemy.Die ();
			Destroy(this.gameObject);
		}
		if(collider.gameObject.tag == "Enemy"){
			EnemyShooterController enemy = collider.gameObject.GetComponent<EnemyShooterController> ();
			if (enemy) {
				enemy.Die ();
			} else {
				EnemyController _enemy = collider.gameObject.GetComponent<EnemyController> ();
				_enemy.Die ();
			}
			//Destroy(this.gameObject);
		}
		if(collider.gameObject.tag == "Player" && !player.isActive){
			player.setRestoreLife();
		}
		if(collider.gameObject.tag == "Player" && collider.gameObject != player.gameObject){
			int combo = 0;
			if(player.shotgun) combo = 1;
			collider.gameObject.GetComponent<PlayerController> ().Combo(combo);
			Destroy(this.gameObject);
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
