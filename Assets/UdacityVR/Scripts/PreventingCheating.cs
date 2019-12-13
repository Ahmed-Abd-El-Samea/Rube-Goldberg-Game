using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventingCheating : MonoBehaviour {


    private bool isHoldingBall;
    public GameObject Ball;
    bool isCheating;
    private Vector3 initialPose;
    private Rigidbody rig;
    public static PreventingCheating cheating;
	// Use this for initialization
	void Start () {
        cheating = this;
        initialPose = Ball.transform.position;
        rig = Ball.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isHoldingBall)
        {
            if(Ball.transform.position.z > 0 || Ball.transform.position.z < -3 || Ball.transform.position.x > 1.3 || Ball.transform.position.x < -1.3)
            {
                Ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                isCheating = true;
            }
            else
            {
                Ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                isCheating = false;
            }
        }
        else
        {
            Ball.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
            isCheating = false;
        }
	}


    public void changeBallState(bool x)
    {
        isHoldingBall = x;
    }

    public void handleCheating()
    {
        if (isCheating)
        {
            Debug.Log("You Cheated");
            Ball.transform.position = initialPose;
            rig.velocity = Vector3.zero;
            rig.angularVelocity = Vector3.zero;
        }
    }
}
