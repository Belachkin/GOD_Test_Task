using UnityEngine;

namespace Code.Enemy.States
{
    public class ChasingEnemyState : IEnemyState
    {
        private Rigidbody2D _rb;
        private float _walkSpeed;
        private float _detectionRadius;
        
        private bool isChasing = false;
        private Vector2 direction;
        
        private EnemyStateMashine _stateMashine;
        public ChasingEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, float walkSpeed, float detectionRadius)
        {
            _stateMashine = stateMashine;

            _rb = rb;
            _walkSpeed = walkSpeed;
            _detectionRadius = detectionRadius;
        }
        
        public void Enter()
        {
            Debug.Log("EnterChasingState");
            isChasing = false;
        }

        public void Updater()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_rb.position, _detectionRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    var playerPosition = new Vector2(collider.transform.position.x, collider.transform.position.y);
                    direction = (playerPosition - _rb.position).normalized;
                    isChasing = true;
                    return;
                }
                else
                {
                    _stateMashine.SetState<RandomWalkEnemyState>();
                }
            }
        }

        public void FixedUpdater()
        {
            if (isChasing == true)
            {
                _rb.MovePosition(_rb.position + direction * _walkSpeed * Time.fixedDeltaTime);
            }
        }

        public void Exit()
        {
            Debug.Log("ExitChasingState");
        }
    }
}