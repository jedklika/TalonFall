﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<int> keys;
    public float timer;
    public bool TimeToDie;
	
    public float playerHealth = 90;
	public float playerMaxHealth = 90;
	
    public float SprintTime = 30;
    public bool sprint;
	private bool exhausted = false;
	
	public int revolverMaxAmmo;
	private int revolverAmmo;
	
	public int shotgunMaxAmmo;
	private int shotgunAmmo;
	
	public bool canBeDamaged;
	public bool isDead;
	
	//For player state (will make enumeration for this later) -- David P
	public int playerState = 0;
	public int cursorState = 0;
	/*For now:
		0 = player can control
		1 = player teleporting/in cutscene
		2 = interaction
		3 = combo lock
	*/
	
	//For indicating stage player is in
	public int playerStage = 0;					//Need enumeration here too
	// 0 = officenight indoor, 1 = officenight outdoor, 2 = diner
	
	PlayerMovement player;
	PlayerSound player_sound;
	
	UI_changer ui_manager;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		player_sound = FindObjectOfType<PlayerSound>();
		ui_manager = GameObject.Find("ui_canvas").GetComponent<UI_changer>();
		
		revolverAmmo = revolverMaxAmmo;
		shotgunAmmo = shotgunMaxAmmo;
		
		canBeDamaged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprint && !player.onLadder)
        {
            SprintTime -= Time.deltaTime;
            player.Speed = player.RunSpeed;
            player.jumpHeight = 7;
            //Debug.Log(SprintTime);
        } 
		else 
		{
            player.Speed = 4;
            player.jumpHeight = 5;
			
			if(SprintTime < 30)
				SprintTime += Time.deltaTime;
        }
		
        if (TimeToDie)
        {
            timer = 30;
            timer -= Time.deltaTime;
            Mathf.Round(1);
        } 
		else 
		{
            timer = 100;
        }
		
		checkLeaveInteraction();
    }
	
	//Managing sprint
	public bool CanSprint()
	{
		//Recover a bit if exhausted
		if (exhausted)
		{
			if (SprintTime > 10.0f)
				exhausted = false;
			
			return false;
		}
		else
		{
			if (SprintTime < 1.0f)
				exhausted = true;
			
			return true;
		}
	}
	
	//Managing health
	public void setDamage(float new_damage)
	{
		playerHealth-=new_damage;
		
		//Checking bounds
		//Max
		if (playerHealth > playerMaxHealth)
		{
			playerHealth = playerMaxHealth;
		//Min									--The player dies if this reaches 0
		} 
		else if (playerHealth <= 0)
		{
			playerHealth = 0;
			StartCoroutine(setDeath());
		}
		
		//Flashing and sounds
		//Damage	(make sure not invincible)
		if (new_damage > 0 && canBeDamaged)
		{
			StartCoroutine(ui_manager.displayDamageFlash());
			player.SetAnimation(7);
			player_sound.PlayHurt();
			setInvincible();
			
			//Check to queue heartbeat
			player_sound.PlayWeak(playerHealth);
			
		//Heal
		} 
		else if (new_damage < 0)
		{
			StartCoroutine(ui_manager.displayHealFlash());
			player_sound.StopWeak(playerHealth);
		}
		
		//Setting face
		ui_manager.setFace(playerHealth);
	}
	
	IEnumerator setInvincible()
	{
		canBeDamaged = false;
		
		yield return new WaitForSeconds(1.5f);
		
		canBeDamaged = true;
	}
	
	IEnumerator setDeath()
	{
		if (!isDead)
		{
			isDead = true;
			ui_manager.SetDeathOverlay();
			player_sound.PlayDeath();
			yield return new WaitForSeconds(4.9f);
			SceneManager.LoadScene("TitleScene");
		}
	}
	
	public bool atMaxHealth(){
		return playerHealth == playerMaxHealth;
	}
	
	//Managing ammo
	public bool remainingRevolverAmmo()
	{
		return revolverAmmo > 0;
	}
	
	public bool atRevolverMaxAmmo()
	{
		return revolverAmmo == revolverMaxAmmo;
	}
	
	public void useRevolverAmmo(int used_ammo)
	{
		revolverAmmo-=used_ammo;
		
		if (revolverAmmo < 0)
		{
			revolverAmmo = 0;
		}
		else if (revolverAmmo > revolverMaxAmmo)
		{
			revolverAmmo = revolverMaxAmmo;
		}
		
		//Check if using revolver ammo
		if (used_ammo > 0)
			player_sound.PlayGun();
		else if (used_ammo < 0)
			player_sound.PlayLoad();
		else
			player_sound.PlayUnholster();
		
		//Display ammo capacity
		ui_manager.setGunAmmoDisplay(revolverAmmo.ToString()+"/"+revolverMaxAmmo.ToString());
	}
	
	public bool remainingShotgunAmmo()
	{
		return shotgunAmmo > 0;
	}
	
	public bool atMaxShotgunAmmo()
	{
		return shotgunAmmo == shotgunMaxAmmo;
	}
	
	public void useShotgunAmmo(int used_ammo)
	{
		shotgunAmmo-=used_ammo;
		
		if (shotgunAmmo < 0)
		{
			shotgunAmmo = 0;
		}
		else if (shotgunAmmo > shotgunMaxAmmo)
		{
			shotgunAmmo = shotgunMaxAmmo;
		}
		
		//Check if using shotgun ammo
		if (used_ammo > 0)
			player_sound.PlayGun();
		else if (used_ammo < 0)
			player_sound.PlayLoad();
		else
			player_sound.PlayUnholster();
		
		//Display ammo capacity
		ui_manager.setGunAmmoDisplay(shotgunAmmo.ToString()+"/"+shotgunMaxAmmo.ToString());
	}
	
	//INTERACTION AND KEYS
	public void notifyText(string notification){
		ui_manager.setTextActive(3.0f);
		ui_manager.setTextLabel(notification);
		Debug.Log(notification);
	}
	
	public void clearText(){
		ui_manager.setTextActive(0.0f);
	}
	
	public void addKey(int keyValue){
		keys.Add(keyValue);
	}
	
	public bool checkKey(int keyValue){
		bool keyResult = keys.Exists(element => element == keyValue);
		Debug.Log(keyResult);
		return keyResult;
	}
	
	//FOR CLICKING FOR INTERACTIONS
	public bool canInteract(){
		//CHECK IF ALREADY INTERACTING
		return playerState == 0;
	}
	
	public void setCursorState(int cursor_state){
		cursorState = cursor_state;
		ui_manager.setCursorImage(cursor_state);
	}
	
	public void interactionOpen(Sprite interaction_sprite){
		playerState = 2;
		ui_manager.setInteractionActive();
		ui_manager.setInteractionImageSprite(interaction_sprite);
	}
	
	public void checkLeaveInteraction(){
		//FOR EXITING OBSERVATION COMBO LOCK
		if (Input.GetKeyDown(KeyCode.X)){
			if (playerState == 2){
				playerState = 0;
				
				//close interaction image
				ui_manager.setInteractionInactive();
				clearText();
			} else if (playerState == 3){
				playerState = 0;
				
				//close lock
			}
		}
	}
	
	public void requestComboLock(int key_1, int key_2, int key_3, int key_4){
		if (canInteract()){
			playerState = 3;
			
			//ACTIVE LOCK IN UI MANAGER
			//SEND COMBO SOLUTION TO UI
		}
	}
}
