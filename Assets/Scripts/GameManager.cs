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
		Cursor.visible = false;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
			Application.Quit ();
		}

		if (started) {
			if ((Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.F))&& !player1) {
				player1 = true;
				player.gameObject.SetActive (true);
				player.player1 = true;
				StartCoroutine(player.CantDie());
			} else if ((Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.K)) && !player2) {
				player2 = true;
				player.gameObject.SetActive (true);
				StartCoroutine(player.CantDie());
			}
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
