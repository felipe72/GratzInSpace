using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplodeShoot : MonoBehaviour {
	PlayerController player;
	public float speed = 0.1f;
	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + speed, this.transform.position.y);
		this.transform.position = nextPosition;
		if(this.transform.position.x > 30f){
			Destroy(this.gameObject);
		}
	}
	public void Explode(){
		if (player) {
			this.transform.DOScale (5f, 0.2f).SetEase (Ease.InQuad);
			player.exist = false;
			Destroy (this.gameObject, 0.2f);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(player && collider.gameObject.tag == "Enemy" && !player.exist){
			EnemyShooterController enemy = collider.gameObject.GetComponent<EnemyShooterController> ();
			if (enemy) {
				enemy.Die ();
			} else if (collider.gameObject.GetComponent<EnemyController> ()) {
				EnemyController _enemy = collider.gameObject.GetComponent<EnemyController> ();
				_enemy.Die ();
			} else if (collider.gameObject.GetComponent<Boss> ()) {
				Boss boss = collider.gameObject.GetComponent<Boss> ();
				boss.ReceiveDamage (50);
			}
		}
		if(collider.gameObject.tag == "Player" && collider.gameObject != player.gameObject){
			collider.gameObject.GetComponent<PlayerController> ().Combo(3);
			player.exist = false;
			Destroy(this.gameObject);
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
