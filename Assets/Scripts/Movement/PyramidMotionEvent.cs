using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class PyramidMotionEvent : MonoBehaviour
    {
        public float initialHeight = .5f;
        public float objectSpacing = .4f;
        public float rowSpacing = .5f;

        public void CreatePyramid()
        {
            foreach (var p in PlayerManager.Instance.playerList)
            {
                p.GetComponent<NavMeshAgent>().enabled = false;
                p.GetComponent<Rigidbody>().isKinematic = true;
            }

            StartCoroutine("PyramidSetTransformDuration");
        }

        private IEnumerator PyramidSetTransformDuration()
        {
            int objectCount = PlayerManager.Instance.playerList.Count;
            int maxRows = Mathf.CeilToInt(Mathf.Sqrt(2 * objectCount));
            Debug.Log(maxRows);
            float height = initialHeight;
            int objectIndex = 0;

            for (int row = maxRows; row >= 1; row--)
            {
                int objectsInRow = Mathf.Min(objectCount, row - 1);

                float startX = -(objectsInRow - 1) * objectSpacing / 2f;

                for (int i = 0; i < objectsInRow; i++)
                {
                    GameObject obj = PlayerManager.Instance.playerList[objectIndex];
                    float posX = startX + i * objectSpacing;
                    obj.transform.localPosition = new Vector3(posX, height, 0f);
                    objectIndex++;
                }

                height += objectSpacing + rowSpacing;
                objectCount -= objectsInRow;
                yield return new WaitForSeconds(0.1f);
            }

            // Place the remaining objects
            if (objectIndex < objectCount)
            {
                List<int> availableIndices = new List<int>();
                for (int i = objectIndex; i < objectCount; i++)
                {
                    availableIndices.Add(i);
                }

                for (int i = objectIndex; i < objectCount; i++)
                {
                    GameObject obj = PlayerManager.Instance.playerList[i];
                    int randomIndex = Random.Range(0, availableIndices.Count);
                    int replacementIndex = availableIndices[randomIndex];
                    availableIndices.RemoveAt(randomIndex);
                    GameObject replacementObj = PlayerManager.Instance.playerList[replacementIndex];
                    obj.transform.localPosition = replacementObj.transform.localPosition;
                }
            }
        }
    }
}