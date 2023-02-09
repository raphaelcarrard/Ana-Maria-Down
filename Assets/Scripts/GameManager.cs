using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public GameObject ButtonR;
    public GameObject ButtonL;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        if(!PlayerPrefs.HasKey("First")){
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("First", 1);
            ButtonL.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(true);
            ButtonR.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(true);
            var tempc = ButtonL.GetComponent<Image>().color;
            tempc.a = 0.5f;
            ButtonL.GetComponent<Image>().color = tempc;
            tempc = ButtonR.GetComponent<Image>().color;
            tempc.a = 0.5f;
            ButtonR.GetComponent<Image>().color = tempc;
            StartCoroutine("startwait");
        }
    }

    public void RestartGame(){
        Invoke("restartAfterTime", 2f);
    }

    void restartAfterTime(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    IEnumerator startwait(){
        yield return new WaitForSeconds(5f);
        ButtonL.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
        ButtonR.GetComponent<Transform>().GetChild(0).transform.gameObject.SetActive(false);
        var tempc = ButtonL.GetComponent<Image>().color;
        tempc.a = 0f;
        ButtonL.GetComponent<Image>().color = tempc;
        tempc = ButtonR.GetComponent<Image>().color;
        tempc.a = 0f;
        ButtonR.GetComponent<Image>().color = tempc;
    }
}
