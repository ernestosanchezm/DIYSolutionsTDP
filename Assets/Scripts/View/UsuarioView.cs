using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuarioView : MonoBehaviour
{
    UsuarioController usuarioController;
    public void inicializacionVista()
    {
        usuarioController = GameObject.FindGameObjectWithTag("ventana3 usuario").GetComponent<UsuarioController>();
        FillAddEditUser();
    }
    public void FillAddEditUser()
    {
        usuarioController.fillAddEditUser();
    }
    public void Onclick_botones(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        usuarioController.evento(ref parametroVista);
    }

    public void Onclick_botonesBack(int parametroVista)
    {
        Debug.Log("Vista usuario: Numero de ventana es" + parametroVista);
        usuarioController.eventoBack(ref parametroVista);
    }

    public void Onclick_botonesSave(int parametroVista)
    {
        usuarioController.eventoSave(ref parametroVista);
    }

}
