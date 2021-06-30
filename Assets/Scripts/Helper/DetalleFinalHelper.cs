using Assets.Scripts.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DetalleFinalHelper : MonoBehaviour
{
    public void SaveDetalleFinal(){
        var userActivityId = PlayerPrefs.GetString("UserActivityId", "No Name");
        var tiempoTrancurridotexto = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/TiempoNum").GetComponent<Text>().text.Split(':');
        var tiempoTrancurridoValor = Convert.ToInt32(tiempoTrancurridotexto[0]) * 60 + Convert.ToInt32(tiempoTrancurridotexto[1]);

        SaveGameEntity model = new SaveGameEntity
        {
            UserActivityId = Convert.ToInt32(userActivityId),
            StepId = 1,
            Description = userActivityId + "-PUERTA",
            Status = "FIN",
            TimeElapse = tiempoTrancurridoValor,
            CorrectSteps = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosNum").GetComponent<Text>().text),
            WrongSteps = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundContador/PasosErradorNum").GetComponent<Text>().text),
            StatusActivity = "FIN",
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
    }

    private void ResponseSaveCallback(string data)
    {
        SceneManager.LoadScene(2);
    }
}
