using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalManager : MonoBehaviour
{
    PrincipalController principalController;
    void Start()
    {
        principalController = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PrincipalController>();
        principalController.inicializarModulo();
    }
}
