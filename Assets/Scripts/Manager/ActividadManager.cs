using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadManager : MonoBehaviour
{
    ActividadController actividadController;
    void Start()
    {
        actividadController = GameObject.FindGameObjectWithTag("ventana4 detalle actividad").GetComponent<ActividadController>();
        actividadController.inicializarModulo();
    }
}
