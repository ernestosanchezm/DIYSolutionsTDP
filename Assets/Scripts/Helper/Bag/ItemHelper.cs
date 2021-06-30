using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHelper : MonoBehaviour
{
    private Transform player;
    //public GameObject explosionEffect;
    public string root;
    public string rootPosition;
    public Material ColorPositionBlue;

    private void Start()    
    {
        player = GameObject.FindGameObjectWithTag("LeftHand").transform;
    }

    public void Use()
    {
        //Instantiate(explosionEffect, player.transform.position, Quaternion.identity);
        //var root = "Game/Design/Puerta/" + tag;
        var activation = GameObject.Find(root).gameObject;
        activation.SetActive(true);

        activation.transform.position = player.position;
        activation.GetComponent<Rigidbody>().useGravity = false;
        activation.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY 
            | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        activation.GetComponent<OVRGrabbable>().enabled = false;

        player.GetComponent<OVRGrabber>().enabled = false;
        player.GetComponent<SphereCollider>().enabled = false;
        activation.transform.SetParent(player.transform);
        Destroy(gameObject);

        //OBJECT POSITION
        if (!string.IsNullOrEmpty(rootPosition))
        {
            var activationPosition = GameObject.Find(rootPosition).transform;
            activationPosition.GetComponent<Renderer>().material = ColorPositionBlue;
            var objetosHijo = activationPosition.GetComponentsInChildren<Renderer>();// = lightMaterial;ç
            if (objetosHijo != null)
            {
                foreach (var item in objetosHijo)
                {
                    item.material = ColorPositionBlue;
                }
            }
        }
    }
}
