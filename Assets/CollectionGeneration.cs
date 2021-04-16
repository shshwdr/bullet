using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGeneration : Singleton<CollectionGeneration>
{
    [SerializeField] GameObject collectionItem;
    [SerializeField] Transform topRight;
    [SerializeField] Transform bottomLeft;
    // Start is called before the first frame update
    void Start()
    {
        generate();
    }

    public void generate()
    {
        Vector3 position = new Vector3(Random.Range(bottomLeft.position.x, topRight.position.x), Random.Range(bottomLeft.position.y, topRight.position.y), 0);
        GameObject collection = Instantiate(collectionItem, position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
