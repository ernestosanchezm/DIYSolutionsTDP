using Assets.Scripts.Entity;
using Assets.Scripts.Model;
using Assets.Scripts.ViewModel.PausaViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxelBusters.Serialization;

public class PausaController : MonoBehaviour
{
    PausaManager pausaManager;
    PausaView pausaView;
    public bool isPausa;
    public Canvas menuPausa;
    public GameObject laserPointer;
    private List<GameObject> _lstGameOnjects = new List<GameObject>();
    public void inicializarModulo()
    {
        pausaManager = GameObject.FindGameObjectWithTag("menu pausa").GetComponent<PausaManager>();
        pausaView = GameObject.FindGameObjectWithTag("ventana pausa").GetComponent<PausaView>();
        pausaView.inicializacionVista();
    }
    public void fillScene(List<GameObject> lstGameOnjects)
    {
        var reiniciar = PlayerPrefs.GetString("Reiniciar", "No Name");
        if (reiniciar == "0") {
            var userActivityId = PlayerPrefs.GetString("UserActivityId", "No Name");
            var UrlNew = "http://homerepairvr.com/apigames/chargedonegames/" + userActivityId;
            StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseCargeGameCallback));
        }
    }
    public void controlPausa(Canvas _menuPausa, GameObject _laserPointer)
    {
        this.menuPausa = _menuPausa;
        if (OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.LTouch))
        {
            _menuPausa.enabled = true;
            //laserPointer.SetActive(true);
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador").GetComponent<Canvas>().enabled = false;
            isPausa = true;
        }
        else if (isPausa == false)
        {
            _menuPausa.enabled = false;
            //GameObject.Find("Game/LaserRightHand").gameObject.SetActive(false);
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador").GetComponent<Canvas>().enabled = true;
            isPausa = false;
        }
    }
    public void eventoContinuar()
    {
        this.menuPausa.enabled = false;
        //this.laserPointer.SetActive(false);
        isPausa = false;
        //laserPointer.SetActive(false);
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana pausa/txtAlert").GetComponent<Text>().text = "";
    }
    public void eventoReiniciar(ref int parametroControlador)
    {
        PlayerPrefs.SetString("Reiniciar", "1");
        SceneManager.LoadScene(parametroControlador);
    }
    public void eventoGuardar()
    {
        var actividadId = PlayerPrefs.GetString("ActivityId", "No Name");
        var userActivityId = PlayerPrefs.GetString("UserActivityId", "No Name");
        string descripcion = String.Empty;
        SaveGame saveGame = new SaveGame();
        switch (actividadId)
        {
           
            case "2":
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-LOCETA");
                break;
            case "5": 
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-ALERO");
                break;
            case "11": 
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-PUERTA");
                break;
            case "12":
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-LUCES");
                break;
            case "15":
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-VENTANA");
                break;
            case "16":
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-MUEBLE");
                break;
            case "17":
                saveGame.Save(pausaView.lstGameObjects, userActivityId.ToString() + "-FREGADERO");
                break;
        }
        
        var tiempoTrancurridotexto = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/TiempoNum").GetComponent<Text>().text.Split(':');
        var tiempoTrancurridoValor = Convert.ToInt32(tiempoTrancurridotexto[0]) * 60 + Convert.ToInt32(tiempoTrancurridotexto[1]);

        var actividad = string.Empty;
        switch (actividadId)
        {
           
            case "2":
                actividad = "-LOCETA";
                break;
            case "5":
                actividad = "-ALERO";
                break;
            case "11":
                actividad = "-PUERTA";
                break;
            case "12":
                actividad = "-LUCES";
                break;
            case "15":
                actividad = "-VENTANA";
                break;
            case "16":
                actividad = "-MUEBLE";
                break;
            case "17":
                actividad = "-FREGADERO";
                break;
        }

        SaveGameEntity model = new SaveGameEntity
        {
            UserActivityId = Convert.ToInt32(userActivityId),
            StepId = 1,
            Description = userActivityId + actividad,
            Status = "COR",
            TimeElapse = tiempoTrancurridoValor,
            CorrectSteps = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosNum").GetComponent<Text>().text),
            WrongSteps = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosErradorNum").GetComponent<Text>().text),
            StatusActivity = "COR",
            StepTutorial = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundTutorial/TutorialNum").GetComponent<Text>().text),
        };
        WWWForm form = new WWWForm();
        form.AddField("UserActivityId", model.UserActivityId);
        form.AddField("Description", model.Description);
        form.AddField("Status", model.Status);
        form.AddField("TimeElapse", model.TimeElapse);
        form.AddField("CorrectSteps", model.CorrectSteps);
        form.AddField("WrongSteps", model.WrongSteps);
        form.AddField("StatusActivity", model.StatusActivity);
        form.AddField("StepTutorial", model.StepTutorial);
        var UrlNew = "http://homerepairvr.com/apigames/savegames";
        StartCoroutine(ConsumeServiceHelper.Post(UrlNew, form, this.ResponseSaveCallback));
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana pausa/txtAlert").GetComponent<Text>().text = "JUEGO GUARDADO";
    }
    public void eventoSalir(ref int parametroControlador)
    {
        SceneManager.LoadScene(parametroControlador);
    }
    public void eventoSalirJuego()
    {
        Application.Quit();
    }
    public void eventoAyuda()
    {
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana pausa").gameObject.SetActive(false);
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana ayuda").gameObject.SetActive(true);
    }
    public void eventoRetrocederAyuda()
    {
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana pausa").gameObject.SetActive(true);
        GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/Canvas/Ventana ayuda").gameObject.SetActive(false);
    }
    private void ResponseSaveCallback(string data)
    {
        var Result = JsonUtility.FromJson<ResponseEntity<String>>(data);
    }
    private void ResponseCargeGameCallback(string data)
    {
        var loadData = JsonUtility.FromJson<ResponseEntity<LoadGameEntity>>(data);
        if (!String.IsNullOrEmpty(loadData.Data.Description)) {
            var actividadId = PlayerPrefs.GetString("ActivityId", "No Name");
            SaveGame saveGame = new SaveGame();
           
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosNum").GetComponent<Text>().text = loadData.Data.CorrectSteps.ToString();
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosErradorNum").GetComponent<Text>().text = loadData.Data.WrongSteps.ToString();
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundTutorial/TutorialNum").GetComponent<Text>().text = loadData.Data.StepTutorial.ToString();
            GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/TiempoNum").GetComponent<TimerHelper>().tiempoAMostrarEnSegundos = loadData.Data.TimeElapse;
            GameObject.Find("Game/TutorialManager").GetComponent<TutorialManager>().SetNextTutorial(loadData.Data.StepTutorial - 1);
            switch (actividadId)
            {
              
                case "2"://LOCETA
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "5"://ALERO 
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "11"://PUERTA 
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "12"://LUCES
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "15"://VENTANA 
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "16"://MUEBLE 
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
                case "17"://FREGADERO
                    saveGame.Load(pausaView.lstGameObjects, pausaView.lstGameObjectsPosition, loadData.Data.Description);
                    break;
            }
        }

    }

    public string ActualizarReloj(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        if (tiempoEnSegundos < 0) tiempoEnSegundos = 0;

        minutos = (int)tiempoEnSegundos / 60;
        segundos = (int)tiempoEnSegundos % 60;

        textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");
        return textoDelReloj;
    }
}
