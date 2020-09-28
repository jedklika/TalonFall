using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyData : MonoBehaviour
{
	public int keyValue;
	public string keyName;
	
	GameManager gm;
	PlayerSound sound;
	
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
		sound = FindObjectOfType<PlayerSound>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnCollisionEnter2D(Collision2D col){
		//if player touched this
		if (col.gameObject.CompareTag("Player")){
			gm.addKey(keyValue);
			gm.notifyText(keyName + " obtained!");
			sound.PlayKeyCollect();
			Destroy(gameObject);
		}
	}
}
