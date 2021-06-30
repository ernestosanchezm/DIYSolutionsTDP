using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadView : MonoBehaviour
{
    ActividadController actividadController;
    public void inicializacionVista()
    {
        actividadController = GameObject.FindGameObjectWithTag("ventana4 detalle actividad").GetComponent<ActividadController>();
        FillMostrarDetalle();
    }
    public void FillMostrarDetalle()
    {
        actividadController.fillMostrarDetalle();
    }
    public void Onclick_empezar()
    {
        actividadController.eventoEmpezar();
    }
    public void Onclick_atras(int parametroVista)
    {
        actividadController.eventoAtras(ref parametroVista);
    }
    public void Onclick_verestadistica()
    {
        actividadController.eventoVerEstadistica();
    }
}
