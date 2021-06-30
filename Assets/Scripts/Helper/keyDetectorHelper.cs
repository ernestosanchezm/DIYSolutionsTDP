using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class keyDetectorHelper : MonoBehaviour
{
    private InputField playertextOutput;
    void Start()
    {
        playertextOutput = GameObject.FindGameObjectWithTag("InputName").GetComponent<InputField>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponentInChildren<TextMeshPro>();
        if (key != null)
        {
            //if (OVRInput.Get(OVRInput.RawButton.X, OVRInput.Controller.LTouch) || OVRInput.Get(OVRInput.RawButton.A, OVRInput.Controller.RTouch))
            if (OVRInput.Get(OVRInput.RawButton.X) || OVRInput.Get(OVRInput.RawButton.A))
            {
                var keyFeedback = other.gameObject.GetComponent<KeyBoardHelper>();
                keyFeedback.keyHit = true;
                if (other.gameObject.GetComponent<KeyBoardHelper>().keyCanBeHitAgain)
                {
                    if (key.text == "ESPACIO")
                    {
                        playertextOutput.text += " ";
                    }
                    else if (key.text == "BORRAR")
                    {
                        playertextOutput.text = playertextOutput.text.Substring(0, playertextOutput.text.Length - 1);
                    }
                    else
                    {
                        playertextOutput.text += key.text;
                    }
                }
            }
        }
    }


}