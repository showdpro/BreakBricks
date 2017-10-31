using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
	// -- CONSTANT --
	private const int START_LIVES = 3;
	private const int START_LEVEL = 1;
	private int MAX_LEVELS;
	private const float RESET_DELAY  = 0.5f;

	// -- GAME CONTROL -- 
	public static int lives = START_LIVES;
	public static int bricks;
	public static int bricksStart;
    public static int points = 0;
	public static int level = START_LEVEL;
	public static int highscore;
	public static int highscoreLevel;
	public Button soundButton;
 
	public Text livesText;
	public Text pointText;
	public Text levelText;
	public Text highscoreText;
	public Text brickCounterText;

	public Sprite soundOn;
	public Sprite soundOff;

	public GameObject bricksPrefab;
	public GameObject paddle;
	public GameObject deathParticles;
	private GameObject[] getCount;
	private GameObject clonePaddle;

	public static GM instance = null;

	// -- CONFIGS --

	public AudioSource levelSound;

    public void Awake()
	{

        LoadSettings ();
		paddle.SetActive(true);

        livesText.text = "" + lives;
        pointText.text = "" + points;
		levelText.text = "" + level;
		highscore = PlayerPrefs.GetInt ("highscore");
		highscoreLevel = PlayerPrefs.GetInt ("highscoreLevel");
		highscoreText.text = "Highscore: " + highscore + " ("+highscoreLevel+")";
		MAX_LEVELS = (SceneManager.sceneCountInBuildSettings) - 1;


        if (instance == null) 
		{
			instance = this;
		}
        else if (instance != this)
            Destroy(gameObject);
		
		Setup ();
	}

	void Start() {
        AdManager.Instance.ShowBanner();
        getCount = GameObject.FindGameObjectsWithTag ("Brick");
		bricks = getCount.Length;
		bricksStart = getCount.Length;
		brickCounterText.text = "" + bricks + "/" + bricksStart;
	}

    public void Setup()
    {	
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
        Instantiate(bricksPrefab, transform.position, Quaternion.identity);

    }

    void CheckGameOver()
    {

        if (bricks < 1)
        {
			paddle.SetActive(false);
			Invoke("NextLevel",RESET_DELAY);
        }

        if (lives < 1)
		{
			CheckHighScore ();
			paddle.SetActive(false);
			//Call Menu Scene
			Reset ();
			CallMenu ();
        }

    }

    void NextLevel()
    {
		if (MenuControl.muteSound) {
			levelSound.GetComponent<AudioSource> ().Play ();
		}

		paddle.SetActive(true);
        level++;

        if (level <= MAX_LEVELS){
			bricks = bricksStart;
            PlayerPrefs.SetInt("Level", level);
            SaveSettings();
            SceneManager.LoadScene(level);

        }
        else{
			level--;
			bricks = bricksStart;
            PlayerPrefs.SetInt("Level", level);
            SaveSettings();
            SceneManager.LoadScene(level);
        }
    }

	public void Reset(){
		paddle.SetActive(true);
		bricks = bricksStart;
        level = START_LEVEL;
        points = 0;
		lives = START_LIVES;
		SaveSettings ();
        SceneManager.LoadScene(level);
    }

    public void SetPoints(){
        points++;
        pointText.text = "" + points;
    }

    public void LoseLife(){
        lives--;
        livesText.text = "" + lives;
	
        Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy(clonePaddle);
		Invoke("SetupPaddle", RESET_DELAY);
        CheckGameOver();
    }

    void SetupPaddle()
    {
        clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
    }

    public void DestroyBrick()
    {
        bricks--;
        CheckGameOver();
		brickCounterText.text = "" + bricks + "/" + bricksStart;
    }

	public void CheckHighScore(){
		if (points > highscore) {
			highscore = points;
			PlayerPrefs.SetInt ("highscore", highscore);
		}
		if (level > highscoreLevel) {
			highscoreLevel = level;
			PlayerPrefs.SetInt ("highscoreLevel", highscoreLevel);
		}
	}

	public void OnDestroy(){
		CheckHighScore ();
	}

	public void SaveSettings(){
		CheckHighScore ();
		if (MenuControl.muteSound) {
			PlayerPrefs.SetInt ("SoundOption", 0);
		} else {
			PlayerPrefs.SetInt ("SoundOption", 1);
		}
		PlayerPrefs.SetInt ("Level", level);
		PlayerPrefs.SetInt ("Lives", lives);
	}

	public void LoadSettings(){
		if (PlayerPrefs.GetInt ("SoundOption") == 0) {
			MenuControl.muteSound = true;
			soundButton.GetComponent<Button> ().image.overrideSprite = soundOn;
		} else {
			MenuControl.muteSound = false;
			soundButton.GetComponent<Button>().image.overrideSprite = soundOff;
		}

		level = PlayerPrefs.GetInt ("Level");
		lives = PlayerPrefs.GetInt ("Lives");
		highscore = PlayerPrefs.GetInt ("hightsore");
		highscoreLevel = PlayerPrefs.GetInt ("hightsoreLevel");

		if (MenuControl.newGame) {
			Reset ();
			MenuControl.newGame = false;
		}
	}

	public void CallMenu(){
		SaveSettings ();
		points -= (bricksStart - bricks);
		bricks = bricksStart;
		SceneManager.LoadScene(0);
	}


	public void MuteGame(){
		MenuControl.muteSound = !MenuControl.muteSound;
		if (MenuControl.muteSound) {
			PlayerPrefs.SetInt ("SoundOption", 0);
			soundButton.GetComponent<Button> ().image.overrideSprite = soundOn;
		} else {
			PlayerPrefs.SetInt ("SoundOption", 1);
			soundButton.GetComponent<Button>().image.overrideSprite = soundOff;
		}	
	}
	public int GetHighScore ()
	{
		return highscore;

	}

    public void SetLifeBuff()
    {
        lives++;
        livesText.text = "" + lives;
    }

    public void SetScoreBuff()
    {
        points += 10;
        pointText.text = "" + points;
    }

}