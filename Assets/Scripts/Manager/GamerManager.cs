using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerManager : MonoBehaviour
{
    public Canvas menuPausa;
    public bool isPausa;
    private void Start()
    {
        menuPausa = GameObject.FindGameObjectWithTag("menu pausa").GetComponent<Canvas>();
    }
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
        {
            menuPausa.enabled = true;
            isPausa = true;
        }
        else if(isPausa == false)
        {
            menuPausa.enabled = false;
            isPausa = false;
        }
    }
}
