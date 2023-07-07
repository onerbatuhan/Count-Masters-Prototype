using System.Collections.Generic;
using UnityEngine;

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

            return null;
        }

        public void ReturnToPool(GameObject obj)
        {
            ObjectTypes objectType = obj.GetComponent<ObjectTypes>();
            obj.SetActive(false);
            objectQueues[objectType.objectType].Enqueue(obj);
        }
    }
}