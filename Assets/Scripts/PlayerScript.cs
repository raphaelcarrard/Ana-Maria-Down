using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;

    public GameObject OnPlatform;

    public Animator SpikeAnim;

    public Text score;
    public int scoreInt;
    public Text Life;
    public int LifeInt;
    private Rigidbody2D mybody;
    public float moveSpeed;
    public float platformMoveSpeed;
    public float BGMoveSpeed;
    public int playerScore;
    bool left, right;

    void Awake(){
        Time.timeScale = 0f;
        instance = this;
        scoreInt = 0;
        LifeInt = 3;
        Life.text = LifeInt.ToString();
        playerScore = 75;
        score.text = scoreInt.ToString();
        StartCoroutine("scoreincrement");
        left = right = false;
        mybody = GetComponent<Rigidbody2D>();
    }

    public void Reset(){
        mybody.velocity = Vector2.zero;
        if(OnPlatform != null && OnPlatform.transform.position.y < 2f){
            this.transform.position = new Vector2(OnPlatform.transform.position.x, OnPlatform.transform.position.y + 0.5f);
        }else{
            GameObject [] g= GameObject.FindGameObjectsWithTag("Platform");
            GameObject newtile = Instantiate(UI.instance.platform);
            for(int i = 0; i < g.Length; i++){
                if((g[i].transform.position.y > newtile.transform.position.y - 1.5 && g[i].transform.position.y <= newtile.transform.position.y) || (g[i].transform.position.y < newtile.transform.position.y + 1.5 && g[i].transform.position.y >= newtile.transform.position.y)){
                    Destroy(g[i].gameObject);
                }
            }
            this.transform.position = new Vector2(0f, -3.5f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Time.timeScale = 0f;
        }
        if(Input.GetKey(KeyCode.Space)){
            SpikeAnim.SetBool("Animate", true);
        }
        Move();
        if(scoreInt == playerScore){
            BGMoveSpeed += 0.05f;
            moveSpeed += 0.2f;
            platformMoveSpeed += 0.1f;
            playerScore += 75;
        }
    }

    private void Move(){
        if(Input.GetAxisRaw("Horizontal") > 0f || right){
            mybody.velocity = new Vector2(moveSpeed, mybody.velocity.y);
        }
        if(Input.GetAxisRaw("Horizontal") < 0f || left){
            mybody.velocity = new Vector2(-moveSpeed, mybody.velocity.y);
        }
    }

    public void platformMove(float x){
        mybody.velocity = new Vector2(x, mybody.velocity.y);
    }

    public void moveRight(){
        right = true;
    }

    public void moveLeft(){
        left = true;
    }

    public void setFalse(){
        right = false;
        left = false;
    }

    IEnumerator scoreincrement(){
        yield return new WaitForSeconds(0.2f);
        scoreInt++;
        score.text = "Points: " + scoreInt.ToString();
        StartCoroutine("scoreincrement");
    }
}
