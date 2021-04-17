using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : Singleton<EnemyGeneration>
{
    [SerializeField] GameObject collectionItem;
    CollectLevelManager levelManager;
    [SerializeField] Transform topRight;
    [SerializeField] Transform bottomLeft;
    // Start is called before the first frame update
    void Start()
    {
        generate();
        levelManager = GameObject.FindObjectOfType<CollectLevelManager>();
        if (!levelManager)
        {
            Debug.LogError("collect level manager does not exist");
        }
    }

    public void generate()
    {
        Vector3 position = new Vector3(Random.Range(bottomLeft.position.x, topRight.position.x), Random.Range(bottomLeft.position.y, topRight.position.y), 0);



        GameObject collection = Instantiate(collectionItem, position, Quaternion.identity);
    }
    public void collect()
    {
        if (levelManager)
        {
            levelManager.collect();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
