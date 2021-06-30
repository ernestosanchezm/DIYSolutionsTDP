using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaController : MonoBehaviour
{
    PuertaManager puertaManager;
    PuertaView puertalView;
    public void inicializarModulo()
    {
        puertaManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<PuertaManager>();
        puertalView = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PuertaView>();
        puertalView.inicializacionVista();
    }
}
