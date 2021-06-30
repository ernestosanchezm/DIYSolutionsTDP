using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardHelper : MonoBehaviour
{
    private SoundHandlerHelper soundHandler;
    public bool keyHit = false;
    public bool keyCanBeHitAgain = false;
    private float originalZPosition;
    void Start()
    {
        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandlerHelper>();
        originalZPosition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyHit)
        {
            soundHandler.PlayKeyClick();
            keyCanBeHitAgain = false;
            keyHit = false;
            transform.position += new Vector3(0, 0, -0.03f);
        }
        if (transform.position.z < originalZPosition)
        {
            transform.position += new Vector3(0, 0, 0.005f);
        }
        else
        {
            keyCanBeHitAgain = true;
        }
    }
}
