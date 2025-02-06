using UnityEngine;

namespace Code.Enemy.States
{
    public class AttackEnemyState : IEnemyState
    {
        private float _attackRadius;
        private Rigidbody2D _rb;
        
        private Transform _attackPoint;
        private float _attackRange;
        private Animator _animator;
        private Health _health;

        private Transform playerTransform;
        private EnemyStateMashine _stateMashine;
        public AttackEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb , 
            float attackRadius, int damage, 
            float attackCooldown, Animator animator, Health health)
        {
            _stateMashine = stateMashine;

            _rb = rb;
            _attackRadius = attackRadius;
            /*_damage = damage;
            _attackCooldown = attackCooldown;*/
            _animator = animator;
            _health = health;
        }
        public void Enter()
        {
            
        }

        public void Updater()
        {
            if (_health.Value <= 0)
            {
                _stateMashine.SetState<DeadEnemyState>();
            }
            
            /*timeSinceLastAttack += Time.deltaTime;*/
            
            bool playerDetected = false;
            
            Collider2D[] collidersForAttack = Physics2D.OverlapCircleAll(_rb.position, _attackRadius);
            foreach (Collider2D collider in collidersForAttack)
            {
                if (collider.CompareTag("Player"))
                {
                    /*playerHealth = collider.GetComponent<Health>();*/
                    playerTransform = collider.transform;
                    playerDetected = true;
                    break;
                }
            }
            
            /*if (playerDetected && timeSinceLastAttack >= _attackCooldown)
            {
                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage);
                }
                
                timeSinceLastAttack = 0f; 
            }*/

            

            if (playerDetected)
            {
                /*float deltaX = playerTransform.position.x - _rb.position.x;
                
                int direction = deltaX < 0 ? -1 : 1; 
                
                Debug.Log($"{deltaX} {direction}");
                
                _animator.SetFloat("Direction", direction);*/
                _animator.SetBool("Attacking", true);
            }
            else
            {
                _animator.SetBool("Attacking", false);
            }
            
            
            if (!playerDetected)
            {
                _stateMashine.SetState<RandomWalkEnemyState>();
            }
        }

        public void FixedUpdater()
        {
            
        }

        public void Exit()
        {
           
        }
    }
}