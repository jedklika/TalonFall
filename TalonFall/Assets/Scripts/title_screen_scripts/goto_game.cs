using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goto_game : MonoBehaviour
{
	public Button toGameButton;
	
    // Start is called before the first frame update
    void Start()
    {
        toGameButton.onClick.AddListener(GoToGame);
    }

	void GoToGame()
	{
		//Going to game
		SceneManager.LoadScene("GameScene");
	}
}
