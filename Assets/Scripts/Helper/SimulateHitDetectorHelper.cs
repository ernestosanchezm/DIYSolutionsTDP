using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateHitDetectorHelper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponent<GameObject>();
        if (key != null)
        {
            var keyFeedback = other.gameObject.GetComponent<SimulateHitHelper>();
            if (keyFeedback.golpeado == false) {
                keyFeedback.EjecutarMovimiento();
            }
        }
    }
}
