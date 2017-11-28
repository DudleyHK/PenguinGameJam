using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


	private AudioSource SFXSource;
	private AudioSource musicSource;
	/*[SerializeField]
	public AudioClip swimSound;
	[SerializeField]
	public AudioClip walkSound;
	[SerializeField]
	public AudioClip jumpSound;
	[SerializeField]
	public AudioClip drowningSound;
	[SerializeField]
	public AudioClip starvingSound;
	[SerializeField]
	public AudioClip eatSound;*/
	[SerializeField]
	public AudioClip theme;


	// Use this for initialization
	void Start () {
		musicSource = GetComponent<AudioSource>();
		SFXSource = GetComponentInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!musicSource.isPlaying)	
		{
			musicSource.PlayOneShot (theme);
		}

	}

	/*
	public IEnumerator PlayNext(AudioClip clip)
	{
		float timer = 0f;
		while (SFXSource.isPlaying)
		{
			timer += Time.deltaTime;
			yield return false;
			if (timer > 3f)
			{
				yield return true;
			}
		}
		SFXSource.PlayOneShot (clip);
		yield return true;
	}*/
}
