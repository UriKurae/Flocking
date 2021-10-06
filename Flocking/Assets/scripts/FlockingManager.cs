using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject[] allFish;
    public GameObject prefab;

    public Vector3 SwimLimits;
   
    public bool bounded;
    public bool randomize;
    public bool followLider;

    public GameObject lider;

    public float minSpeed = 2.0f;
    public float maxSpeed = 5.0f;
    public float neighbourDistance = 2.0f;
    public float rotationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Flocking();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Flocking()
    {
        for (int i = 0; i < allFish.Length; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            Vector3 randomize = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f));
            allFish[i] = (GameObject)Instantiate(prefab, pos, Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }
}
