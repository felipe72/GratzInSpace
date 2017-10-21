using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("once", 0) == 0) {
			PlayerPrefs.SetInt ("once", 1);
			StarterRanks ();
		}

		if (PlayerPrefs.GetInt ("currentScore", 0) != 0) {
			UpdateRanking ();
		}
	}

	void UpdateRanking(){
		int index = 10;

		for (int i = 9; i >= 0; i--) {
			if (PlayerPrefs.GetInt ("rankscore" + i.ToString ()) > PlayerPrefs.GetInt ("currentScore")) {
				index = i + 1;
				break;
			}
		}

		if (index != 10) {
			for (int i = 9; i >= index + 1; i--) {
				PlayerPrefs.SetInt ("rankscore" + i.ToString (), PlayerPrefs.GetInt ("rankscore" + (i - 1).ToString ()));
				PlayerPrefs.SetString ("rankname" + i.ToString (), PlayerPrefs.GetString ("rankname" + (i - 1).ToString ()));
			}
				
			PlayerPrefs.SetInt ("rankscore" + index.ToString (), PlayerPrefs.GetInt ("currentScore"));
			PlayerPrefs.SetString ("rankname" + index.ToString (), PlayerPrefs.GetString ("currentName"));
		}

		PlayerPrefs.SetInt ("currentScore", 0);
	}

	void StarterRanks(){
		string[] ranks = new string[10]{"AAA", "XYZ", "AFK", "LOL", "WTF", "BBQ", "KKK", "ONE", "RAW", "POP"};

		for (int i = 0; i < 10; i++) {
			PlayerPrefs.SetString ("rankname" + i.ToString (), ranks [i]);
			PlayerPrefs.SetInt ("rankscore" + i.ToString(), 1000 * (10 - i));
		}
	}

	void PrintRank(){
		for (int i = 0; i < 10; i++) {
			print (PlayerPrefs.GetString ("rankname" + i.ToString ()) + PlayerPrefs.GetInt ("rankscore" + i.ToString ()).ToString ());
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.K) || Input.GetKeyDown(KeyCode.F)) {
			LoadingScreenManager.LoadScene (2);
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			PrintRank ();		
		}
	}
}
