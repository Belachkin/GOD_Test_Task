using UnityEngine;
using Code.Enemy.States;

public class ChasingEnemyState : IEnemyState
{
    private Rigidbody2D _rb;
    private float _walkSpeed;
    private float _detectionRadius;
    private float _attackRadius;
    
    private bool isChasing = false;
    private Vector2 direction;
    private Animator _animator;
    private Health _health;
    
    private EnemyStateMashine _stateMashine;
    public ChasingEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, 
        float walkSpeed, float detectionRadius, 
        float attackRadius, Animator animator, Health health)
    {
        _stateMashine = stateMashine;
        _rb = rb;
        _walkSpeed = walkSpeed;
        _detectionRadius = detectionRadius;
        _attackRadius = attackRadius;
        _animator = animator;
        _health = health;
    }
    
    public void Enter()
    {
        _animator.SetBool("Attacking", false);
        isChasing = false;
    }

    public void Updater()
    {
        
        if (_health.Value <= 0)
        {
            _stateMashine.SetState<DeadEnemyState>();
        }
        
        Collider2D[] collidersForAttack = Physics2D.OverlapCircleAll(_rb.position, _attackRadius);
        foreach (Collider2D collider in collidersForAttack)
        {
            if (collider.CompareTag("Player"))
            {
                _stateMashine.SetState<AttackEnemyState>();
                break;
            }
        }
        
        
        Collider2D[] collidersForChasing = Physics2D.OverlapCircleAll(_rb.position, _detectionRadius);
        bool playerDetected = false;
        
        foreach (Collider2D collider in collidersForChasing)
        {
            if (collider.CompareTag("Player"))
            {
                var playerPosition = new Vector2(collider.transform.position.x, collider.transform.position.y);
                direction = (playerPosition - _rb.position).normalized;
                isChasing = true;
                playerDetected = true;
                break; // Нашли игрока, дальше проверять нет необходимости
            }
        }

        if (!playerDetected)
        {
            isChasing = false;
            _stateMashine.SetState<RandomWalkEnemyState>();
        }
    }

    public void FixedUpdater()
    {
        if (isChasing)
        {
            _rb.MovePosition(_rb.position + direction * _walkSpeed * Time.fixedDeltaTime);
            
            int tempDirection = direction.x < 0 ? -1 : 1;
            
            _animator.SetFloat("Direction", tempDirection);
        }
    }

    public void Exit()
    {
    }
}