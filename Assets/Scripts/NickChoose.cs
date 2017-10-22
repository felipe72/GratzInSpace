using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class NickChoose : MonoBehaviour {

	string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	public Text[] letters;

	int index = 0;
	float lasttime = 0;
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 3; i++) {
			if (i == index) {
				letters [i].color = Color.yellow;
			} else {
				letters [i].color = Color.white;
			}
		}

		if (Input.GetAxis("Horizontal") < -.5f || Input.GetAxis("Horizontal2") < -.5f && Time.time - lasttime > .25f) {
			lasttime = Time.time;
			index = (index == 0 ? 2 : (index - 1));
		} else if (Input.GetAxis("Horizontal") > .5f || Input.GetAxis("Horizontal2") > .5f && Time.time - lasttime > .25f) {
			lasttime = Time.time;
			index = (index == 2 ? 0 : (index + 1));
		}


		if (Input.GetAxis("Vertical") > .5f || Input.GetAxis("Vertical2") > .5f && Time.time - lasttime > .25f) {
			lasttime = Time.time;
			int _index = s.IndexOf(letters [index].text [0]);
			print (_index);

			_index += 1;
			_index %= 26;
			letters [index].text = s [_index].ToString();
		} else if (Input.GetAxis("Vertical") < -.5f || Input.GetAxis("Vertical2") < -.5f && Time.time - lasttime > .25f) {
			lasttime = Time.time;
			int _index = s.IndexOf(letters [index].text [0]);
			_index -= 1;
			_index = (_index + 26)%26;
			letters [index].text = s [_index].ToString();
		}

		if (Input.GetKeyDown(KeyCode.Joystick1Button0)|| Input.GetKeyDown(KeyCode.Joystick2Button0) && Time.time - lasttime > .25f) {
			lasttime = Time.time;
			PlayerPrefs.SetString ("currentName", letters [0].text + letters [1].text + letters [2].text);
			LoadingScreenManager.LoadScene (0);
		}
	}
}
