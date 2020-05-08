﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
	public AudioSource music;
	
	public AudioClip office;
	public AudioClip diner;
	public AudioClip sewer;
	public AudioClip tbc;
	
	private int musicState;
	
    // Start is called before the first frame update
    void Start()
    {
        musicState = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void PlayTrack(int trackNum)
	{
		if (musicState != trackNum)
		{
			//Debug.Log("playing track number " + trackNum);
			music.Stop();
			
			switch (trackNum)
			{
			case 0:
				PlayOffice();
			break;
			case 1:
				PlayDiner();
			break;
			case 2:
				PlaySewer();
			break;
			case 3:
				PlayTbc();
			break;
			}
		}
	}
	
	private void PlayOffice()
	{
		music.clip = office;
		music.Play();
		musicState = 0;
	}
	
	private void PlayDiner()
	{
		music.clip = diner;
		music.Play();
		musicState = 1;
	}
	
	private void PlaySewer()
	{
		music.clip = sewer;
		music.Play();
		musicState = 2;
	}
	
	private void PlayTbc()
	{
		music.clip = tbc;
		music.Play();
		musicState = 3;
	}
}
