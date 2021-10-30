using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Audio[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Audio s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
		//Mute();
		//DataModel.startedMute = false;
	}

	private void Start()
	{
		//Mute();
	}

	public void Play(string sound)
	{
		Audio s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if(!s.source.isPlaying && s.bgm)
		s.source.Play();
		else if(s.sfx)
			s.source.Play();
	}


	public void Stop(string sound)
	{
		Audio s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	public void Mute()
	{
		DataModel.startedMute = false;
		Debug.Log("DISPAROU Mute");
		foreach (Audio s in sounds)
		{
			if(DataModel.muteBgm)
			{
				if (s.bgm)
					s.source.mute = true;
			}
			else
				if (s.bgm)
				s.source.mute = false;

			if (DataModel.muteSfx)
			{
				if (s.sfx)
					s.source.mute = true;
			}
			else
				if (s.sfx)
				s.source.mute = false;
		}
	}

#region Inscrição e trancamento nos eventos

		void OnEnable()
	{
		Settings.OnMute += Mute;
	}

	void OnDisable()
	{
		Settings.OnMute -= Mute;
	}
#endregion
}
