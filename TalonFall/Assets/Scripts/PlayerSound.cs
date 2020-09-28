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
	
	public AudioClip key_sound;
	public AudioClip observe_sound;
	
	public AudioClip lock_sound;
	public AudioClip unlock_sound;
	
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
	
	//INTERACT SOUND FX
	public void PlayKeyCollect(){
		fx_sound.PlayOneShot(key_sound);
	}
	
	public void PlayObserve(){
		fx_sound.PlayOneShot(observe_sound);
	}
	
	public void PlayLock(){
		fx_sound.PlayOneShot(lock_sound);
	}
	
	public void PlayUnlock(){
		fx_sound.PlayOneShot(unlock_sound);
	}
}
