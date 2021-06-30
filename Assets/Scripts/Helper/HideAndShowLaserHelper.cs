using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndShowLaserHelper : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    private void Start()
    {
        laser.gameObject.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.RTouch) > 0.0f)
        {
            laser.gameObject.SetActive(true);
        }
        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.RTouch) == 0.0f)
        //{
        //    laser.gameObject.SetActive(false);
        //}
    }
}
