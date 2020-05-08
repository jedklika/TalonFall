using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescreen_music : MonoBehaviour
{
	public AudioSource musicSource;
	
	public AudioClip titleMusic;
	
	//A bit much for only one track as of now, but this script is in case music needs to be changed or stopped
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void StopMusic()
	{
		musicSource.Stop();
	}
	
	public void PlayTitle()
	{
		musicSource.PlayOneShot(titleMusic);
	}
}
