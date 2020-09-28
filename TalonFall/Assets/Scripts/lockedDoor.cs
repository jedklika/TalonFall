using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedDoor : MonoBehaviour
{
	GameManager gm;
	PlayerSound sound;
	
	public int lockValue;
	public string lockDesc;
	public string keyName;
	
	private bool debounce = false;
	private float debounce_time = 4.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
		sound = FindObjectOfType<PlayerSound>();
    }

    // Update is called once per frame
    void Update()
    {
        if (debounce){
			debounce_time -= Time.deltaTime;
			
			if (debounce_time < 0.0f)
				debounce_time = 0.0f;
			
			if (debounce_time == 0.0f)
				debounce = false;
		}
    }
	
	public void OnCollisionEnter2D(Collision2D col){
		//MAKE SURE COLLISION IS A PLAYER
		if (col.gameObject.CompareTag("Player") && !debounce){
			
			//CHECK IF KEY NEEDED EXISTS IN INVENTORY
			if (gm.checkKey(lockValue)){
				//OPEN DOOR
				sound.PlayUnlock();
				gm.notifyText(lockDesc);
				Destroy(gameObject);
			} else {
				//INDICATE LOCKED
				sound.PlayLock();
				gm.notifyText(keyName + " required");
			}
			
			debounce = true;
			debounce_time = 4.0f;
		}
	}
}
