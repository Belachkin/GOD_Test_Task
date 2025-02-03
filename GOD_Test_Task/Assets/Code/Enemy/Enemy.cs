using Code.Enemy.States;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float changeDirectionInterval = 0.5f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float attackRadius = 2f;
    
    [SerializeField] private int damage = 1;
    [SerializeField] private int attackCooldown = 1;
    
    [SerializeField] private Health health;
    
    private EnemyStateMashine stateMashine;
    
    private void Start()
    {
        stateMashine = new EnemyStateMashine();

        stateMashine.AddState(new RandomWalkEnemyState(stateMashine, rb, 
                                                        walkSpeed, changeDirectionInterval, 
                                                        detectionRadius, animator, health));
        stateMashine.AddState(new ChasingEnemyState(stateMashine, rb, 
                                                    walkSpeed, detectionRadius, 
                                                    attackRadius, animator, health));
        stateMashine.AddState(new AttackEnemyState(stateMashine, rb, 
                                                    attackRadius, damage, 
                                                    attackCooldown, animator, health));
        
        stateMashine.AddState(new DeadEnemyState(stateMashine, animator));
        
        stateMashine.SetState<RandomWalkEnemyState>();
    }

    private void Update()
    {
        stateMashine.Update();
    }

    private void FixedUpdate()
    {
        stateMashine.FixedUpdate();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
