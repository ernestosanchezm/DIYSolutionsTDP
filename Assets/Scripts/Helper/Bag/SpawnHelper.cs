using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelper : MonoBehaviour
{
    private Transform playerPos;
    public GameObject item;

    private void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("RightHand").GetComponent<Transform>();
    }

    public void SpawnItem()
    {
        Instantiate(item, playerPos.position, Quaternion.identity);
    }
}
