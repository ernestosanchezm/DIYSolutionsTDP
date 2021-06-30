using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public GameObject SnapLocation;
    public GameObject rocket;
    public bool isSnapped;
    private bool objectSnapped;
    private bool grabbed;
    public Material CurrentColor;
    void Update()
    {
        grabbed = GetComponent<OVRGrabbable>().isGrabbed;
        objectSnapped = SnapLocation.GetComponent<SnapToLocation>().Snapped;
        if (objectSnapped == true)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(rocket.transform);
            isSnapped = true;

            SnapLocation.GetComponent<Renderer>().material = CurrentColor;
            var objetosHijo = SnapLocation.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
            if (objetosHijo != null)
            {
                foreach (var item in objetosHijo)
                {
                    item.material = CurrentColor;
                }
            }
        }
        if (objectSnapped == false && grabbed == false)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
