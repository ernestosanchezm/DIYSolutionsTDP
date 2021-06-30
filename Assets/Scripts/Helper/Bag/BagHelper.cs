using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHelper : MonoBehaviour
{
    bool isClosed = false;
    public GameObject bag;

    public void OpenCloseBag()
    {
        if (isClosed == true)
        {
            bag.SetActive(true);
            isClosed = false;
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundMochila").GetComponent<RectTransform>().offsetMin = new Vector2(Convert.ToSingle(565.1028), -260);//left-bottom; //.sizeDelta = new Vector2(Convert.ToSingle(-0.03723145), -260);
        }
        else
        {
            bag.SetActive(false);
            isClosed = true;
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundMochila").GetComponent<RectTransform>().offsetMin = new Vector2(Convert.ToSingle(565.1028), 10);//.sizeDelta = new Vector2(Convert.ToSingle(-0.03723145), 10);
        }
    }
}
