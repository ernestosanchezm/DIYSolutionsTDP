using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PickupHelper : MonoBehaviour
{
    private InventoryHelper inventory;
    public GameObject itemButton;
    public GameObject effect;
    public GameObject parentInitial;

    public Material OriginalColor;
    public Material ErrorMaterial;

    public GameObject PositionObject;
    public Material ColorMaterialPosition;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("RightHand").GetComponent<InventoryHelper>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightHand"))
        {
            if (OVRInput.Get(OVRInput.RawButton.A))
            {
                for (int i = 0; i < inventory.items.Length; i++)
                {
                    if (inventory.items[i] == 0) {

                        var lstKeyTutorial = GameObject.Find("Game/TutorialManager/Tutorial").GetComponents<KeyTutorial>();
                        var order = Convert.ToInt32(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/CanvasContador/BackgroundTutorial/TutorialNum").GetComponent<Text>().text) - 1;
                        var keysTutorials = lstKeyTutorial.OrderBy(x=>x.Order).ToList();

                        if (!keysTutorials[order].NoKeys.Any(x => x.gameObject.name == this.transform.name))
                        {
                            inventory.items[i] = 1; // makes sure that the slot is now considered FULL
                            Instantiate(itemButton, inventory.slots[i].transform, false); // spawn the button so that the player can interact with it
                            GetComponent<Rigidbody>().useGravity = true;
                            this.transform.SetParent(parentInitial.transform);
                            gameObject.SetActive(false);

                            if (PositionObject != null) {
                                PositionObject.GetComponent<Renderer>().material = ColorMaterialPosition;
                                var objetosHijo = PositionObject.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
                                if (objetosHijo != null)
                                {
                                    foreach (var item in objetosHijo)
                                    {
                                        item.material = ColorMaterialPosition;
                                    }
                                } 
                            }

                            break;
                        }
                        else
                        {
                            this.transform.GetComponent<Renderer>().material = ErrorMaterial;
                            var objetosHijo = this.transform.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
                            if (objetosHijo != null)
                            {
                                foreach (var item in objetosHijo)
                                {
                                    item.material = ErrorMaterial;
                                }
                            }
                        }    
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.transform.GetComponent<Renderer>().material = OriginalColor;
        var objetosHijo = this.transform.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
        if (objetosHijo != null)
        {
            foreach (var item in objetosHijo)
            {
                item.material = OriginalColor;
            }
        }
    }
}
