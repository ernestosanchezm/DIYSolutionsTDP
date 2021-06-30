using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaView : MonoBehaviour
{
    PuertaController puertaController;
    public void inicializacionVista()
    {
        puertaController = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PuertaController>();
    }
    public void OnTouch_Left()
    {
    }
    public void OnTouch_Right()
    {

    }
}
