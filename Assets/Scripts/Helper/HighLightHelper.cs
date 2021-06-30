using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightHelper : MonoBehaviour
{
    public string tag;
    public GameObject shapeZone;
    public Material lightMaterial;
    public Material defaultMaterial;
    public GameObject RightHandGameobject;

    void Update()
    {
        var Ray = new Ray(RightHandGameobject.transform.position, RightHandGameobject.transform.forward);

        RaycastHit hit;
        var selectionRender = new Renderer();

        if (Physics.Raycast(Ray, out hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(tag))
            {
                selectionRender = selection.GetComponent<Renderer>();
                if (selectionRender != null)
                {
                    //selectionRender.material = lightMaterial;
                    shapeZone.GetComponent<Renderer>().material = lightMaterial;
                    var objetosHijo = shapeZone.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
                    if (objetosHijo != null) {
                        foreach (var item in objetosHijo)
                        {
                            item.material = lightMaterial;
                        }
                    }
                }
            }
            else
            {
                //shapeZone.GetComponent<Renderer>().material = defaultMaterial;
                //var objetosHijo = shapeZone.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
                //if (objetosHijo != null)
                //{
                //    foreach (var item in objetosHijo)
                //    {
                //        item.material = defaultMaterial;
                //    }
                //}
            }
        }
        else
        {
            //shapeZone.GetComponent<Renderer>().material = defaultMaterial;
            //var objetosHijo = shapeZone.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
            //if (objetosHijo != null)
            //{
            //    foreach (var item in objetosHijo)
            //    {
            //        item.material = defaultMaterial;
            //    }
            //}
        }
    }

}
