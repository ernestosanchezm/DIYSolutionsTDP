using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsuarioManager : MonoBehaviour
{
    UsuarioController usuarioController;
    void Start()
    {
        usuarioController = GameObject.FindGameObjectWithTag("ventana3 usuario").GetComponent<UsuarioController>();
        usuarioController.inicializarModulo();
    }
}
