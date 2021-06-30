using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UsuarioController : MonoBehaviour
{

    ResponseEntity<UserEntity> User = new ResponseEntity<UserEntity>();

    //NavegationManager navegationManager;
    UsuarioManager usuarioManager;
    UsuarioView usuarioView;
    public void inicializarModulo()
    {
        usuarioManager = GameObject.FindGameObjectWithTag("menu manager").GetComponent<UsuarioManager>();
        usuarioView = GameObject.FindGameObjectWithTag("ventana3 usuario").GetComponent<UsuarioView>();
        usuarioView.inicializacionVista();
    }
    public void fillAddEditUser()
    {
        var Id = PlayerPrefs.GetString("UserAddEditId", "No Name");
        if (!String.IsNullOrEmpty(Id)) {
            var url = "http://homerepairvr.com/apiusers/viewusers/" + Id;
            StartCoroutine(ConsumeServiceHelper.Get(url, this.ResponseEditProfileCallback));
        }
    }
    private void ResponseEditProfileCallback(string data)
    {
        User = JsonUtility.FromJson<ResponseEntity<UserEntity>>(data);
        GameObject.Find("Canvas/ventana3 usuario/InpNombreUsuario").GetComponent<InputField>().text = User.Data.UserName;
    }
    public void evento(ref int parametroControlador)
    {
        Debug.Log("controlador 3: " + parametroControlador);
        //navegationManager.navegation_to(ref parametroControlador);
    }
    public void eventoBack(ref int parametroControlador)
    {
        GameObject.Find("Canvas/ventana3 usuario/InpNombreUsuario").GetComponent<InputField>().text = null;
        SceneManager.LoadScene(0);
    }
    public void eventoSave(ref int parametroControlador)
    {
        var Id = PlayerPrefs.GetString("UserAddEditId", "No Name");
        UserEntity model = new UserEntity();
        model.UserId = !String.IsNullOrEmpty(Id) ? Id : "";
        model.UserName = GameObject.Find("Canvas/ventana3 usuario/InpNombreUsuario").GetComponent<InputField>().text;
        model.UserUrlPicture = "";
        /****/
        WWWForm form = new WWWForm();
        form.AddField("UserId", model.UserId);
        form.AddField("UserName", model.UserName);
        form.AddField("UserUrlPicture", "http://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/768px-User_icon_2.svg.png");
        form.AddField("DateCreate", " ");
        form.AddField("Status", " ");
        form.AddField("DateUpdate", " ");

        this.StartCoroutine(ConsumeServiceHelper.Post("http://homerepairvr.com/apiusers/addeditusers", form, this.ResponseSaveUserCallback));
        SceneManager.LoadScene(0);
    }
    private void ResponseSaveUserCallback(string data)
    {
        ResponseEntity<String> response = JsonUtility.FromJson<ResponseEntity<String>>(data);
        SceneManager.LoadScene(0);
    }
   

}
