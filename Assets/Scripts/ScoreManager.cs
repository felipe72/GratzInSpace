using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	int IScore = 3560;

	public Text score;

	// Use this for initialization
	void Start () {
		UpdateScore ();
	}

	public void End(){
		PlayerPrefs.SetInt ("currentScore", IScore);

		CheckRanking ();
	}

	void CheckRanking(){
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
		score.text = "Score: <size=80>" + IScore.ToString () + "</size>";
	}
}
