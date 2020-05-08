using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is to manage player sounds

public class PlayerSound : MonoBehaviour
{
	public AudioSource fx_sound;
	
	//public AudioSource heal_sound;
	public AudioSource weak_sound;
	public AudioClip hurt_sound;
	public AudioClip death_sound;
	
	public AudioClip unholster_sound;
	public AudioClip gun_sound;
	public AudioClip load_sound;
	
	//HEALTH SOUND FX
	public void PlayHurt(){
		fx_sound.PlayOneShot(hurt_sound);
	}
	
	public void PlayWeak(float playerHealth){
		if (playerHealth < 45)
			weak_sound.Play(0);
	}
	
	public void StopWeak(float playerHealth){
		if (playerHealth >= 45)
			weak_sound.Stop();
	}
	
	public void PlayDeath(){
		fx_sound.PlayOneShot(death_sound);
	}
	
	//GUN SOUND FX
	public void PlayUnholster(){
		fx_sound.PlayOneShot(unholster_sound);
	}
	
	public void PlayGun(){
		fx_sound.PlayOneShot(gun_sound);
	}
	
	public void PlayLoad(){
		fx_sound.PlayOneShot(load_sound);
	}
}
