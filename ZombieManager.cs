using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ZombieManager instance;

    void Start()
    {
       
        Instance();
        player = GameObject.Find("Player");
    }

    void Instance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private float playerX, playerZ;
    private GameObject player;
    public GameObject zombie1, zombie2;
    public bool isNearZoneZombie1, isNearZoneZombie2;
    public float thresholdX = 30, thresholdZ = 30;

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        float playerY = player.transform.position.y;
        float thresholdY = 1f;

        if (!zombie1.Equals(null))
        {
            if (zombie1.transform.position.y - thresholdY < playerY &&
                playerY < zombie1.transform.position.y + thresholdY)
            {
                if (zombie1.transform.position.x - thresholdX < playerX &&
                    playerX < zombie1.transform.position.x + thresholdX &&
                    zombie1.transform.position.z - thresholdZ < playerZ &&
                    playerZ < zombie1.transform.position.z + thresholdZ)
                {
                    isNearZoneZombie1 = true;
                }
                else
                {
                    isNearZoneZombie1 = false;
                }
            }
        }


        if (!zombie2.Equals(null))
        {
            if (zombie2.transform.position.y - thresholdY < playerY &&
                playerY < zombie2.transform.position.y + thresholdY)
            {
                if (zombie2.transform.position.x - thresholdX < playerX &&
                    playerX < zombie2.transform.position.x + thresholdX &&
                    zombie2.transform.position.z - thresholdZ < playerZ &&
                    playerZ < zombie2.transform.position.z + thresholdZ)
                {
                    isNearZoneZombie2 = true;
                }
                else
                {
                    isNearZoneZombie2 = false;
                }
            }
        }
    }
}