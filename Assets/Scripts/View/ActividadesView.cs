using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActividadesView : MonoBehaviour
{
    ActividadesController actividadesController;
    public void inicializacionVista()
    {
        actividadesController = GameObject.FindGameObjectWithTag("ventana2 actividades").GetComponent<ActividadesController>();
        FillListActivities();
    }

    public void FillListActivities()
    {
        actividadesController.fillListActivities();
    }

    public void Onclick_atras(int parametroVista)
    {
        actividadesController.eventoAtras(ref parametroVista);
    }
}
