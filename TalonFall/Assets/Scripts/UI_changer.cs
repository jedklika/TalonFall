using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
	
public class UI_changer : MonoBehaviour
{
	public Image cursor;
	public Sprite cursor_normal;
	public Sprite cursor_observe;
	public Sprite cursor_lock;
	
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
	
	static float t = 0.0f;
	
	private bool display_hurt_flash;
	private bool display_heal_flash;
	
	public Color starting_hurt_color;
	private Color ending_hurt_color;
	
	public Color starting_heal_color;
	private Color ending_heal_color;
	
	private Color starting_tbc_color;
	public Color ending_tbc_color;
	
	private Color current_color;
	
	public Image death_screen;
	public Color death_color;
	
	//To be continued
	public Image tbc_screen;
	private bool display_tbc = false;
	
	//Observation
	public Text interaction_text;
	private float text_timer = 0.0f;
	private bool text_active = true;
	
	public Image interaction_image;
	
	
	//FOR COMBINATION LOCK
	//
	//COMBINATION LOCK DATA
	private int current_lock_1, current_lock_2, current_lock_3, current_lock_4;
	private int lock_check_1, lock_check_2, lock_check_3, lock_check_4;
	public GameObject lock_door_to_open;
	
	//COMBINATION LOCK UI
	public GameObject lock_bundle;
	
	public Text lock_display_1;
	public Button lock_1_increase_button;
	public Button lock_1_decrease_button;
	
	public Text lock_display_2;
	public Button lock_2_increase_button;
	public Button lock_2_decrease_button;
	
	public Text lock_display_3;
	public Button lock_3_increase_button;
	public Button lock_3_decrease_button;
	
	public Text lock_display_4;
	public Button lock_4_increase_button;
	public Button lock_4_decrease_button;
	
	public Button unlock;
	//
	//


    // Start is called before the first frame update
    void Start()
    {
		current_lock_1 = current_lock_2 = current_lock_3 = current_lock_4 = 0;
		
		display_hurt_flash = false;
		display_heal_flash = false;
		
		starting_hurt_color = Color.red;
		ending_hurt_color = hurt_flash.color;
		//starting_hurt_color.a = 0;
		
		starting_heal_color = Color.green;
		ending_heal_color = heal_flash.color;
		//starting_heal_color.a = 0;
		
		starting_tbc_color = tbc_screen.color;
		
        //MAKING THE CURSOR INVISIBLE
		Cursor.visible = false;
		
		
		
		//SETTING UP LOCK UI
		lock_1_increase_button.onClick.AddListener(increaseLockOne);
		lock_1_decrease_button.onClick.AddListener(decreaseLockOne);
		
		lock_2_increase_button.onClick.AddListener(increaseLockTwo);
		lock_2_decrease_button.onClick.AddListener(decreaseLockTwo);
		
		lock_3_increase_button.onClick.AddListener(increaseLockThree);
		lock_3_decrease_button.onClick.AddListener(decreaseLockThree);
		
		lock_4_increase_button.onClick.AddListener(increaseLockFour);
		lock_4_decrease_button.onClick.AddListener(decreaseLockFour);
		
		unlock.onClick.AddListener(lockMatch);
		
		lock_bundle.SetActive(false);
    }

    // Update is called once per frame
	void Update()
	{
		if (display_hurt_flash)
		{
			//setting up lerp for hurt
			current_color = Color.Lerp(starting_hurt_color, ending_hurt_color, Mathf.MoveTowards(0, 1, t));
			hurt_flash.color = current_color;
			
			t += 1.9f * Time.deltaTime;
		} 
		else if (display_heal_flash)
		{
			//setting up lerp for heal
			current_color = Color.Lerp(starting_heal_color, ending_heal_color, Mathf.MoveTowards(0, 1, t));
			heal_flash.color = current_color;
			
			t += 1.9f * Time.deltaTime;
		} 
		else if (display_tbc)
		{
			current_color = Color.Lerp(starting_tbc_color, ending_tbc_color, Mathf.MoveTowards(0,1,t));
			tbc_screen.color = current_color;
			
			t += 0.35f * Time.deltaTime;
		}
		
		if (text_timer == 0.0f && text_active){
			text_active = false;
			interaction_text.text = "";
		} else if (text_timer != 0.0f){
			text_timer -= Time.deltaTime;
			
			if (text_timer < 0.0f)
				text_timer = 0.0f;
		}
	}
	
	//For custom mouse
    void FixedUpdate()
    {
        //HAVE THE "CURSOR" FOLLOW THE REAL CURSOR'S POSITION
		
		//GET THE REAL CURSOR'S POSITION
		Ray mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 mouse_position = mouse_ray.origin;
		mouse_position.z = 0;
		
		cursor.transform.position = mouse_position;
		
		//Debug.Log(mouse_position);
		
		//In case of tabbing out
		if (Cursor.visible)
			Cursor.visible = false;
    }
	
