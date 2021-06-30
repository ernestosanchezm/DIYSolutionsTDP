    using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHelper : MonoBehaviour
{
    TutorialEntity tutorialEntity;
    int indice = 0;
    // Start is called before the first frame update
    void Start()
    {
        var actividadId = PlayerPrefs.GetString("ActivityId", "No Name");
        switch (actividadId)
        {
            case "15"://VENTANA 
                tutorialEntity = GetTutorialInstalacionVentana();
                GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
                GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
                break;
            case "2"://LOCETA
                tutorialEntity = GetTutorialinstalacionLosetasMayolica();
                GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
                GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
                break;
            case "5"://ALERO 
                tutorialEntity = GetTutorialAlero();
                GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
                GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
                break;
            case "11"://PUERTA 
                tutorialEntity = GetTutorialArmadoPuerta();
                GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
                GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
                break;
            case "12"://LUCES
                tutorialEntity = GetTutorialLuces();
                GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
                GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var actividadId = PlayerPrefs.GetString("ActivityId", "No Name");
        switch (actividadId)
        {
            case "15"://VENTANA 
                if (indice < 0)
                {
                    indice = 0;
                }
                else if (indice > 4)
                {
                    indice = 4;
                }
                break;
            case "2"://LOCETA
                if (indice < 0)
                {
                    indice = 0;
                }
                else if (indice > 5)
                {
                    indice = 5;
                }
                break;
            case "5"://ALERO 
                break;
            case "11"://PUERTA 
                if (indice < 0)
                {
                    indice = 0;
                }
                else if (indice > 5)
                {
                    indice = 5;
                }
                break;
            case "12"://LUCES
                if (indice < 0)
                {
                    indice = 0;
                }
                else if (indice > 6)
                {
                    indice = 6;
                }
                break;
        }
        
        if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            indice++;
            GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
            GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;
        }
        if (OVRInput.GetUp(OVRInput.RawButton.X))
        {
            indice--;
            GameObject.Find("Ventana Tutorial/Paso").GetComponent<Text>().text = "Paso " + tutorialEntity.Pasos[indice].Numero.ToString();
            GameObject.Find("Ventana Tutorial/Descripcion").GetComponent<Text>().text = tutorialEntity.Pasos[indice].Descripcion;;
        }
    }

    public TutorialEntity GetTutorialArmadoPuerta()
    {
        string data = "{\"Actividad\":\"INSTALACIÓN DE PUERTA\",\"Pasos\":[{\"Numero\": 1,\"Descripcion\": \"COLOCAR LOS TARUGOS Y GOLPEARLO CON UN MARTILLO\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 2,\"Descripcion\": \"COLOCAR EL MARCO DE LA PUERTA\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 3,\"Descripcion\": \"COLOCAR LOS TORNILLOS EN LOS MARCO Y PRESIONARLO CON EL TALADRO\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 4,\"Descripcion\": \"COLOCAR LAS BIZAGRAS CON LOS PERNOS Y PRESIONARLO CON EL TALADRO\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 5,\"Descripcion\": \"COLOCAR LA PUERTA\",\"Cantidad\": 1,\"Bandera\": 0}, {\"Numero\": 6,\"Descripcion\": \"ACTIVIDAD FINALIZADA\",\"Cantidad\": 0,\"Bandera\": 0}]}";
        var test = data.Trim();
        var result = JsonUtility.FromJson<TutorialEntity>(test);
        return result;
    }
    public TutorialEntity GetTutorialInstalacionVentana()
    {
        string data = "{\"Actividad\":\"INSTALACIÓN DE VENTANA\",\"Pasos\":[{\"Numero\": 1,\"Descripcion\": \"COLOCAR LOS MARCOS DE LA VENTANA Y AJUSTARLO CON LOS PERNOS\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 2,\"Descripcion\": \"COLOCAR LOS CORREDORES CON EN LA PARTE SUPERIOR E INFERIOR DE LOS MARCOS\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 3,\"Descripcion\": \"COLOCAR LAS LUNAS\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 4,\"Descripcion\": \"COLOCAR LOS XXX DE LAS VENTANAS PARA CUBRIR DE CORTES\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 5,\"Descripcion\": \"ACTIVIDAD FINALIZADA\",\"Cantidad\": 1,\"Bandera\": 0}]}";
        var test = data.Trim();
        var result = JsonUtility.FromJson<TutorialEntity>(test);
        return result;
    }
    public TutorialEntity GetTutorialinstalacionLosetasMayolica()
    {
        string data = "{\"Actividad\":\"INSTALACIÓN DE LOSETAS Y MAYOLICAS\",\"Pasos\":[{\"Numero\": 1,\"Descripcion\": \"COGER LA PALETA Y CON EL BOTÓN 'B' VERTIR LA MASA PARA COLOCAR LA LOSETA\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 2,\"Descripcion\": \"COLOCAR LAS LOSETAS Y LAS CRUCES ENCIMA DE LA MEZCLA COLOCADA CON LA PALETA\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 3,\"Descripcion\": \"INSTALACIÓN DE LOSETAS FINALIZADAS CONTINUE PARA INSTALAR LA MAYOLICA\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 4,\"Descripcion\": \"COGER LA PALETA Y CON EL BOTÓN 'B' VERTIR LA MASA PARA COLOCAR LA MAYÓLICA\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 5,\"Descripcion\": \"COLOCAR LAS MAYÓLICAS Y LAS CRUCES ENCIMA DE LA MEZCLA COLOCADA CON LA PALETA\",\"Cantidad\": 1,\"Bandera\": 0}, {\"Numero\": 6,\"Descripcion\": \"ACTIVIDAD FINALIZADA\",\"Cantidad\": 0,\"Bandera\": 0}]}";
        var test = data.Trim();
        var result = JsonUtility.FromJson<TutorialEntity>(test);
        return result;
    }
    public TutorialEntity GetTutorialLuces()
    {
        string data = "{\"Actividad\":\"INSTALACIÓN DE LUCES E INTERRUPTOR\",\"Pasos\":[{\"Numero\": 1,\"Descripcion\": \"COGER LO CABLES AZUL Y ROJO COLOCALOS EN LA POSICIÓN DONDE SERÁ INSTALADA LA LUZ Y EL INTERRUPTOR\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 2,\"Descripcion\": \"COLOCAR EL INTERRUPTOR\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 3,\"Descripcion\": \"COLOCA LOS PERNOS PARA ASEGURAR EL INTERRUPTOR\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 4,\"Descripcion\": \"COLOCAR EL SOCATE EN EL OTRO EXTREMO DE LA POSICION DEL CABLE\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 5,\"Descripcion\": \"COGER LOS TORNILLOS Y COLOCALO EN EL SOCATE PARA ASEGURARLO\",\"Cantidad\": 1,\"Bandera\": 0}, {\"Numero\": 6,\"Descripcion\": \"COLOCA EL FOCO\",\"Cantidad\": 0,\"Bandera\": 0}, {\"Numero\": 7,\"Descripcion\": \"ACTIVIDAD FINALIZADA\",\"Cantidad\": 0,\"Bandera\": 0}]}";
        var test = data.Trim();
        var result = JsonUtility.FromJson<TutorialEntity>(test);
        return result;
    }
    public TutorialEntity GetTutorialAlero()
    {
        string data = "{\"Actividad\":\"INSTALACIÓN DE ALERO\",\"Pasos\":[{\"Numero\": 1,\"Descripcion\": \"COGER LAS PARANTES AZULES PARA LA BASE DEL ALERO\",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 2,\"Descripcion\": \"COGER LOS BLOQUES DE COLOR AMARILLO \",\"Cantidad\": 8,\"Bandera\": 0},{\"Numero\": 3,\"Descripcion\": \"COLOCA LOS PERNOS PARA SUJETARLO CONTRA LA PARED\",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 4,\"Descripcion\": \"COGER Y COLOCAR LOS BLOQUES DE COLOR VERDE PARA UNIR LOS BLOQUES COLOR AZUL Y AMARILLO \",\"Cantidad\": 3,\"Bandera\": 0},{\"Numero\": 5,\"Descripcion\": \"COGER LOS TORNILLOS Y ASEGURARLOS\",\"Cantidad\": 1,\"Bandera\": 0}, {\"Numero\": 6,\"Descripcion\": \"COLOCA LA CALAMINA DEL ALERO\",\"Cantidad\": 0,\"Bandera\": 0}, {\"Numero\": 7,\"Descripcion\": \"ACTIVIDAD FINALIZADA\",\"Cantidad\": 0,\"Bandera\": 0}]}";
        var test = data.Trim();
        var result = JsonUtility.FromJson<TutorialEntity>(test);
        return result;
    }
}
