using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> objectQueue;

    private void Start()
    {
        objectQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        if (objectQueue.Count == 0)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);
            return newObj;
        }
        
        GameObject pooledObj = objectQueue.Dequeue();
        pooledObj.SetActive(true);
        return pooledObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectQueue.Enqueue(obj);
    }
}