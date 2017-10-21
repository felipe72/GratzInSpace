using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool player1 = false;
	public bool player2 = false;

	public bool started = false;

	public GameObject player;

	public Color32 player1Color = new Color32(253, 0, 246, 255);
	public Color32 player2Color = new Color32(255, 195, 0, 255);

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		if (started) {
			if (Input.GetKeyDown (KeyCode.F) && !player1) {
				player1 = true;
				PlayerController _player = Instantiate (player, Vector3.zero, Quaternion.identity).GetComponent<PlayerController> ();
				_player.player1 = true;
			} else if (Input.GetKeyDown (KeyCode.K) && !player2) {
				player2 = true;
				PlayerController _player = Instantiate (player, Vector3.zero, Quaternion.identity).GetComponent<PlayerController> ();
				_player.player1 = false;
			}
		}
	}
}
