using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public static PlatformScript Instance;
    public float move_speed;
    public float bound_y = 6f;
    public bool moving_platform_left, moving_platform_right, is_breakable, is_spike, is_platform;
    private Animator anim;
    public int score;

    private void Awake(){
        if(is_breakable){
            anim = GetComponent<Animator>();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        move_speed = PlayerScript.instance.platformMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move(){
        Vector2 temp = transform.position;
        temp.y += move_speed * Time.deltaTime;
        transform.position = temp;
        if(temp.y >= bound_y){
            gameObject.SetActive(false);
        }
    }

    void DeactivateGameObject(){
        SoundManager.instance.iceBreakSound();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "Player"){
            if(is_spike){
                target.transform.position = new Vector2(1000f, 1000f);
                SoundManager.instance.DeathSound();
                UI.instance.GameOver();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D target) {
        if(target.gameObject.tag == "Player"){
            SoundManager.instance.landSound();
            if(is_breakable){
                anim.SetBool("Break", true);
                Destroy(this.gameObject, 1.5f);
            }
            if(is_platform){
                if(this.transform.position.y > PlayerBound.instance.min_Y){
                    PlayerScript.instance.OnPlatform = this.gameObject;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D target) {
        if(target.gameObject.tag == "Player"){
            if(moving_platform_left){
                target.gameObject.GetComponent<PlayerScript>().platformMove(-1f);
            }
            if(moving_platform_right){
                target.gameObject.GetComponent<PlayerScript>().platformMove(1f);
            }
        }
    }
}
