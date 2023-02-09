using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public io.newgrounds.core ngio_core;

    public GameObject platform;
    public GameObject gameOver;
    public GameObject mainMenu;
    public static UI instance;
    public int LChance = 2;
    public int tempp = 0;

    public Text GameOverPoints;
    public Text HighScore;
    public AudioSource BGMusic;

    public void NGSubmitScore(int score_id, int score){
        io.newgrounds.components.ScoreBoard.postScore submit_score = new io.newgrounds.components.ScoreBoard.postScore();
        submit_score.id = score_id;
        submit_score.value = score;
        submit_score.callWith(ngio_core);
        Debug.Log("Score Added!");
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver.gameObject.SetActive(false);
        instance = this;
        HighScore.text = "HIGH SCORES : " + PlayerPrefs.GetInt("HighScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("Game");
        }
    }

    public void PlayButton(){
        Time.timeScale = 1f;
        gameOver.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void Restart(){
        if(PlayerScript.instance.LifeInt > 0){
            gameOver.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            if(PlayerPrefs.GetInt("Sound") == 0){
                BGMusic.mute = true;
            }else{
                BGMusic.mute = false;
            }
            PlayerScript.instance.Reset();
            PlayerBound.instance.out_of_bounds = false;
            PlayerScript.instance.LifeInt--;
            PlayerScript.instance.Life.text = PlayerScript.instance.LifeInt.ToString();
        }
        else 
        {
            SceneManager.LoadScene("Game");
        }
    }

    public void Life(){
        Time.timeScale = 0f;
        SceneManager.LoadScene("Game");
    }

    public void GameOver(){
        BGMusic.mute = true;
        //Handheld.Vibrate();
        Time.timeScale = 0.0f;
        gameOver.gameObject.SetActive(true);
        GameOverPoints.text = PlayerScript.instance.scoreInt.ToString();
        if(PlayerScript.instance.scoreInt > PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore", PlayerScript.instance.scoreInt);
            NGSubmitScore(12534, PlayerScript.instance.scoreInt);
        }
        HighScore.text = "HIGH SCORES : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
