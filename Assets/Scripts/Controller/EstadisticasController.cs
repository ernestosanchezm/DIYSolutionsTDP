using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EstadisticasController : MonoBehaviour
{
    EstadisticasManager estadisticasManager;
    EstadisticasView estadisticasView;
    ResponseEntity<List<StatisticsEntity>> statistics = new ResponseEntity<List<StatisticsEntity>>();
    public Image imageToDisplay;
    public void inicializarModulo()
    {
        estadisticasManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<EstadisticasManager>();
        estadisticasView = GameObject.FindGameObjectWithTag("ventana5 statistic").GetComponent<EstadisticasView>();
        estadisticasView.inicializacionVista();
    }
    public void fillMostrarEstadistica()
    {
        var activityId = PlayerPrefs.GetString("ActivityId", "No Name");
        var userId = PlayerPrefs.GetString("UserId", "No Name");
        var UrlNew = "http://homerepairvr.com/apiactivities/liststatisticsactivities/" + activityId  + "/users/" + userId;
        StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseCallback));
    }
    public void eventoAtras(ref int parametroControlador)
    {
        SceneManager.LoadScene(3);
    }
    private void ResponseCallback(string data)
    {
        statistics = JsonUtility.FromJson<ResponseEntity<List<StatisticsEntity>>>(data);

        //CAMBIAR
        GameObject.Find("Canvas/ventana5 statistic/Titulo").GetComponent<Text>().text = statistics.Data[0]. Titulo;
        GameObject.Find("Canvas/ventana5 statistic/cerrores").GetComponent<Text>().text = statistics.Data[0].Incorrectas.ToString();
        GameObject.Find("Canvas/ventana5 statistic/ttranscurrido").GetComponent<Text>().text = statistics.Data[0].TiempoTranscurrido.ToString();
        GameObject.Find("Canvas/ventana5 statistic/npaso").GetComponent<Text>().text = statistics.Data[0].TiempoTranscurrido.ToString() + " (Segundos)";
        GameObject.Find("Canvas/ventana5 statistic/ccorrecta").GetComponent<Text>().text = statistics.Data[0].StepTutorial.ToString();
        //CAMBIAR

        StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl("https://i.pinimg.com/originals/69/b6/1b/69b61be124e320d69f2275b1a3cee967.jpg", ResponseImageCallback));
    }
    private void ResponseImageCallback(Sprite data)
    {
        var imageToDisplays = GameObject.Find("Canvas/ventana5 statistic/Image");
        this.imageToDisplay = imageToDisplays.GetComponent<Image>();
        this.imageToDisplay.sprite = data;
    }
}
