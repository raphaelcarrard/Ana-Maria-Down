using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public static PlatformSpawner Instance;

    public GameObject platformPrefab;
    public GameObject spikePlatformPrefab;
    public GameObject[] moving_Platforms;
    public GameObject breakablePlatform;

    public float platform_spawn_timer;
    private float current_platform_spawn_timer;
    private int platform_spawn_count;
    public float min_X = -2f, max_X = 2f;

    int speedIncreaser;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        current_platform_spawn_timer = platform_spawn_timer;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPlatform();
    }

    void spawnPlatform(){
        current_platform_spawn_timer += Time.deltaTime;
        if(current_platform_spawn_timer >= platform_spawn_timer){
            platform_spawn_count++;
            Vector3 temp = transform.position;
            temp.x = Random.Range(min_X, max_X);
            GameObject newPlatform = null;
            if(platform_spawn_count < 2){
                newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
            }
            else if(platform_spawn_count == 2)
            {
                if(Random.Range(0, 2) > 0){
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(moving_Platforms[Random.Range(0, moving_Platforms.Length)], temp, Quaternion.identity);
                }
            }
            else if(platform_spawn_count == 3){
                if(Random.Range(0, 2) > 0){
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(spikePlatformPrefab, temp, Quaternion.identity);
                }
            }
            else if(platform_spawn_count == 4){
                if(Random.Range(0, 2) > 0){
                    newPlatform = Instantiate(platformPrefab, temp, Quaternion.identity);
                }
                else
                {
                    newPlatform = Instantiate(breakablePlatform, temp, Quaternion.identity);
                }
                platform_spawn_count = 0;
            }
            if(newPlatform)
            newPlatform.transform.parent = transform;
            current_platform_spawn_timer = 0f;
        }
    }
}
