using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuControl : MonoBehaviour {

	public Sprite soundOn;
	public Sprite soundOff;
	public Button soundButton;

	public Text highScoreMenuText;
	public Text highLevelMenuText;
	private int highScoreMenu;

	public static bool muteSound = true;
	public static bool newGame = false;

	public static MenuControl instance = null;

	void Awake(){
        if (instance == null)
			instance = this;

		Setup ();
	}


	void Setup(){

		highScoreMenuText.text = "Highscore: " + PlayerPrefs.GetInt ("highscore");
		highLevelMenuText.text = "Level: " + PlayerPrefs.GetInt ("highscoreLevel");

		if (muteSound) {
			soundButton.GetComponent<Button> ().image.overrideSprite = soundOn;
		} else {
			soundButton.GetComponent<Button>().image.overrideSprite = soundOff;
		}	
	}

	void NewGameBtn(){
        newGame = true;
		SceneManager.LoadScene(1);
	}
	void LoadGameBtn(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));

	}

	public void ExitBtn(){
		Application.Quit();
	}
	int i = 0;


	public void ChangeVolume(){
		muteSound = ! muteSound;

		if (MenuControl.muteSound) {
			PlayerPrefs.SetInt ("SoundOption", 0);
			soundButton.GetComponent<Button> ().image.overrideSprite = soundOn;
		} else {
			soundButton.GetComponent<Button>().image.overrideSprite = soundOff;
			PlayerPrefs.SetInt ("SoundOption", 1);

		}
			
	}


}
