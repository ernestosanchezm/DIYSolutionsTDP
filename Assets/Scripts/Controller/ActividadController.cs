
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActividadController : MonoBehaviour
{
    ActividadManager actividadManager;
    ActividadView actividadView;
    ResponseEntity<ActivityEntity> Activity = new ResponseEntity<ActivityEntity>();
    ResponseEntity<SelectGameResultEntity> SelectGameResult = new ResponseEntity<SelectGameResultEntity>();
    public Image imageToDisplay;
    public void inicializarModulo()
    {
        actividadManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<ActividadManager>();
        actividadView = GameObject.FindGameObjectWithTag("ventana4 detalle actividad").GetComponent<ActividadView>();
        actividadView.inicializacionVista();
    }
    public void fillMostrarDetalle()
    {
        PlayerPrefs.SetString("Reiniciar", "0");
        var Id = PlayerPrefs.GetString("ActivityId", "No Name");
        var UrlNew = "http://homerepairvr.com/apiactivities/viewactivities/" + Id;
        var Done = PlayerPrefs.GetString("Done", "No Name");
        if(Done == "1")
        {
            GameObject.Find("Canvas/ventana4 detalle actividad/VerEstadistica").SetActive(false);
        }
        StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseNewCallback));
    }
    private void ResponseNewCallback(string data)
    {
        Activity = JsonUtility.FromJson<ResponseEntity<ActivityEntity>>(data);
        GameObject.Find("Canvas/ventana4 detalle actividad/DifficultValue").GetComponent<Text>().text = "3";
        GameObject.Find("Canvas/ventana4 detalle actividad/TimeValue").GetComponent<Text>().text = Activity.Data.ActivityTime;
        GameObject.Find("Canvas/ventana4 detalle actividad/Title").GetComponent<Text>().text = Activity.Data.ActivityName;
        GameObject.Find("Canvas/ventana4 detalle actividad/Description").GetComponent<Text>().text = Activity.Data.ActivityDescription;
        StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl(Activity.Data.ActivityUrlPicture, ResponseImageCallback));
    }
    private void ResponseImageCallback(Sprite data)
    {
        var imageToDisplays = GameObject.Find("Canvas/ventana4 detalle actividad/Image");
        this.imageToDisplay = imageToDisplays.GetComponent<Image>();
        this.imageToDisplay.sprite = data;
    }
    public void eventoAtras(ref int parametroControlador)
    {
        SceneManager.LoadScene(2);
    }
    public void eventoEmpezar()
    {
        /*SAVE USER ACTIVITY*/
        var UrlNew = "http://homerepairvr.com/apigames/selectgames";
        SelectGameEntity model = new SelectGameEntity();
        var activityId = PlayerPrefs.GetString("ActivityId", "No Name");
        var userId = PlayerPrefs.GetString("UserId", "No Name");
        model.ActivityId = Convert.ToInt32(activityId);
        model.UsuarioId = Convert.ToInt32(userId);

        WWWForm form = new WWWForm();
        form.AddField("UserId", model.UsuarioId);
        form.AddField("ActivityId", model.ActivityId);

        StartCoroutine(ConsumeServiceHelper.Post(UrlNew, form, this.ResponseCallback));       
    }
    public void eventoVerEstadistica()
    {
        SceneManager.LoadScene(8);
    }

    private void ResponseCallback(string data)
    {
        SelectGameResult = JsonUtility.FromJson<ResponseEntity<SelectGameResultEntity>>(data);

        PlayerPrefs.SetString("UserActivityId", SelectGameResult.Data.UserActivityId);
        var Id = PlayerPrefs.GetString("Scene", "No Name");
        int numScene = Convert.ToInt32(Id);
        SceneManager.LoadScene(numScene);
    }
}
