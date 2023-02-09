using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBound : MonoBehaviour
{

    public static PlayerBound instance;

    public float min_X = -2.6f,
        max_X = 2.6f,
        min_Y = -5.6f, max_Y = 5.6f;
    public bool out_of_bounds;
    public int LChance = 2;

    // Start is called before the first frame update
    private void Start()
    {
        min_X = -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        max_X = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }

    void CheckBounds(){
        Vector2 temp = transform.position;
        if(temp.x > max_X)
            temp.x = max_X;
        if(temp.x < min_X)
            temp.x = min_X;
        transform.position = temp;

        if(temp.y <= min_Y){
            if(!out_of_bounds){
                out_of_bounds = true;
                SoundManager.instance.DeathSound();
                UI.instance.GameOver();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if(target.tag == "TopSpike"){
            transform.position = new Vector2(1000f, 1000f);
            SoundManager.instance.DeathSound();
            UI.instance.GameOver();
        }
    }
}
