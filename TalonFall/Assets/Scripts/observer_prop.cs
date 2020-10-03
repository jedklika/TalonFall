using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class observer_prop : MonoBehaviour
{
	public bool has_image;
	public Sprite observe_image;
	public int sprite_x, sprite_y;
	public string observe_description;
	
	GameManager gm;
	
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
	
    void Update()
    {
		Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mouse_point = new Vector2(mouse.x, mouse.y);
		RaycastHit2D hit = Physics2D.Raycast(mouse_point, Vector2.zero);
		
		if (hit){
			//CHECK IF OVER THAT PROP
			//Debug.Log(hit.collider.name + " " + this.name);
			
			//This is not an approach I like, but hey, overengineered problems require overengineered solution.
			if (hit.collider.name == this.name){
				gm.setCursorState(1);
				
				//Checking mouse click
				if (Input.GetMouseButtonDown(0)){
					clickOnProp();
				}
				
				return;
			}
		}
		
		gm.setCursorState(0);
    }
	
	private void clickOnProp(){
		if (gm.canInteract()){
			if(has_image)
				gm.interactionOpen(observe_image, sprite_x, sprite_y);
			gm.notifyText(observe_description);
		}
	}
}
