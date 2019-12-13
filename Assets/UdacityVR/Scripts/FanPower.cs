using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPower : MonoBehaviour {

    //private bool isStopped;
    private Rigidbody rig;
	// Use this for initialization
	void Start () {
        //isStopped = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Throwable")
        {
            Debug.Log("Entered");
            rig = other.GetComponent<Rigidbody>();
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
            //isStopped = true;
        }
        else
        {
            Debug.Log("Something else is Entering");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Throwable")
        {
            Debug.Log("Staying and doing something");
            rig.AddForce(-transform.forward * 20);
        }
        else
        {
            Debug.Log("Something else is staying");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rig = null;
    }


}
