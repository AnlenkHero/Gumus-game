using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private GameObject _player;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        PlayerController.OnGameOver += DestroyWithDelay;
    }

    void Start()
    {
        _player = PlayerController.Instance;
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        if (transform.position.y > -0.5f)
        {
            Vector3 lookDirection = (_player.transform.position - transform.position).normalized; 
            _rigidbody.AddForce(lookDirection * speed * Time.deltaTime);
        }
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
        
    }

    private void DestroyWithDelay()
    {
        DestroyCoroutine();
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
