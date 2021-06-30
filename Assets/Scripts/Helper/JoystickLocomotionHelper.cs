using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickLocomotionHelper : MonoBehaviour
{
    public Rigidbody player;
    public float spped;
    private float subida;
    void Start()
    {
        
    }

    void Update()
    {
        var joystickAxis = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick, OVRInput.Controller.LTouch);
        float fixedY = player.position.y + subida;
        player.position += (transform.right * joystickAxis.x + transform.forward * joystickAxis.y) * Time.deltaTime * spped;
        player.position = new Vector3(player.position.x, fixedY, player.position.z); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Subida1" || other.tag == "Subida2")
        {
            subida = (float)0.5;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Subida1" || other.tag == "Subida2")
        {
            subida = (float)0.0;
            player.position = new Vector3(player.position.x, (float)2.22, player.position.z);
        }
       
    }
}
