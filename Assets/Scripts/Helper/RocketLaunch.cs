using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLaunch : MonoBehaviour
{
    public GameObject[] rocketParts;
    private bool partAdded;
    private bool launched = false;
    void Update()   
    {
        if (rocketLaunch() == true && launched == false)
        {
            gameObject.AddComponent<ConstantForce>().force = new Vector3(0f, 1500f, 0f);
            launched = true;
        }
    }
    private bool rocketLaunch()
    {
        for (int i = 0; i < rocketParts.Length; i++)
        {
            partAdded = rocketParts[i].GetComponent<SnapObject>().isSnapped;
            if (partAdded == false)
            {
                return false;
            }
        }
        return true;
    }
}
