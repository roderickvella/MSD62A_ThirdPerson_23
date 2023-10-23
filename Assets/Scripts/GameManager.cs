using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static GameObject canvas;

    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        //create singleton
        if (GameManager.Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        canvas = GameObject.Find("Canvas");

        //when the game starts, set the gameState to Area A (this is becuase the player spawns here)
        gameState = GameState.AreaA;
    }

    public void OnChangeGameState(GameState gameState)
    {
        print("Changing game state to:" + gameState.ToString());
        this.gameState = gameState;
    }

    public void OnButtonPressed(string key)
    {
        if(gameState == GameState.AreaA)
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
        else if (gameState == GameState.AreaB)
        {
            switch (key)
            {
                case "TAB":
                    print("Hide coin in area b");
                    GameObject.Find("Plane2/Coin").SetActive(false);
                    break;
                default:
                    break;
            }
        }
     
    }

    public void OnMouseButtonPressed(int mouse)
    {
        if(gameState == GameState.AreaA)
        {
            switch (mouse)
            {
                case 0:
                    GameObject.Find("Player").GetComponent<PlayerManager>().ThrowGrenade();
                    break;
            }
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

    public enum GameState
    {
        AreaA,
        AreaB
    }
   
}
