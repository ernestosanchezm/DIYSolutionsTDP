using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTutorial : Tutorial
{

    public List<GameObject> ObjectWithoutPositionPrefat;
    public List<GameObject> ObjectWithoutPosition;
    public List<TutorialItem> Keys = new List<TutorialItem>();
    public List<TutorialItem> NoKeys = new List<TutorialItem>();
    public List<TutorialItem> KeysInPosition = new List<TutorialItem>();
    public List<TutorialItem> KeyEffect = new List<TutorialItem>();
    public List<TutorialItem> KeyPaint = new List<TutorialItem>();
    public List<GameObject> Huecos = new List<GameObject>();
    public Text PasosCorrectos;
    public Text PasosErrados;
    public Material ColorIndicator;
    public Material ColorToPaint;
    public Material ColorToTransparent;
    public Material ColorHueco;

    public override void CheckIfHappening()
    {
        for (int i = 0; i < Keys.Count; i++)
        {
            Keys[i].gameObject.GetComponent<Renderer>().material = ColorIndicator;
            var objetosHijo = Keys[i].GetComponentsInChildren<Renderer>();// = lightMaterial;ç
            if (objetosHijo != null)
            {
                foreach (var item in objetosHijo)
                {
                    item.material = ColorIndicator;
                }
            }
            if (Keys[i].IsSelected == true)
            {
                Keys.RemoveAt(i);
                var pcorrectos = Convert.ToInt32(PasosCorrectos.text);
                PasosCorrectos.text = (pcorrectos + 1).ToString();
                break;
            }
        }

        for (int i = 0; i < NoKeys.Count; i++)
        {
            if (NoKeys[i].IsSelected == true)
            {
                var perrados = Convert.ToInt32(PasosErrados.text);
                PasosErrados.text = (perrados + 1).ToString();
                NoKeys[i].IsSelected = false;
                break;
            }
        }

        //for (int i = 0; i < NoKeys.Count; i++)
        //{
        //    if (NoKeys[i].IsSelected == true)
        //    {
        //        PasosErrados.text = (Convert.ToInt32(PasosErrados.text) + 1).ToString();
        //        NoKeys[i].IsSelected = false;
        //        break;
        //    }
        //}

        for (int i = 0; i < KeysInPosition.Count; i++)
        {
            if (KeysInPosition[i].IsPlaced == true)
            {
                KeysInPosition[i].IsPlaced = false;
                KeysInPosition.RemoveAt(i);
                break;
            }
        }

        for (int i = 0; i < Huecos.Count; i++)
        {
            Huecos[i].gameObject.GetComponent<Renderer>().material = ColorHueco;
        }

        for (int i = 0; i < KeyEffect.Count; i++)
        {
            if (KeyEffect[i].IsEffected == true)
            {
                KeyEffect.RemoveAt(i);
                PasosCorrectos.text = (Convert.ToInt32(PasosCorrectos.text) + 1).ToString();
                //NoKeys[i].IsSelected = false;
                break;
            }
        }
        for (int i = 0; i < KeyPaint.Count; i++)
        {
           
            if (KeyPaint[i].IsPainted == true)
            {
                KeyPaint[i].gameObject.GetComponent<Renderer>().material = ColorToTransparent;
                KeyPaint.RemoveAt(i);
                break;
            }
            else
            {
                KeyPaint[i].gameObject.GetComponent<Renderer>().material = ColorToPaint;
            }
        }


        if (Keys.Count == 0 && KeysInPosition.Count == 0 && KeyEffect.Count == 0 && KeyPaint.Count == 0) {
            if (KeyEffect.Count == 0)
            {

                for(int k = 0; k < ObjectWithoutPositionPrefat.Count; k++){
                    var objecto = ObjectWithoutPosition[k];
                  
                   
                    objecto.transform.SetParent(GameObject.Find("Game/Design").gameObject.transform);
                    objecto.transform.position = new Vector3(0,0,0);
                    objecto.transform.position = ObjectWithoutPositionPrefat[k].transform.position;
                    objecto.GetComponent<Rigidbody>().useGravity = true;
                    objecto.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    objecto.GetComponent<OVRGrabbable>().enabled = false;
                }
            }
            TutorialManager.Instance.CompletedTutorial();
        }
    }
    public class PosicionMaterial
    {
        public double x;
        public double y;
        public double z;
    }
}
