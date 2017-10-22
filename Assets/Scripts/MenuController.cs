using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuController : MonoBehaviour {
	public Image[] curtains;
	public Image[] players;

	GameManager gameManager;

	void Start(){
		gameManager = FindObjectOfType<GameManager> ();
		gameManager.started = false;
		gameManager.player1 = false;
		gameManager.player2 = false;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.F)) {
			if (gameManager.player1) {
				LoadingScreenManager.LoadScene (3);
			} else {
				gameManager.player1 = true;
				curtains [0].rectTransform.DOMoveY (curtains [0].rectTransform.position.y + 2000, 1);
			}
		}

		if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.K)) {
			if (gameManager.player2) {
				LoadingScreenManager.LoadScene (3);
			} else {
				gameManager.player2 = true;
				curtains [1].rectTransform.DOMoveY (curtains [1].rectTransform.position.y + 2000, 1);
			}
		}
	}
}
