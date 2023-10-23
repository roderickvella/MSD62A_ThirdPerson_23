using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        //create singleton
        if (GameManager.Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        canvas = GameObject.Find("Canvas");
    }

    public void OnButtonPressed(string key)
    {
        switch (key)
        {
            case "TAB":
                canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
                break;
            case "J":
                canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(true);
                break;
            case "K":
                canvas.GetComponentInChildren<InventoryManager>().ChangeSelection(false);
                break;
            case "RETURN":
                canvas.GetComponentInChildren<InventoryManager>().ConfirmSelection();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    ShowToggleInventory();
        //}
    }

    //void ShowToggleInventory()
    //{
    //    //call method ShowToggleInventory which is a child found under the Canvas game object
    //    canvas.GetComponentInChildren<InventoryManager>().ShowToggleInventory();
    //}

   
}
