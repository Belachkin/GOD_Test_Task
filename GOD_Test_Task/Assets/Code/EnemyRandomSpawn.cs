using UnityEngine;

namespace Code
{
    public class EnemyRandomSpawn : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private Vector2 _maxBounds;
        [SerializeField] private Vector2 _minBounds;
        
        [SerializeField] private GameObject _enemyPrefab;
        private void Start()
        {
            for (int i = 0; i < _count; i++)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(_minBounds.x, _maxBounds.x),
                    Random.Range(_minBounds.y, _maxBounds.y),
                    0f
                );
                
                Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Vector3 bottomLeft = new Vector3(_minBounds.x, _minBounds.y, 0);
            Vector3 bottomRight = new Vector3(_maxBounds.x, _minBounds.y, 0);
            Vector3 topLeft = new Vector3(_minBounds.x, _maxBounds.y, 0);
            Vector3 topRight = new Vector3(_maxBounds.x, _maxBounds.y, 0);
            
            Gizmos.DrawLine(bottomLeft, bottomRight); // Нижняя сторона
            Gizmos.DrawLine(bottomRight, topRight);   // Правая сторона
            Gizmos.DrawLine(topRight, topLeft);       // Верхняя сторона
            Gizmos.DrawLine(topLeft, bottomLeft);     // Левая сторона
        }
        
    }
}