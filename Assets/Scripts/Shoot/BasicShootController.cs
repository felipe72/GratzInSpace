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

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "EnemyStasis"){
			EnemyController enemy = col.gameObject.GetComponent<EnemyController> ();
			enemy.Die ();
			Destroy(this.gameObject);
		}
		if(col.gameObject.tag == "Enemy"){
			print ("boi");
			EnemyShooterController enemy = col.gameObject.GetComponent<EnemyShooterController> ();
			if (enemy) {
				enemy.Die ();
			} else if(col.gameObject.GetComponent<EnemyController> ()){
				EnemyController _enemy = col.gameObject.GetComponent<EnemyController> ();
				_enemy.Die ();
			} else if(col.gameObject.GetComponent<Boss> ()){
				print ("macaco");
				Boss boss = col.gameObject.GetComponent<Boss> ();
				boss.ReceiveDamage (10);
			}
			//Destroy(this.gameObject);
		}

		PlayerController _player = col.GetComponent<PlayerController> ();
		if(_player && !_player.isActive && _player != player){
			_player.setRestoreLife();
		}
		if(col.gameObject.tag == "Player" && col.gameObject != player.gameObject){
			int combo = 0;
			if(player.shotgun) combo = 1;
			col.gameObject.GetComponent<PlayerController> ().Combo(combo);
			Destroy(this.gameObject);
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
