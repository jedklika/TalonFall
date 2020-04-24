using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is to manage player sounds

public class PlayerSound : MonoBehaviour
{
	public AudioSource hurt_sound;
	public AudioSource gun_sound;
	public AudioSource weak_sound;
	
	public void PlayHurt(){
		hurt_sound.Play(0);
	}
	
	public void PlayGun(){
		gun_sound.Play(0);
	}
	
	public void PlayWeak(float playerHealth){
		if (playerHealth < 45)
			weak_sound.Play(0);
	}
	
	public void StopWeak(float playerHealth){
		if (playerHealth >= 45)
			weak_sound.Stop();
	}
}
