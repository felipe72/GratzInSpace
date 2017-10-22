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
				if (player) {
					player.transform.position = new Vector3 (-6.71f, 2.94f, 0);
					player.gameObject.SetActive (true);
					player.player1 = true;
					player.invul = true;
					player.isActive = true;
					player.GetComponent<SpriteRenderer>().flipX = true;
					var scale = player.transform.localScale;
					scale.x = Mathf.Abs (scale.x);
					player.transform.localScale = scale;
					StartCoroutine (player.CantDie ());
				}
			} else if ((Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.K)) && !player2) {
				player2 = true;
				if (player) {
					player.transform.position = new Vector3 (-6.74f, -1.89f, 0);
					player.gameObject.SetActive (true);
					player.invul = true;
					player.GetComponent<SpriteRenderer>().flipX = true;
					player.isActive = true;
					var scale = player.transform.localScale;
					scale.x = Mathf.Abs (scale.x);
					player.transform.localScale = scale;
					StartCoroutine (player.CantDie ());
				}
			}
		}
	}

	public void Load(PlayerController _player){
		player = _player;
	}
}
