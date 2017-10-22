using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	int IScore = 0;

	public Text score;

	public Text[] images;

	public Image endgame;

	GameManager gameManager;

	void Update(){
		if (gameManager) {
			images [1].enabled = !gameManager.player1;
			images [0].enabled = !gameManager.player2;
		}
	}

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		UpdateScore ();
	}

	public void End(){
		PlayerPrefs.SetInt ("currentScore", IScore);
		endgame.gameObject.SetActive (true);
		StartCoroutine(CheckRanking ());
	}

	IEnumerator CheckRanking(){
		yield return new WaitForSeconds (2f);
		int index = 10;

		for (int i = 9; i >= 0; i--) {
			if (PlayerPrefs.GetInt ("rankscore" + i.ToString ()) > PlayerPrefs.GetInt ("currentScore")) {
				index = i + 1;
				break;
			}
		}

		if (index != 10) {
			LoadingScreenManager.LoadScene (5);
		} else {
			LoadingScreenManager.LoadScene (0);
		}
	}

	void UpdateScore(){
		score.text = IScore.ToString ();
	}

	public void AddScore(int amount){
		IScore += amount;

		UpdateScore ();
	}
}
