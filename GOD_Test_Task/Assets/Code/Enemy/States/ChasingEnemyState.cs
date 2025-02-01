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
    
    private EnemyStateMashine _stateMashine;
    public ChasingEnemyState(EnemyStateMashine stateMashine, Rigidbody2D rb, float walkSpeed, float detectionRadius, float attackRadius)
    {
        _stateMashine = stateMashine;
        _rb = rb;
        _walkSpeed = walkSpeed;
        _detectionRadius = detectionRadius;
        _attackRadius = attackRadius;
    }
    
    public void Enter()
    {
        Debug.Log("EnterChasingState");
        isChasing = false;
    }

    public void Updater()
    {
        
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
        }
    }

    public void Exit()
    {
        Debug.Log("ExitChasingState");
    }
}