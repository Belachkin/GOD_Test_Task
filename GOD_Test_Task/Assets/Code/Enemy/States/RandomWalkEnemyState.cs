using UnityEngine;

namespace Code.Enemy.States
{
    public class RandomWalkEnemyState : IEnemyState
    {
        
        private Rigidbody2D _rb;
        private float _walkSpeed;
        private float _travelDistance;
        private float _changeDirectionInterval;
        private float _nextChangeTime;
        private float _detectionRadius;
        
        private EnemyStateMashine _stateMashine;
        private Vector2 travelDirection = Vector2.zero;
        
        public RandomWalkEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, 
                                    float walkSpeed, float travelDistance, 
                                    float changeDirectionInterval, float detectionRadius)
        {
            _stateMashine = stateMashine; 
            
            _rb = rb;
            _walkSpeed = walkSpeed;
            _travelDistance = travelDistance;
            _changeDirectionInterval = changeDirectionInterval;
            _detectionRadius = detectionRadius;
        }
        
        public void Enter()
        {
            Debug.Log("Enter RndWalk");
            _nextChangeTime = Time.time + _changeDirectionInterval;
            GenerateNewDirection();
        }

        public void Updater()
        {
            if (Time.time >= _nextChangeTime)
            {
                GenerateNewDirection();
                
                _nextChangeTime = Time.time + _changeDirectionInterval;
            }
            
            TryDetectionPlayer();
            
            // if (travelDirection != _rb.position)
            // {
            //     travelDirection = new Vector2(Random.Range(_rb.position.x + -_travelDistance, _rb.position.x + _travelDistance), 
            //                                     Random.Range(_rb.position.y + -_travelDistance, _rb.position.y + _travelDistance));
            // }
        }

        public void FixedUpdater()
        {
            if (travelDirection != _rb.position)
            {
                _rb.MovePosition(_rb.position + travelDirection * _walkSpeed * Time.fixedDeltaTime); 
            }
        }
        
        public void Exit()
        {
            Debug.Log("Exit RndWalk");
            _stateMashine.SetState<ChasingEnemyState>();
        }
        
        private void GenerateNewDirection()
        {
            // Генерация нового случайного направления
            float angle = Random.Range(0, 2 * Mathf.PI);
            travelDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        }

        private void TryDetectionPlayer()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_rb.position, _detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("PlayerEnterZone");
                    Exit();
                    return;
                }
            }
        }
        
        
        //DEBUG:
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_rb.position, _detectionRadius);
        }
    }
}