using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] int maxItems;
    Queue<GameObject> itemQueue = new Queue<GameObject>();

    [SerializeField] GameObject itemPrefab;

    [SerializeField] Transform parent;

    GameObject CreateOneItem()
    {
        GameObject item;
        item = Instantiate(itemPrefab);
        item.transform.parent = transform;
        item.SetActive(false);
        itemQueue.Enqueue(item);
        return itemPrefab;
    }

    void PrepareItems()
    {
        for (int i = 0; i < maxItems; i++)
        {
            CreateOneItem();
        }
    }

    public GameObject getItem()
    {
        if (itemQueue.Count > 0)
        {
            GameObject coin = itemQueue.Dequeue();
            return coin;
        }
        else
        {
               return CreateOneItem();

        }
    }

    public void returnItem(GameObject returnedItem)
    {
        itemQueue.Enqueue(returnedItem);
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (parent)
        {
            parent = transform;
        }
        PrepareItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
