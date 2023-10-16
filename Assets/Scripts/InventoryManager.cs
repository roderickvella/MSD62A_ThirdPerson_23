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

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }


    public class InventoryItem
    {
        public ItemScriptableObject item;
        public int quantity;
    }
}
