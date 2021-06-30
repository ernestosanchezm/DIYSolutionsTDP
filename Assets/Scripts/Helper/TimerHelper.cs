using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHelper : MonoBehaviour
{
    [Tooltip("Tiempo inicial en segundos")]
    public int tiempoInicial;
    [Tooltip("Escala del tiempo del reloj")]
    [Range(-10.0f,10.0f)]
    public float escalaDeTiempo = 1;

    private Text myText;
    private float tiempoDelFrameConTimeScale = 0f;
    public float tiempoAMostrarEnSegundos = 0f;
    private float escalaDeTiempoAlPausar, escalaDeTiempoInicial;
    private bool estaPausado = false;

    void Start()
    {
        escalaDeTiempoInicial = escalaDeTiempo;
        var test = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/TiempoNum").GetComponent<Text>().text;
       // var contador = Convert.ToInt32(GameObject.Find("OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/TiempoNum").GetComponent<Text>().text);
        myText = GetComponent<Text>();
        tiempoAMostrarEnSegundos = tiempoInicial;
        ActualizarReloj(/*contador > 0 ? contador :*/ tiempoInicial);
    }
    void Update()
    {
        tiempoDelFrameConTimeScale = Time.deltaTime * escalaDeTiempo;
        tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
        ActualizarReloj(tiempoAMostrarEnSegundos);
    }

    public void ActualizarReloj(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        if (tiempoEnSegundos < 0) tiempoEnSegundos = 0;

        minutos = (int)tiempoEnSegundos / 60;
        segundos = (int)tiempoEnSegundos % 60;

        textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");
        myText.text = textoDelReloj;
     }
}
