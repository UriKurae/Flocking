using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharkController : MonoBehaviour
{
    public FlockingManager flockingManager;
    public float speed = 5.0f;
    public bool move = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            move = true;
        }

        if (move)
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;

            Debug.Log(Vector3.Distance(this.transform.position, flockingManager.transform.position));

            if (Vector3.Distance(this.transform.position, flockingManager.transform.position) <= flockingManager.dangerZone)
            {
                flockingManager.danger = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "bounds")
        {
            this.transform.position = new Vector3(12.0f, 3.0f, 27.0f);
            move = false;
        }
    }
}
