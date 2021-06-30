using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrincipalController : MonoBehaviour
{
    PrincipalManager principalManager;
    PrincipalView principalView;
    ResponseEntity<List<UserEntity>> ListUsers = new ResponseEntity<List<UserEntity>>();
    ResponseEntity<UserEntity> User = new ResponseEntity<UserEntity>();

    [SerializeField] GameObject buttonProfile;
    [SerializeField] GameObject buttonContinuar;
    [SerializeField] GameObject buttonEditar;
    [SerializeField] GameObject buttonEliminar;

    public Image imageToDisplay;

    int x = -245;
    int y = 115;

    public void inicializarModulo()
    {
        principalManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<PrincipalManager>();
        principalView = GameObject.FindGameObjectWithTag("ventana1 principal").GetComponent<PrincipalView>();
        principalView.inicializacionVista();
    }
    public void eventoEditar(ref int parametroControlador)
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        PlayerPrefs.SetString("UserAddEditId", Id);
        SceneManager.LoadScene(1);
    }
    public void eventoCrear(ref int parametroControlador)
    {
        PlayerPrefs.SetString("UserAddEditId", null);
        SceneManager.LoadScene(1);
    }
    public void eventoContinuar(ref int parametroControlador)
    {
        SceneManager.LoadScene(2);
    }
    public void eventoEliminar(ref int parametroControlador)
    {
        var Id = PlayerPrefs.GetString("UserId", "No Name");
        this.StartCoroutine(ConsumeServiceHelper.Get("http://homerepairvr.com/apiusers/deleteusers/" + Id, this.ResponseDeleteUserCallback));
    }
    public void fillListUser()
    {
        this.StartCoroutine(ConsumeServiceHelper.Get("http://homerepairvr.com/apiusers/listusers", this.ResponseListUserCallback));
    }
    public void fillMakeClickProfile(string userId)
    {
        GameObject btnGameObject = EventSystem.current.currentSelectedGameObject;
        ColorBlock btnColors = btnGameObject.GetComponent<Button>().colors;

        buttonContinuar = GameObject.Find("Canvas/ventana1 principal/btnContinuar");
        buttonContinuar.SetActive(true);
        buttonEditar = GameObject.Find("Canvas/ventana1 principal/btnEditar");
        buttonEditar.SetActive(true);
        buttonEliminar = GameObject.Find("Canvas/ventana1 principal/btnEliminar");
        buttonEliminar.SetActive(true);

        var url = "http://homerepairvr.com/apiusers/viewusers/" + userId;
        StartCoroutine(ConsumeServiceHelper.Get(url, this.ResponseInfoProfileCallback));
    }
    private void ResponseDeleteUserCallback(string data)
    {
        ResponseEntity<String> response = JsonUtility.FromJson<ResponseEntity<String>>(data);
        if (!String.IsNullOrEmpty(response.Data))
        {
            SceneManager.LoadScene(0);
        }
    }
    private void ResponseListUserCallback(string data)
    {
        ListUsers = JsonUtility.FromJson<ResponseEntity<List<UserEntity>>>(data);
        foreach (var item in ListUsers.Data)
        {
            GameObject button = (GameObject) Instantiate(buttonProfile);

            button.SetActive(true);
            button.name = "btnProfileUser-" + item.UserId.ToString();
            button.transform.position = new Vector3(x, y, 0.0f);
            y = y - 40;
            button.GetComponentInChildren<Text>().text = item.UserName; //.GetComponent<TextButton>().SetText(item.UserName);
            button.GetComponent<Button>().onClick.AddListener(
                    () =>
                    {
                        fillMakeClickProfile(item.UserId.ToString());
                    }
                );
            button.transform.SetParent(buttonProfile.transform.parent, false);
        }
    }  
    private void ResponseInfoProfileCallback(string data)
    {
        User = JsonUtility.FromJson<ResponseEntity<UserEntity>>(data);
        PlayerPrefs.SetString("UserId", User.Data.UserId);
        var imageToDisplays = GameObject.Find("Canvas/ventana1 principal/imgPerfil");
        this.imageToDisplay = imageToDisplays.GetComponent<Image>();
        StartCoroutine(LoadImageHelper.loadSpriteImageFromUrl(User.Data.UserUrlPicture, ResponseImageProfileCallback));
    }
    private void ResponseImageProfileCallback(Sprite data)
    {
        this.imageToDisplay.sprite = data;
    }
}
