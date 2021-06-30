using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadesManager : MonoBehaviour
{
    ActividadesController actividadesController;
    void Start()
    {
        actividadesController = GameObject.FindGameObjectWithTag("ventana2 actividades").GetComponent<ActividadesController>();
        actividadesController.inicializarModulo();
    }
}
