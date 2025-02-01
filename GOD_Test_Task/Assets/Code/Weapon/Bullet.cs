using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _speed = 10f;

    public Vector2 Direction;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.velocity = Direction.normalized * _speed;

        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
    
    
}
