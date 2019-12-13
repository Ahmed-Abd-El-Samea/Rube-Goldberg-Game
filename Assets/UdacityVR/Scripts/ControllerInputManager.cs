using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerInputManager : MonoBehaviour {



    //public SteamVR_ActionSet activateActionSetOnAttach;
    public SteamVR_Action_Boolean istouching;
    public SteamVR_Action_Boolean instansiatePrefab;
    public SteamVR_Action_Vector2 touchPosition;
    public SteamVR_Action_Boolean isClicking;
    public GameObject objectMenu;
    private List<GameObject> menuItems = new List<GameObject>();
    public List<GameObject> prefabs = new List<GameObject>();
    public List<int> ObjectsSizes = new List<int>();
    private int index;
    private float waitingTime, passedTime;
    // Use this for initialization
    void Start () {
        if (!objectMenu)
        {
            objectMenu = GameObject.FindGameObjectWithTag("Menu").transform.GetChild(0).gameObject;
        }
        waitingTime = 0.5f;
        passedTime = waitingTime;
        index = 0;
        for(int i = 0; i < objectMenu.transform.childCount; i++)
        {
            menuItems.Add(objectMenu.transform.GetChild(i).gameObject);
        }
        DisableAllObject();
    }
	
	// Update is called once per frame
	void Update () {
        passedTime -= Time.deltaTime;
        if (objectMenu.activeSelf)
        {
            if (!istouching.GetState(SteamVR_Input_Sources.RightHand))
            {
                objectMenu.SetActive(false);
            }
            if (isClicking.GetState(SteamVR_Input_Sources.RightHand) && passedTime <= 0)
            {
                passedTime = waitingTime;
                if (touchPosition.GetAxis(SteamVR_Input_Sources.RightHand).x > 0)
                    index++;
                else
                {
                    if (index == 0) index = menuItems.Count -1;
                    else index--;
                }
                index = index % 4;
                enableCorrespondingObject();
            }

            if (instansiatePrefab.GetState(SteamVR_Input_Sources.RightHand) && passedTime <= 0)
            {
                passedTime = waitingTime;
                if (ObjectsSizes[index] > 0)
                {
                    Instantiate(prefabs[index], menuItems[index].transform.position, menuItems[index].transform.rotation);
                    ObjectsSizes[index]--;
                }
            }
            
           
            
        }
        else
        {
            if (istouching.GetState(SteamVR_Input_Sources.RightHand))
            {
                objectMenu.SetActive(true);
                menuItems[index].SetActive(true);
            }
        }
    }

    public void enableCorrespondingObject()
    {
        foreach(var obj in menuItems)
        {
            obj.SetActive(false);
        }
        menuItems[index].SetActive(true);
    }

    public void DisableAllObject()
    {
        foreach (var obj in menuItems)
        {
            obj.SetActive(false);
        }
    }
}
