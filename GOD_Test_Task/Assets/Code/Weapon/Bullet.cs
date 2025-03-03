using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _speed = 10f;

    public Vector2 Direction;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = Direction.normalized * _speed;

        StartCoroutine(Lifetime());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
    
    
}