	//Managing cursor image
	public void setCursorImage(int selection){
		switch (selection){
			//DEFAULT
			case 0:
				cursor.sprite = cursor_normal;
			break;
			//OBSERVE
			case 1:
				cursor.sprite = cursor_observe;
			break;
			//LOCK
			case 2:
				cursor.sprite = cursor_lock;
			break;
		}
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
	
	//Flashing screen
	public IEnumerator displayDamageFlash(){
		if (!display_hurt_flash)
		{
			//Debug.Log("Turning on damage");
			current_color = starting_hurt_color;
			display_hurt_flash = true;
			t = 0;
			
			yield return new WaitForSeconds(0.65f);
			
			display_hurt_flash = false;
		}
	}
	
	public IEnumerator displayHealFlash(){
		if (!display_heal_flash)
		{
			current_color = starting_heal_color;
			display_heal_flash = true;
			t = 0;
			
			yield return new WaitForSeconds(0.65f);
			
			display_heal_flash = false;
		}
	}
	
	public void displayTBC(){
		t = 0;
		
		display_tbc = true;
	}
	
	public void SetDeathOverlay(){
		death_screen.color = death_color;
	}
	
	//FOR OBSERVATION EVENTS
	public void setTextActive(float timeActive){
		text_timer = timeActive;
		text_active = true;
	}
	
	public void setTextLabel(string notification){
		interaction_text.text = notification;
	}
	
	public void setInteractionActive(){
		interaction_image.color = Color.white;
	}
	
	public void setInteractionInactive(){
		interaction_image.color = Color.clear;
	}
	
	public void setInteractionImageSprite(Sprite interaction_sprite, int sprite_x, int sprite_y){
		interaction_image.sprite = interaction_sprite;
		interaction_image.rectTransform.sizeDelta = new Vector2(sprite_x, sprite_y);
	}
	
	
	//FOR THE LOCK
	public void openComboLock(int key_1, int key_2, int key_3, int key_4, GameObject toOpen){
		current_lock_1 = current_lock_2 = current_lock_3 = current_lock_4 = 0;
		
		lock_display_1.text = lock_display_2.text = lock_display_3.text = lock_display_4.text = current_lock_1.ToString();
		
		lock_check_1 = key_1;
		lock_check_2 = key_2;
		lock_check_3 = key_3;
		lock_check_4 = key_4;
		
		lock_door_to_open = toOpen;
		
		
		//MAKING THE LOCK UI VISIBLE AND ACTIVE
		lock_bundle.SetActive(true);
	}
	
	public void closeComboLock(){
		//RESET THE LOCK COMBO
		lock_check_1 = lock_check_2 = lock_check_3 = lock_check_4 = 0;
		
		//HIDING THE UI AND SETTING INACTIVE
		lock_bundle.SetActive(false);
	}
	
	public void increaseLockOne(){
		current_lock_1+=1;
		
		if (current_lock_1 > 9)
			current_lock_1 = 0;
		
		lock_display_1.text = current_lock_1.ToString();
	}
	
	public void decreaseLockOne(){
		current_lock_1-=1;
		
		if (current_lock_1 < 0)
			current_lock_1 = 9;
		
		lock_display_1.text = current_lock_1.ToString();
	}
	
	public void increaseLockTwo(){
		current_lock_2+=1;
		
		if (current_lock_2 > 9)
			current_lock_2 = 0;
		
		lock_display_2.text = current_lock_2.ToString();
	}
	
	public void decreaseLockTwo(){
		current_lock_2-=1;
		
		if (current_lock_2 < 0)
			current_lock_2 = 9;
		
		lock_display_2.text = current_lock_2.ToString();
	}
	
	
	public void increaseLockThree(){
		current_lock_3+=1;
		
		if (current_lock_3 > 9)
			current_lock_3 = 0;
		
		lock_display_3.text = current_lock_3.ToString();
	}
	
	public void decreaseLockThree(){
		current_lock_3-=1;
		
		if (current_lock_3 < 0)
			current_lock_3 = 9;
		
		lock_display_3.text = current_lock_3.ToString();
	}
	
	
	public void increaseLockFour(){
		current_lock_4+=1;
		
		if (current_lock_4 > 9)
			current_lock_4 = 0;
		
		lock_display_4.text = current_lock_4.ToString();
	}
	
	public void decreaseLockFour(){
		current_lock_4-=1;
		
		if (current_lock_4 < 0)
			current_lock_4 = 9;
		
		lock_display_4.text = current_lock_4.ToString();
	}
	
	
	public void lockMatch(){
		if (current_lock_1 == lock_check_1 &&
			current_lock_2 == lock_check_2 &&
			current_lock_3 == lock_check_3 &&
			current_lock_4 == lock_check_4){
			
			setTextActive(2.0f);
			setTextLabel("Combination lock opened!");
			
			lock_door_to_open.SetActive(false);
			
			//CLOSE THE LOCK UI AND OPEN THE DOOR
			closeComboLock();
		} else {
			setTextActive(2.0f);
			setTextLabel("Combination not correct!");
		}
	}
}
