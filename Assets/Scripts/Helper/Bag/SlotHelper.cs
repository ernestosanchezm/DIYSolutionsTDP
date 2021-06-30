using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHelper : MonoBehaviour
{
    private InventoryHelper inventory;
    public int index;

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("RightHand").GetComponent<InventoryHelper>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.items[index] = 0;
        }
    }

    public void Cross()
    {
        var hijo = this.transform;
        foreach (Transform child in transform)
        {
            var root = child.GetComponent<ItemHelper>().root;
            var activation = GameObject.Find(root).gameObject;
            activation.SetActive(true);
            GameObject.Destroy(child.gameObject);
        }
        //var activation = GameObject.Find("").gameObject;
        //activation.SetActive(true);
        //Destroy(gameObject);
    }
}
