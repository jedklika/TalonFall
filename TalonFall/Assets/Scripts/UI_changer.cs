using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_changer : MonoBehaviour
{
	public Image health_image;
	public Sprite sprite_face_good;
	public Sprite sprite_face_okay;
	public Sprite sprite_face_weak;
	
	public Image gun_image;
	public Sprite sprite_shotgun;
	public Sprite sprite_handgun;
	
	public Text gun_ammo;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
	public void setToShotgun(){
		gun_image.sprite = sprite_shotgun;
		RectTransform rt = gun_image.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(175,50);
		
	}
	
	public void setToHandgun(){
		gun_image.sprite = sprite_handgun;
		RectTransform rt = gun_image.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(100,100);
	}
	
	public void setGunAmmoDisplay(string new_ammo){
		gun_ammo.text = new_ammo;
	}
}
