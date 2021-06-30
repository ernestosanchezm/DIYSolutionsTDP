using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActividadesController : MonoBehaviour
{
    ActividadesManager actividadesManager;
    ActividadesView actividadesView;

    ResponseEntity<List<ActivityEntity>> ListDone = new ResponseEntity<List<ActivityEntity>>();
    ResponseEntity<List<ActivityEntity>> ListNew = new ResponseEntity<List<ActivityEntity>>();
    
    public Image imageDoneToDisplay;
    public Image imageNewToDisplay;

    [SerializeField] private GameObject buttonActivityNew;
    [SerializeField] private GameObject buttonActivityDone;
    [SerializeField] private GameObject imageActivityNew;
    [SerializeField] private GameObject imageActivityDone;

    float xDone = -274;
    float yDone = 33;
    float xNew = -274;
    float yNew = -126;
    float xImageDone = (float)-276.5;
    float yImageDone = 92;
    float xImageNew = (float)-276.5;
    float yImageNew = -68;
    public void inicializarModulo()
    {
        actividadesManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<ActividadesManager>();
        actividadesView = GameObject.FindGameObjectWithTag("ventana2 actividades").GetComponent<ActividadesView>();
        actividadesView.inicializacionVista();
    }
    public void eventoAtras(ref int parametroControlador)
    {
        SceneManager.LoadScene(0);
    }
    public void fillListActivities()
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        var UrlNew = "http://homerepairvr.com/apiactivities/newactivities/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(UrlNew, this.ResponseNewCallback));
        var UrlDone = "http://homerepairvr.com/apiactivities/doneactivities/" + Id;
        StartCoroutine(ConsumeServiceHelper.Get(UrlDone, this.ResponseDoneCallback));
    }
    private void ResponseNewCallback(string data)
    {
        ListNew = JsonUtility.FromJson<ResponseEntity<List<ActivityEntity>>>(data);
        foreach (var item in ListNew.Data)
        {
            GameObject button = Instantiate(buttonActivityNew) as GameObject;
            button.SetActive(true);
            button.name = item.ActivityId.ToString();
            button.transform.position = new Vector3(xNew, yNew, 0.0f);
            button.GetComponentInChildren<Text>().text = item.ActivityName;
            button.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        fillMakeClickActivity(item.ActivityId.ToString(), item.Scene.ToString(), "1");
                    }
                );
            button.transform.SetParent(buttonActivityNew.transform.parent, false);
            xNew = xNew + 160;

            GameObject image = Instantiate(imageActivityNew) as GameObject;
            image.SetActive(true);
            image.name = "imagen" + item.ActivityId;
            image.transform.position = new Vector3(xImageNew, yImageNew, 0.0f);
            image.transform.SetParent(imageActivityNew.transform.parent, false);
            xImageNew = xImageNew + 160;


            StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl2(item.ActivityUrlPicture, item.ActivityId, ResponseImageNewCallback));
        }
    }
    private void ResponseDoneCallback(string data)
    {
        ListDone = JsonUtility.FromJson<ResponseEntity<List<ActivityEntity>>>(data);
        foreach (var item in ListDone.Data)
        {
            GameObject button = Instantiate(buttonActivityDone) as GameObject;

            button.SetActive(true);
            button.name = item.ActivityId.ToString();
            button.transform.position = new Vector3(xDone, yDone, 0.0f);
            button.GetComponentInChildren<Text>().text = item.ActivityName;
            button.GetComponent<Button>().onClick.AddListener(
                     () =>
                     {
                         fillMakeClickActivity(item.ActivityId.ToString(), item.Scene.ToString(), "0");
                     }
                 );
            button.transform.SetParent(buttonActivityDone.transform.parent, false);
            xDone = xDone + 160;

            GameObject image = Instantiate(imageActivityDone) as GameObject;
            image.SetActive(true);
            image.name = "imagen" + item.ActivityId;
            image.transform.position = new Vector3(xImageDone, yImageDone, 0.0f);
            image.transform.SetParent(imageActivityDone.transform.parent, false);
            xImageDone = xImageDone + 160;


            StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl2(item.ActivityUrlPicture, item.ActivityId, ResponseImageDoneCallback));
        }
    }
    public void fillMakeClickActivity(string activityId, string scene, string isDone)
    {
        PlayerPrefs.SetString("ActivityId", activityId);
        PlayerPrefs.SetString("Scene", scene);
        PlayerPrefs.SetString("Done", isDone);
        SceneManager.LoadScene(3);
    }
    private void ResponseImageNewCallback(Sprite data, string id)
    {
        var imageToDisplays = GameObject.Find("Canvas/ventana2 actividades/imagen" + id);
        this.imageNewToDisplay = imageToDisplays.GetComponent<Image>();
        this.imageNewToDisplay.sprite = data;
    }
    private void ResponseImageDoneCallback(Sprite data, string id)
    {
        var imageToDisplays = GameObject.Find("Canvas/ventana2 actividades/imagen" + id);
        this.imageDoneToDisplay = imageToDisplays.GetComponent<Image>();
        this.imageDoneToDisplay.sprite = data;
    }
}
