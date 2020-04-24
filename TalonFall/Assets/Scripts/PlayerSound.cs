using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is to manage player sounds

public class PlayerSound : MonoBehaviour
{
	public AudioSource hurt_sound;
	public AudioSource gun_sound;
	
	public void PlayHurt(){
		hurt_sound.Play(0);
	}
	
	public void PlayGun(){
		gun_sound.Play(0);
	}
}
