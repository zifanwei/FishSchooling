using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GlobalFlock myFlock;
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    static int numOfFish = 10;
    public static GameObject[] allFish = new GameObject[numOfFish];
    public static int tankSize = 1;
    public static Vector3 goalPosition = Vector3.zero;
    public void FishSpeed(float speedMult)
    {
        Debug.Log(speedMult);
        for(int i = 0; i<numOfFish; i++)
        {
            allFish[i].GetComponent<Flock>().speedMult = speedMult;
        }
    }
    void Start()
    {
        myFlock = this;
        goalPosition = this.transform.position;
        //setting fog effect
        //RenderSettings.fogColor = Camera.main.backgroundColor;
        //RenderSettings.fogDensity = 0.03F;
        //RenderSettings.fog = true;
        for(int i = 0; i< numOfFish; i++)
        {
            // create a position of fish
            Vector3 position = new Vector3(Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize),
                                           Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fishPrefab, position, Quaternion.identity);
            //allFish[i].GetComponent<Flock>().myManager = this;
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
            goalPrefab.transform.position = goalPosition;
        }
    }
}
