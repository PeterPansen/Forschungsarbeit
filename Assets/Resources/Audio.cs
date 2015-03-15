using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {


	AudioClip Hover;
	AudioClip Click;
	AudioSource Speaker;

	// Use this for initialization
	void Start () {
	
		Hover = (AudioClip)Resources.Load ("Hover", typeof(AudioClip));
		Click = (AudioClip)Resources.Load ("Click", typeof(AudioClip));
		Speaker =  this.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(string SoundName)
	{
		if (SoundName == "Hover") 
		{
			if(GetComponent<AudioSource>().clip != Hover || GetComponent<AudioSource>().isPlaying == false)
			{
				Speaker.clip = Hover;
				Speaker.Play();
			}
		}

		if (SoundName == "Click") 
		{
			if(GetComponent<AudioSource>().clip != Click || GetComponent<AudioSource>().isPlaying == false)
			{
				Speaker.clip = Click;
				Speaker.Play();
			}
		}

	}
}
