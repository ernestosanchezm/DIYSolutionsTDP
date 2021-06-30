using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    PausaController pausaController;
    public Canvas menuPausa;
    public GameObject laserPointer;
    void Start()
    {
        pausaController = GameObject.FindGameObjectWithTag("ventana pausa").GetComponent<PausaController>();
        pausaController.inicializarModulo();
        menuPausa = GameObject.FindGameObjectWithTag("menu pausa").GetComponent<Canvas>();      
     }
    void Update()
    {
        pausaController.controlPausa(menuPausa, laserPointer);
    }
}
