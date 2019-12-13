using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class BallReset : MonoBehaviour {

    private Vector3 initialpos;
    private Rigidbody rig;
    private GameObject[] stars;
    private bool completed;
    public string nextLevelNAme;
    // Use this for initialization
    void Start () {
        completed = false;
        initialpos = transform.position;
        rig = GetComponent<Rigidbody>();
        stars = GameObject.FindGameObjectsWithTag("Collectable");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            transform.position = initialpos;
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            //GameObject[] stars = GameObject.FindGameObjectsWithTag("Collectable");
            foreach(GameObject star in stars)
            {
                //Debug.Log("Here");
                star.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "Goal")
        {
            completed = true;
            foreach (GameObject star in stars)
            {
                if (star.activeSelf)
                {
                    completed = false;
                }
            }

            if (completed)
            {
                Debug.Log("Game Done");
                //Load the new scene in here
                SteamVR_LoadLevel.Begin(nextLevelNAme);
            }
        }
    }
}
