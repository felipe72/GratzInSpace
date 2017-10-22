using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour {

	public Text[] texts;

	int[] seq = new int[]{8, 9, 0, 1, 2, 3};

	public void Next(){
		for (int i = 0; i < 6; i++) {
			texts [i].text = PlayerPrefs.GetString ("rankname" + seq [i].ToString ()) + " <size=80>" + PlayerPrefs.GetInt("rankscore"+seq[i].ToString()).ToString() + "</size>";
		}

		for (int i = 0; i < 6; i++) {
			seq [i]++;
			seq [i] %= 10;
		}
	}
}
