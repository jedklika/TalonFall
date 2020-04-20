using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_changer : MonoBehaviour
{
	public Image cursor;
	
	public Image hurt_flash;
	public Image heal_flash;
	
	public Image health_image;
	public Sprite sprite_face_good;
	public Sprite sprite_face_okay;
	public Sprite sprite_face_weak;
	
	public Image gun_image;
	public Sprite sprite_shotgun;
	public Sprite sprite_handgun;
	public Sprite sprite_axe;
	public Sprite sprite_knife;
	
	public Text gun_ammo;
	
    // Start is called before the first frame update
    void Start()
    {
        //MAKING THE CURSOR INVISIBLE
		Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //HAVE THE "CURSOR" FOLLOW THE REAL CURSOR'S POSITION
		
		//GET THE REAL CURSOR'S POSITION
		Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mouse_position = mouse_ray.origin;
		mouse_position.z = 0;
		
		cursor.transform.position = mouse_position;
		
		//Debug.Log(mouse_position);
    }
	
	//Managing health UI
	public void setFace(float player_health){
		if (player_health > 60) {
			health_image.sprite = sprite_face_good;
		} else if (player_health > 30) {
			health_image.sprite = sprite_face_okay;
		} else {
			health_image.sprite = sprite_face_weak;
		}
	}
	
	//Managing weapon UI
	//Guns
	public void setToShotgun(){
		gun_image.sprite = sprite_shotgun;
		
	}
	
	public void setToHandgun(){
		gun_image.sprite = sprite_handgun;
	}
	
	public void setGunAmmoDisplay(string new_ammo){
		gun_ammo.text = new_ammo;							//Used by melee too
	}
	
	//Melee
	public void setToAxe(){
		gun_image.sprite = sprite_axe;
	}
	
	public void setToKnife(){
		gun_image.sprite = sprite_knife;
	}
}
