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
        public RandomWalkEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, 
                                    float walkSpeed, 
                                    float changeDirectionInterval, float detectionRadius, Animator animator, Health health)
        {
            _stateMashine = stateMashine; 
            
            _rb = rb;
            _walkSpeed = walkSpeed;
            _changeDirectionInterval = changeDirectionInterval;
            _detectionRadius = detectionRadius;
            _animator = animator; 
            _health = health;
        }
        
        public void Enter()
        {
            Debug.Log("Enter RndWalk");
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
                _rb.MovePosition(_rb.position + travelDirection * _walkSpeed * Time.fixedDeltaTime); 
                
            }
        }
        
        public void Exit()
        {
            Debug.Log("Exit RndWalk");
            
        }
        
        private void GenerateNewDirection()
        {
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
                    _stateMashine.SetState<ChasingEnemyState>();
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