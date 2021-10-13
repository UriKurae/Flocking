using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager myManager;

    public float speed = 5.0f;
    public Vector3 direction;

    int numCohesion;
    int numAlign;
    int numDanger;
    int numLider;

    float freq = 0f;

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq = 0.0f;
            direction = Flocking().normalized * speed;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                          Quaternion.LookRotation(direction),
                                          myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    Vector3 Flocking()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 align = Vector3.zero;
        Vector3 danger = Vector3.zero;
        Vector3 lider = Vector3.zero;
        Vector3 separation = Vector3.zero;

        numCohesion = 0;
        numAlign = 0;
        numDanger = 0;
        numLider = 0;

        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
              cohesion += Cohesion(go);
              align += Align(go);
              danger += FollowLider(go);
              lider += FleeFromDanger(go);
              separation += Separation(go);
            }
        }

        if (numCohesion > 0)
            cohesion = (cohesion / numCohesion - transform.position).normalized * speed;

        if (numAlign > 0)
        {
            align /= numAlign;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }

        if (numLider == myManager.allFish.Length) myManager.followLider = false;

        if (numDanger == myManager.allFish.Length) myManager.danger = false;

        return cohesion + align + separation + danger + lider;
    }

    Vector3 Cohesion(GameObject go)
    {
        Vector3 cohesion = Vector3.zero;

                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    numCohesion++;
                }

        return cohesion;
    }

    Vector3 Align(GameObject go)
    {
        Vector3 align = Vector3.zero;

        float distance = Vector3.Distance(go.transform.position, transform.position);
        if (distance <= myManager.neighbourDistance)
        {
           align += go.GetComponent<Flock>().direction;
           numAlign++;
        }
        return align;
    }

    Vector3 FollowLider(GameObject go)
    {
        Vector3 follow = Vector3.zero;
      
        if (myManager.followLider)
        {
          
          float distance = Vector3.Distance(go.transform.position, myManager.lider.transform.position);

          // Chase
          if (distance <= myManager.liderZone)
          {
            follow -= (transform.position - myManager.lider.transform.position) / (distance * distance);
          }
          else
          {
            numLider++;
          }
        }
        return follow;
    }

    Vector3 FleeFromDanger(GameObject go)
    {
        Vector3 flee = Vector3.zero;

        if (myManager.danger)
        {
           
           float distance = Vector3.Distance(go.transform.position, myManager.transform.position);

           // Escape
           if (distance <= myManager.dangerZone)
           {
             flee += (transform.position - myManager.transform.position) / (distance * distance);
           }
           else
           {
             numDanger++;
           }
        }

        return flee;
    }

    Vector3 Separation(GameObject go)
    {
        Vector3 separation = Vector3.zero;
       
         float distance = Vector3.Distance(go.transform.position, transform.position);

         if (distance <= myManager.neighbourDistance)
             separation -= (transform.position - go.transform.position) / (distance * distance);

        return separation;
    }
}
