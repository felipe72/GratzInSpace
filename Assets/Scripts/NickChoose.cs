using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NickChoose : MonoBehaviour {

	string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public Text[] letters;

	int index = 0;
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 3; i++) {
			if (i == index) {
				letters [i].color = Color.yellow;
			} else {
				letters [i].color = Color.white;
			}
		}

		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			index = (index == 0 ? 2 : (index - 1));
		} else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
			index = (index == 2 ? 0 : (index + 1));
		}


		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			int _index = s.IndexOf(letters [index].text [0]);
			print (_index);

			_index += 1;
			_index %= 26;
			letters [index].text = s [_index].ToString();
		} else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			int _index = s.IndexOf(letters [index].text [0]);
			_index -= 1;
			_index = (_index + 26)%26;
			letters [index].text = s [_index].ToString();
		}

		if (Input.GetKeyDown (KeyCode.F) || Input.GetKeyDown (KeyCode.K)) {
			PlayerPrefs.SetString ("currentName", letters [0].text + letters [1].text + letters [2].text);
			LoadingScreenManager.LoadScene (0);
		}
	}
}
