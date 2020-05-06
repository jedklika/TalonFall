using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goto_game : MonoBehaviour
{
	public titlescreen_sounds sounds;
	public Image background;
	public Image cursor;
	
	public Button toGameButton;
	public Button tryMeButton;
	
	static float t = 0f;
	private bool to_game_transition;
	public Color transition_color_1;
	public Color transition_color_2;
	private Color current_color;
	
    // Start is called before the first frame update
    void Start()
    {
		transition_color_1 = current_color = background.color;
		sounds = FindObjectOfType<titlescreen_sounds>();
		
        toGameButton.onClick.AddListener(GoToGame);
		tryMeButton.onClick.AddListener(PlaySound);
		
		//MAKING THE CURSOR INVISIBLE
		Cursor.visible = false;
    }
	
	void Update()
	{
		if (to_game_transition)
		{
			current_color = Color.Lerp(transition_color_1, transition_color_2, Mathf.MoveTowards(0, 1, t));
			background.color = current_color;
			
			t += 1f * Time.deltaTime;
		}
	}
	
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
	
	void GoToGame()
	{
		sounds.TitleButtonClick();
		StartCoroutine(TransitioningToGameScene());
	}
	
	public IEnumerator TransitioningToGameScene()
	{
		to_game_transition = true;
		
		yield return new WaitForSeconds(1.5f);
		
		to_game_transition = false;
		
		//Fade in then go to the game
		SceneManager.LoadScene("GameScene");
	}
	
	void PlaySound()
	{
		sounds.TitleButtonClick();
	}
}
