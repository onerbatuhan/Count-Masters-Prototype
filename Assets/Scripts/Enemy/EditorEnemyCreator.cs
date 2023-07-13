
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Enemy
{
    public class EditorEnemyCreator : MonoBehaviour
    {
        [SerializeField] private GameObject referenceObject;
        [SerializeField] private int enemyCount;
        [SerializeField] private Transform parentTransform;
        private float _smallCircleRadius = .3f;
        private float _smallCircleSpacing = 1;
        private GameObject _enemyGroupObject;
        
        

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, transform.up, 2f);

            DrawSmallCircles();
#endif
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(EditorEnemyCreator))]
        
        public class EditorObjectSpawnerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                EditorEnemyCreator spawner = (EditorEnemyCreator)target;
                if (GUILayout.Button("Create Enemy Group"))
                {
                    spawner.CreateEnemyGroup();
                }
            }
        }
#endif

        private void DrawSmallCircles()
        {
#if UNITY_EDITOR
            Handles.color = Color.red;
            float angleStep = 360f / enemyCount;
            float totalSpacing = _smallCircleSpacing * (enemyCount - 1);
            float adjustedRadius = _smallCircleRadius + _smallCircleSpacing;

            Quaternion rotation = Quaternion.AngleAxis(angleStep, transform.up);
            Vector3 direction = transform.forward * adjustedRadius;

            for (int i = 0; i < enemyCount; i++)
            {
                Handles.DrawWireDisc(transform.position + direction, transform.up, _smallCircleRadius);
                direction = rotation * direction;
            }
#endif
        }

        private void CreateEnemyGroup()
        {
            _enemyGroupObject = Instantiate(referenceObject,parentTransform);
            _enemyGroupObject.GetComponent<EnemyController>().enemyCount = enemyCount;
            _enemyGroupObject.transform.position = transform.position;
        }
    }
}