using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class observer_prop : MonoBehaviour
{
	public bool has_image;
	public Sprite observe_image;
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
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		
		if (hit){
			gm.setCursorState(1);
		} else {
			gm.setCursorState(0);
		}
			
        //Checking mouse click
		if (Input.GetMouseButtonDown(0)){
			if (hit){
				clickOnProp();
			}
		}
		
    }
	
	private void clickOnProp(){
		if (gm.canInteract()){
			if(has_image)
				gm.interactionOpen(observe_image);
			gm.notifyText(observe_description);
		}
	}
}
