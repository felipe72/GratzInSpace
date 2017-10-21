using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	PlayerController player;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player) {
			transform.position = player.transform.position;

			RaycastHit2D hit = Physics2D.Raycast (player.transform.position, Vector2.right, 10000, 1 << LayerMask.NameToLayer ("Enemy"));

			if (hit) {
				var size = transform.localScale;
				size.x = hit.collider.bounds.center.x - player.transform.position.x;
				transform.localScale = size;

				var pos = player.transform.position;
				pos.x += size.x / 2;
				transform.position = pos;

				Destroy (hit.collider.gameObject, 0.2f);
				hit.collider.gameObject.GetComponent<EnemyController> ().Die ();
			} else {
				var scale = transform.localScale;
				scale.x = 1000;
				transform.localScale = scale;

				var pos = Vector3.zero;
				pos.x = 500;
				transform.position += pos;
			}
		}
	}
}
