using System;
using DefaultNamespace;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private float speed = 15.0f;
    private bool _homing;
    [SerializeField]
    private float rocketStrength = 30.0f;
    [SerializeField]
    private float aliveTimer = 5.0f;
    [SerializeField]
    private ParticleSystem smokeParticle;

    void Update()
    {
        if (_homing && _target != null)
        {
            Vector3 moveDirection = (_target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(_target);
        }

        if (_target == null)
        {
            Destroy(gameObject);
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        Enemy enemy = col.collider.GetComponent<Enemy>();
        PlayerController player = col.collider.GetComponent<PlayerController>();
        
        if (enemy || player)
        {
            Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
            Vector3 away = -col.contacts[0].normal;
            targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse); 
            Destroy(gameObject);
        }

        Ground ground = col.collider.GetComponent<Ground>();
        
        if (ground)
        {
            Destroy(gameObject);
        }


    }
    
    public void Fire(Transform newTarget)
    {
        _target = newTarget;
        _homing = true;
        Destroy(gameObject,aliveTimer);
    }

    private void OnDestroy()
    {
        Instantiate(smokeParticle, transform.position, smokeParticle.transform.rotation);
    }
}
