using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Utilities
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField] private List<GameObject> objectList;
        [SerializeField] private int initialPoolSize;
        private Dictionary<ObjectTypes.Type, Queue<GameObject>> objectQueues;

        private void Start()
        {
            objectQueues = new Dictionary<ObjectTypes.Type, Queue<GameObject>>();

            foreach (GameObject obj in objectList)
            {
                ObjectTypes objectType = obj.GetComponent<ObjectTypes>();

                if (!objectQueues.ContainsKey(objectType.objectType))
                    objectQueues.Add(objectType.objectType, new Queue<GameObject>());

                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject newObj = Instantiate(obj);
                    newObj.SetActive(false);
                    objectQueues[objectType.objectType].Enqueue(newObj);
                }
            }
        }

        public GameObject GetPooledObject(ObjectTypes.Type objectType)
        {
            if (objectQueues.ContainsKey(objectType) && objectQueues[objectType].Count > 0)
            {
                
                GameObject obj = objectQueues[objectType].Dequeue();
                obj.SetActive(true);
                return obj;
            }
            
                GameObject newObj = Instantiate(GetPrefabByType(objectType));
                newObj.SetActive(true);
                return newObj;
            
        }
        private GameObject GetPrefabByType(ObjectTypes.Type objectType)
        {
            foreach (GameObject obj in objectList)
            {
                ObjectTypes objectTypesScript = obj.GetComponent<ObjectTypes>();
                if (objectTypesScript != null && objectTypesScript.objectType == objectType)
                {
                    return obj;
                }
            }
            return null;
        }

        public void ReturnToPool(GameObject currentObject)
        {
            ObjectTypes objectType = currentObject.GetComponent<ObjectTypes>();
            
            currentObject.SetActive(false);
            objectQueues[objectType.objectType].Enqueue(currentObject);
        }
        
        
    }
}