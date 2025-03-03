using UnityEngine;

namespace Code.Enemy.States
{
    public class RandomWalkEnemyState : IEnemyState
    {
        private Rigidbody2D _rb;
        private float _walkSpeed;
        private float _changeDirectionInterval;
        private float _nextChangeTime;
        private float _detectionRadius;
        private Health _health;

        private EnemyStateMashine _stateMashine;
        private Vector2 travelDirection = Vector2.zero;
        private Animator _animator;
        
        private Vector2 _minBounds;
        private Vector2 _maxBounds;
        
        public RandomWalkEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb,
            float walkSpeed,
            float changeDirectionInterval, float detectionRadius, Animator animator, Health health, Vector2 minBounds, Vector2 maxBounds)
        {
            _stateMashine = stateMashine;

            _rb = rb;
            _walkSpeed = walkSpeed;
            _changeDirectionInterval = changeDirectionInterval;
            _detectionRadius = detectionRadius;
            _animator = animator;
            _health = health;
            
            _minBounds = minBounds; 
            _maxBounds = maxBounds; 
        }

        public void Enter()
        {
            _animator.SetBool("Attacking", false);
            _nextChangeTime = Time.time + _changeDirectionInterval;
            GenerateNewDirection();
        }
        public void Updater()
        {
            if (_health.Value <= 0)
            {
                _stateMashine.SetState<DeadEnemyState>();
            }

            if (Time.time >= _nextChangeTime)
            {
                GenerateNewDirection();

                _nextChangeTime = Time.time + _changeDirectionInterval;
            }

            TryDetectionPlayer();
        }
        public void FixedUpdater()
        {
            if (travelDirection != _rb.position)
            {
                int tempDirection = travelDirection.x < 0 ? -1 : 1;
                
                Vector2 nextPosition = _rb.position + travelDirection * _walkSpeed * Time.fixedDeltaTime;
                
                
                _animator.SetFloat("Direction", tempDirection);

                if (nextPosition.x > _minBounds.x && 
                    nextPosition.x < _maxBounds.x && 
                    nextPosition.y < _minBounds.y && 
                    nextPosition.y > _maxBounds.y)
                {
                    _rb.MovePosition(_rb.position + travelDirection * _walkSpeed * Time.fixedDeltaTime);
                }
                
                
            }
        }

        public void Exit()
        {
            
        }

        private void GenerateNewDirection()
        {
            travelDirection = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)).normalized;
        }
        private void TryDetectionPlayer()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_rb.position, _detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    
                    _stateMashine.SetState<ChasingEnemyState>();
                    return;
                }
            }
        }
    }
}