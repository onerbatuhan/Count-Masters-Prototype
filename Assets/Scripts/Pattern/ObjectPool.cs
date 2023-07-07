using System.Collections.Generic;
using UnityEngine;

namespace Pattern
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objectList;
        [SerializeField] private int initialPoolSize;
        private Dictionary<string, Queue<GameObject>> objectQueues;

        private void Start()
        {
            objectQueues = new Dictionary<string, Queue<GameObject>>();

            foreach (GameObject obj in objectList)
            {
                string tag = obj.tag;

                if (!objectQueues.ContainsKey(tag))
                    objectQueues.Add(tag, new Queue<GameObject>());

                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject newObj = Instantiate(obj);
                    newObj.SetActive(false);
                    objectQueues[tag].Enqueue(newObj);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            if (objectQueues.ContainsKey(tag) && objectQueues[tag].Count > 0)
            {
                GameObject obj = objectQueues[tag].Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return null;
        }

        public void ReturnToPool(GameObject obj)
        {
            string tag = obj.tag;
            obj.SetActive(false);
            objectQueues[tag].Enqueue(obj);
        }
    }
}