using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public PlayerController player;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			transform.position = player.transform.position + player.shootPosition;

			RaycastHit2D hit;
			if(player.player1){
				hit = Physics2D.Raycast (player.transform.position + player.shootPosition, Vector2.right, 10000, (1 << (LayerMask.NameToLayer ("Enemy")) | (1 << LayerMask.NameToLayer ("Player2"))));
			}
			else{
				hit = Physics2D.Raycast (player.transform.position + player.shootPosition, Vector2.right, 10000, (1 << (LayerMask.NameToLayer ("Enemy")) | (1 << LayerMask.NameToLayer ("Player1"))));	
			}
			if (hit) {
				var size = transform.localScale;
				size.x = hit.collider.bounds.center.x - (player.transform.position.x);
				transform.localScale = size;

				var pos = player.transform.position;
				pos.x += size.x / 2;
				transform.position = pos;
				if(hit.collider.gameObject.tag == "Player"){
					hit.collider.gameObject.GetComponent<PlayerController> ().Combo(2);
				}
				else{
					EnemyController enemy = hit.collider.gameObject.GetComponent<EnemyController> ();
					if (enemy) {
						enemy.Die ();	
					} else {
						EnemyShooterController _enemy = hit.collider.gameObject.GetComponent<EnemyShooterController> ();
						_enemy.Die ();
					}
				}
			} else {
				var scale = transform.localScale;
				scale.x = 500;
				transform.localScale = scale;

				var pos = Vector3.zero;
				pos.x = 250;
				transform.position += pos;
			}
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
