using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("Number of items")]
    public int numberOfItems = 5;

    [Tooltip("Items selection panel")]
    public GameObject itemsSelectionPanel;

    [Tooltip("List of Items")]
    public List<ItemScriptableObject> itemsAvailable;

    private List<InventoryItem> itemsForPlayer;

    public int currentSelectedIndex = 0; //by default start/select the first button in our inventory system

    [Tooltip("Selected Item Colour")]
    public Color selectedColour;

    [Tooltip("Not Selected Item Colour")]
    public Color notSelectedColour;

    [Tooltip("Show Inventory on GUI")]
    public bool showInventory = false;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        //load the controller so what we can play the animations (inventoryIn/inventoryOut)
        animator = itemsSelectionPanel.GetComponent<Animator>();

        itemsForPlayer = new List<InventoryItem>();

        PopulateInventorySpawn();
        RefreshInventoryGUI();
    }

    private void RefreshInventoryGUI()
    {
        int buttonId = 0;
        foreach(InventoryItem i in itemsForPlayer)
        {
            //load the button
            GameObject button = itemsSelectionPanel.transform.Find("Button" + buttonId).gameObject;

            //search for the child image and change it's sprite
            button.transform.Find("Image").GetComponent<Image>().sprite = i.item.icon;

            //change the quantity
            button.transform.Find("Quantity").GetComponent<TextMeshProUGUI>().text = "x" + i.quantity;

            //check if it is selected
            if(buttonId == currentSelectedIndex)
            {
                button.GetComponent<Image>().color = selectedColour; //set to green
            }
            else
            {
                button.GetComponent<Image>().color = notSelectedColour; //set to white
            }

            //increment the button id by 1 to move to the next button
            buttonId += 1;
        }

        //set active false redundant buttons
       for(int i=buttonId; i<3; i++)
       {
            itemsSelectionPanel.transform.Find("Button" + i).gameObject.SetActive(false);
       }




    }

    private void PopulateInventorySpawn()
    {
        for(int i=0; i<numberOfItems; i++)
        {
            //pick random object from itemsAvailable
            ItemScriptableObject objItem = itemsAvailable[Random.Range(0, itemsAvailable.Count)];
            print(objItem.title);

            //check whether objItem exists in itemsForPlayer. So basically we need to count how many items
            //we've got if type objItem inside itemsForPlayer
            int countItems = itemsForPlayer.Where(x => x.item == objItem).ToList().Count;
            if(countItems == 0)
            {
                //add objItem with quantity 1 because it is the first type inside itemsForPlayer
                itemsForPlayer.Add(new InventoryItem() { item = objItem, quantity = 1 });
            }
            else
            {
                //we find the first occurance of this type (like objItem)
                var item = itemsForPlayer.First(x => x.item == objItem);
                //we update the quantity by increasing 1
                item.quantity += 1;
            }


        }
    }

    public void ChangeSelection(bool moveLeft)
    {
        if (moveLeft)
        {
            currentSelectedIndex -= 1; //move left
        }
        else 
        {
            currentSelectedIndex += 1; //move right
        }

        //check for boundaries
        if (currentSelectedIndex < 0)
            currentSelectedIndex = 0;

        if (currentSelectedIndex == itemsForPlayer.Count)
            currentSelectedIndex = currentSelectedIndex - 1;

        RefreshInventoryGUI();
    }

    public void ConfirmSelection()
    {
        //get the item from the itemsForPlayer list using the currentSelectedIndex
        InventoryItem inventoryItem = itemsForPlayer[currentSelectedIndex];
        print("Inventory Item Selected is:" + inventoryItem.item.name);

        //reduce the quantity
        inventoryItem.quantity -= 1;

        if(inventoryItem.quantity == 0)
        {
            //remove the item from the array using the index
            itemsForPlayer.RemoveAt(currentSelectedIndex);
        }

        //redraw everything (update the text in the label etc)
        RefreshInventoryGUI();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.J) || (Input.GetKeyDown(KeyCode.K))) {
        //    ChangeSelection();
        //}
        //else if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    ConfirmSelection();
        //}
    }

    //[ContextMenu("ToggleInventory")]
    public void ShowToggleInventory()
    {
        if(showInventory == false)
        {
            showInventory = true;
            animator.SetTrigger("InventoryIn");
        }
        else
        {
            showInventory = false;
            animator.SetTrigger("InventoryOut");
        }
    }


    public class InventoryItem
    {
        public ItemScriptableObject item;
        public int quantity;
    }
}
