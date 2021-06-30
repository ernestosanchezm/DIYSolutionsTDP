using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PuertaManager : MonoBehaviour
{
    PuertaController puertaController;
    public Transform trackingSpace; 
    public OVRInput.Controller controller;
    public GameObject BLeft;
    public GameObject BRight;
    public GameObject BTop;
    public GameObject BDoor;

    void Start()
    {
        //puertaController = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PuertaController>();
        //puertaController.inicializarModulo();
        BLeft = GameObject.Find("BIzquierdo");
        BRight = GameObject.Find("BDerecho");
        BTop = GameObject.Find("BSuperior");
        BDoor = GameObject.Find("BPuerta");

    }
    void Update()
    {
        //if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        //{

        //    var position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //    var a = 0;
        //    //Vector3 rotation = trackingSpace.Rotate(OVRInput.GetLocalControllerRotation(controller));
        //    //Instantiate(gameObject, position, rotation);
        //}

        //RaycastHit hit;
        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        

        //if (Physics.Raycast(transform.position, transform.forward,out hit))
        //{
        //    if (hit.collider != null)
        //    {
        //        if (BLeft != hit.collider.gameObject)
        //        {
        //            //BLeft.transform.SendMessage("Test 1");
        //            BLeft = hit.transform.gameObject;   
        //            //BLeft.transform.SendMessage("Test 2");
        //            Debug.Log("Enter");
        //        }
        //        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        //        {
        //            Debug.Log("Funciona down");
        //            //BLeft.transform.SendMessage("Test 3 down");
        //        }
        //        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        //        {
        //            Debug.Log("Funciona up");
        //            //BLeft.transform.SendMessage("Test 3 down");
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("colision");
        //    }
        //}
        //else
        //{
        //    if(BLeft != null)
        //    {
        //        //BLeft.transform.SendMessage("Test 4 exit");
        //        BLeft = new GameObject();
        //    }
        //}
    }
}
