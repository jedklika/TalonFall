using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlescreen_sounds : MonoBehaviour
{
	public AudioSource sound_source;
	public AudioClip button_click_1, button_click_2;
	private System.Random rdm;
	
    // Start is called before the first frame update
    void Start()
    {
        rdm = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void TitleButtonClick()
	{
		int soundChance = rdm.Next(100);
		
		if (soundChance < 50)
		{
			PlayClickOne();
		} 
		else 
		{
			PlayClickTwo();
		}
	}
	
	private void PlayClickOne()
	{
		sound_source.PlayOneShot(button_click_1);
	}
	
	private void PlayClickTwo()
	{
		sound_source.PlayOneShot(button_click_2);
	}
}
