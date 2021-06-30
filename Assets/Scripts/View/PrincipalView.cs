using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalView : MonoBehaviour
{
    PrincipalController principalController;
    public void inicializacionVista()
    {
        principalController = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PrincipalController>();
        FillListUser();
    }
    public void FillListUser()
    {
        principalController.fillListUser();
    }
    public void Onclick_botonesEditar(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        principalController.eventoEditar(ref parametroVista);
    }

    public void Onclick_botonesCrear(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        principalController.eventoCrear(ref parametroVista);
    }
    public void Onclick_botonesContinuar(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        principalController.eventoContinuar(ref parametroVista);
    }
    public void Onclick_botonesEliminar(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        principalController.eventoEliminar(ref parametroVista);
    }
}
