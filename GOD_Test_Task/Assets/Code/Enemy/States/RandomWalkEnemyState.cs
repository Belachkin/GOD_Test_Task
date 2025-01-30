using UnityEngine;

namespace Code.Enemy.States
{
    public class RandomWalkEnemyState : IEnemyState
    {
        
        private Rigidbody2D _rb;
        private float _walkSpeed;
        private float _travelDistance;
        public float _changeDirectionInterval;
        private float _nextChangeTime;
        
        private EnemyStateMashine _stateMashine;
        private Vector2 travelDirection = Vector2.zero;
        
        public RandomWalkEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, float walkSpeed, float travelDistance, float changeDirectionInterval)
        {
            _stateMashine = stateMashine; 
            
            _rb = rb;
            _walkSpeed = walkSpeed;
            _travelDistance = travelDistance;
            _changeDirectionInterval = changeDirectionInterval;
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
        
        void GenerateNewDirection()
        {
            // Генерация нового случайного направления
            float angle = Random.Range(0, 2 * Mathf.PI);
            travelDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        }
        
        public void Exit()
        {
            Debug.Log("Exit RndWalk");
        }
    }
}