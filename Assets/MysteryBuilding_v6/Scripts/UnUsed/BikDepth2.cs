using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikDepth2 : MonoBehaviour {

     GameObject Player;
    public float offset = 1;
    float posZ;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        posZ = transform.position.z;

    }
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<Renderer>().isVisible)
            return;

        if (Player.transform.position.y <= transform.position.y)
        {
           // Debug.Log("Higher");
            transform.position = new Vector3(transform.position.x, transform.position.y, posZ + offset);
        }
        else {
          //  Debug.Log("Lower");
            transform.position = new Vector3(transform.position.x, transform.position.y, posZ - offset);
        }
    }
}
