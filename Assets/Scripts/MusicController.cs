using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum MusicType { Level1, Level2 };


public class MusicController : MonoBehaviour {
	private static MusicController _instance;

	[System.Serializable]
	public class Music {
		public MusicType type;
		public AudioClip clip;
	}

	public Music[] list;

	AudioSource _audio;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);

		if (!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}

	public void Play(MusicType type){
		if (_audio == null) {
			_audio = GetComponent<AudioSource> ();
		}

		Music music = null;

		foreach(var _music in list){
			if (_music.type == type) {
				music = _music;
				break;
			}
		}

		if (_audio.isPlaying) {
			_audio.DOFade (0, 1f).OnComplete (() => {
				_audio.clip = music.clip;
				_audio.Play();
				_audio.DOFade(1f, 1f);
			});
		} else {
			_audio.volume = 0;
			_audio.clip = music.clip;
			_audio.Play();
			_audio.DOFade(1f, 1f);
		}
	}
}
