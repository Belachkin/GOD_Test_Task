using UnityEngine;

namespace Code.Enemy.States
{
    public class AttackEnemyState : IEnemyState
    {
        private float _attackRadius;
        private int _damage;
        private Rigidbody2D _rb;
        private float _attackCooldown;
        
        private float timeSinceLastAttack = 0f;
        private Health playerHealth;


        private Transform _attackPoint;
        private float _attackRange;
        private Animator _animator;
        
        private EnemyStateMashine _stateMashine;
        public AttackEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb , float attackRadius, int damage, float attackCooldown, Animator animator)
        {
            _stateMashine = stateMashine;

            _rb = rb;
            _attackRadius = attackRadius;
            _damage = damage;
            _attackCooldown = attackCooldown;
            _animator = animator;
        }
        public void Enter()
        {
            Debug.Log("AttackState Enter");
        }

        public void Updater()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            bool playerDetected = false;
            
            Collider2D[] collidersForAttack = Physics2D.OverlapCircleAll(_rb.position, _attackRadius);
            foreach (Collider2D collider in collidersForAttack)
            {
                if (collider.CompareTag("Player"))
                {
                    playerHealth = collider.GetComponent<Health>();
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
            Debug.Log("AttackState Exit");
        }
    }
}