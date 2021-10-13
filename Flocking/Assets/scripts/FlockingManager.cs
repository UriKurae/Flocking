using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject[] allFish;
    public GameObject prefab;

    public Vector3 SwimLimits;

    public bool followLider;
    public bool danger;

    public GameObject lider;

    public float minSpeed;
    public float maxSpeed;
    public float neighbourDistance;
    public float rotationSpeed;

    // Zones of action
    public float dangerZone;
    public float liderZone;

    // Start is called before the first frame update
    void Start()
    {
        minSpeed = 1.5f;
        maxSpeed = 2.0f;
        neighbourDistance = 50.0f;
        rotationSpeed = 0.5f;
        dangerZone = 5.0f;
        liderZone = 10.0f;

        SetUp();
    }

    void SetUp()
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
