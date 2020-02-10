using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fishPrefab;
    static int numOfFish = 10;
    public static GameObject[] allFish = new GameObject[numOfFish];
    public static int tankSize = 5;
    public static Vector3 goalPosition = Vector3.zero;
    void Start()
    {
        for(int i = 0; i< numOfFish; i++)
        {
            // create a position of fish
            Vector3 position = new Vector3(Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //reset the movement randomly
        if (Random.Range(0, 10000) < 50)
        {
            goalPosition = new Vector3(Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize));
        }
    }
}
