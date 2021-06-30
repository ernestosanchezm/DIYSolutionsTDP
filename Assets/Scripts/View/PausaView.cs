using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaView : MonoBehaviour
{
    PausaController pausaController;
    public List<GameObject> lstGameObjects = new List<GameObject>();
    public List<GameObject> lstGameObjectsPosition = new List<GameObject>();

    public void inicializacionVista()
    {
        pausaController = GameObject.FindGameObjectWithTag("ventana pausa").GetComponent<PausaController>();
        pausaController.fillScene(lstGameObjects);
    }
    public void Onclick_btnContinuar()
    {
        pausaController.eventoContinuar();
    }
    public void Onclick_btnReiniciar(int parametroVista)
    {
        pausaController.eventoReiniciar(ref parametroVista);
    }
    public void Onclick_btnGuardar()
    {
        pausaController.eventoGuardar();
    }
    public void Onclick_btnSalir(int parametroVista)
    {
        pausaController.eventoSalir(ref parametroVista);
    }
    public void Onclick_btnSalirJuego()
    {
        pausaController.eventoSalirJuego();
    }
    public void Onclick_btnAyuda()
    {
        pausaController.eventoAyuda();
    }
    public void Onclick_btnRetrocederAyuda()
    {
        pausaController.eventoRetrocederAyuda();
    }
}
