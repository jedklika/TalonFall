using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class goto_game : MonoBehaviour
{
	public titlescreen_sounds sounds;
	public titlescreen_music music;
	
	public Image background;
	public Image cursor;
	
	public Button toGameButton;
	public Image toGameButtonImage;
	public Button tryMeButton;
	public Image tryMeButtonImage;
	
	public Text textOne;
	public Text textTwo;
	public Text buttonTextOne;
	public Text buttonTextTwo;
	
	static float t = 0f;
	private bool to_game_transition = false;
	private bool to_game_title_transition = false;
	private Color bg_transition_color_1;
	public Color bg_transition_color_2;
	public Color title_transition_color_1;
	public Color title_transition_color_2;
	private Color current_color;
	
    // Start is called before the first frame update
    void Start()
    {
		bg_transition_color_1 = current_color = background.color;
		sounds = FindObjectOfType<titlescreen_sounds>();
		
        toGameButton.onClick.AddListener(GoToGame);
		tryMeButton.onClick.AddListener(PlaySound);
		
		//MAKING THE CURSOR INVISIBLE
		Cursor.visible = false;
		
		music.PlayTitle();
    }
	
	void Update()
	{
		if (to_game_transition)
		{
			current_color = Color.Lerp(bg_transition_color_1, bg_transition_color_2, Mathf.MoveTowards(0, 1, t));
			background.color = current_color;
			cursor.color = current_color;
			
			t += 1.4f * Time.deltaTime;
		}
		else if (to_game_title_transition)
		{
			current_color = Color.Lerp(title_transition_color_1, title_transition_color_2, Mathf.MoveTowards(0, 1, t));
			textOne.color = current_color;
			textTwo.color = current_color;
			cursor.color = current_color;
			toGameButtonImage.color = current_color;
			tryMeButtonImage.color = current_color;
			buttonTextOne.color = current_color;
			buttonTextTwo.color = current_color;
			
			
			t += 1.6f * Time.deltaTime;
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
		if (!to_game_transition && !to_game_title_transition)
		{
			sounds.TitleButtonClick();
			StartCoroutine(TransitioningToGameScene());
		}
	}
	
	public IEnumerator TransitioningToGameScene()
	{
		to_game_transition = true;
		
		yield return new WaitForSeconds(1.1f);
		
		to_game_transition = false;
		
		t = 0.2f;
		
		to_game_title_transition = true;
		
		music.StopMusic();
		
		yield return new WaitForSeconds(0.5f);
		
		//Fade in then go to the game
		SceneManager.LoadScene("GameScene");
	}
	
	void PlaySound()
	{
		sounds.TitleButtonClick();
		EventSystem.current.SetSelectedGameObject(null);
	}
}
