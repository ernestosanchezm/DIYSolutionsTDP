using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasManager : MonoBehaviour
{
    EstadisticasController estadisticasController;
    void Start()
    {
        estadisticasController = GameObject.FindGameObjectWithTag("ventana5 statistic").GetComponent<EstadisticasController>();
        estadisticasController.inicializarModulo();
    }
}
