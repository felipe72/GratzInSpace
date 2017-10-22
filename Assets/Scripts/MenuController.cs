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

		players [1].color = gameManager.player2Color;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.F)) {
			if (gameManager.player1) {
				LoadingScreenManager.LoadScene (3);
			} else {
				gameManager.player1 = true;
				curtains [0].rectTransform.DOMoveY (curtains [0].rectTransform.position.y + 2000, 1);
			}
		}

		if (Input.GetKeyDown (KeyCode.K)) {
			if (gameManager.player2) {
				LoadingScreenManager.LoadScene (3);
			} else {
				gameManager.player2 = true;
				curtains [1].rectTransform.DOMoveY (curtains [1].rectTransform.position.y - 2000, 1);
			}
		}
	}
}
