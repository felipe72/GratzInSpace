using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public bool player1 = false;
	public bool player2 = false;

	public bool started = false;

	public GameObject player1Go;
	public GameObject player2Go;

	PlayerController player;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}

	void Update(){
		if (started) {
			if (Input.GetKeyDown (KeyCode.F) && !player1) {
				player1 = true;
				player.gameObject.SetActive (true);
				player.player1 = true;
			} else if (Input.GetKeyDown (KeyCode.K) && !player2) {
				player2 = true;
				player.gameObject.SetActive (true);
				player.player1 = false;
			}
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
