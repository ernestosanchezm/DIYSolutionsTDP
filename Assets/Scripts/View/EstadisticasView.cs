using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasView : MonoBehaviour
{
    EstadisticasController estadisticasController;
    public void inicializacionVista()
    {
        estadisticasController = GameObject.FindGameObjectWithTag("ventana5 statistic").GetComponent<EstadisticasController>();
        FillMostrarEstadistica();
    }
    public void FillMostrarEstadistica()
    {
        estadisticasController.fillMostrarEstadistica();
    }
    public void Onclick_atras(int parametroVista)
    {   
        estadisticasController.eventoAtras(ref parametroVista);
    }
}
