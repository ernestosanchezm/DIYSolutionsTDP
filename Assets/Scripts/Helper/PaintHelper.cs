using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintHelper : MonoBehaviour
{
    public GameObject brush;
    public float BrushSize = 0.05f;
    public GameObject middleEyeGameobject;
    public GameObject RightHandGameobject;
    public GameObject parentInitial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0)
        {
            /*var go1 = Instantiate(brush, new Vector3((float)(this.transform.position.x), (float)(this.transform.position.y), this.transform.position.z), this.transform.rotation, transform);
            go1.gameObject.transform.localScale = this.transform.localScale;
            go1.gameObject.transform.rotation = this.transform.rotation;
            go1.gameObject.transform.localPosition = this.transform.localPosition; 
            go1.transform.SetParent(parentInitial.transform);*/

            var go1 = Instantiate(brush, new Vector3((float)(this.transform.position.x), (float)(this.transform.position.y), this.transform.position.z), this.transform.rotation, transform);
            go1.transform.localScale = Vector3.one * BrushSize;
            go1.transform.SetParent(parentInitial.transform);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0)
    //    {
    //        /*var go1 = Instantiate(brush, new Vector3((float)(this.transform.position.x), (float)(this.transform.position.y), this.transform.position.z), this.transform.rotation, transform);
    //        go1.gameObject.transform.localScale = this.transform.localScale;
    //        go1.gameObject.transform.rotation = this.transform.rotation;
    //        go1.gameObject.transform.localPosition = this.transform.localPosition; 
    //        go1.transform.SetParent(parentInitial.transform);*/
    //        if (other.tag.Contains("Silicona")) {
    //            var go1 = Instantiate(brush, new Vector3((float)(this.transform.position.x), (float)(this.transform.position.y), this.transform.position.z), this.transform.rotation, transform);
    //            go1.transform.localScale = Vector3.one * BrushSize;
    //            go1.transform.SetParent(parentInitial.transform);
    //        }
    //    }
    //}
}
    