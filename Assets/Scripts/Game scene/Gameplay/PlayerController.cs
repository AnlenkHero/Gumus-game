using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static GameObject Instance; 
    [SerializeField]
    private float moveForce = 5.0f;
    [SerializeField]
    private GameObject focalPoint;
    [SerializeField]
    private float powerUpStrength=5f;
    [SerializeField]
    private GameObject powerUpIndicator;
    private GameObject _tmpPowerUpIndicator;
    [SerializeField]
    private ParticleSystem rocketTrailPrefab;
    [SerializeField]
    private GameObject smashParticleSystemPrefab;
    [SerializeField]
    private ParticleSystem collideImpactParticle;
    [SerializeField]
    private float hangTime;
    [SerializeField]
    private float smashSpeed;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private GameObject gumusAura;
    private GameObject _tmpGumusAura;
    [SerializeField]
    private AudioSource effectAudioSource;
    [SerializeField]
    private AudioSource powerUpAudioSource;
    [SerializeField]
    private AudioClip plasmaSfx;
    [SerializeField]
    private AudioClip gumusSlowDownSpeedUpSfx;
    [SerializeField]
    private AudioClip powerUpRelease;
    [SerializeField]
    private PostProcessingController postProcessingController;
    [SerializeField]
    private SetPowerUpText powerUpText;
    [SerializeField]
    private GameObject powerUpTextPrefab;
    private KeyCode _skillButton = KeyCode.None;
    private string _skillName;
    private GameObject _tmpPowerUpTextPrefab;
    private Rigidbody _rigidbody;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    private GameObject _tmpRocket;
    private GameObject _tmpSmash;
    private Coroutine _powerUpCountdown;
    private bool _smashing;
    private float _floorY;
    private Renderer _renderer;
    private List<Material> _materials;
    private Material _pushBackMaterial;
    private bool _slowDownSpeedUp;
    private bool _gameOver;
    private bool _gameOverInvoked;
    public static event Action OnGameOver;

    private void OnEnable()
    {
        OnGameOver += DeactivatePowerUpOnLose;
    }

    private void OnDisable()
    {
        OnGameOver -= DeactivatePowerUpOnLose;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = true;
        _materials = _renderer.materials.ToList();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var verticalInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * (verticalInput * moveForce * Time.deltaTime));
    }

    private void Update()
    {
        if (_gameOver) return;
        
        if(_tmpPowerUpIndicator!=null)
            _tmpPowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
        if(_tmpGumusAura != null)
            _tmpGumusAura.transform.position = transform.position;

        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(_skillButton))
        {
            LaunchRockets();
            DeactivatePowerUp();
        }

        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(_skillButton) && !_smashing)
        {
            _smashing = true;
            StartCoroutine(Smash());
            DeactivatePowerUp();
        }

        if (currentPowerUp == PowerUpType.SlowDownSpeedUp && Input.GetKeyDown(_skillButton) && !_slowDownSpeedUp)
        {
            effectAudioSource.clip = gumusSlowDownSpeedUpSfx;
            effectAudioSource.Play();
            Destroy(_tmpPowerUpTextPrefab);
            SlowDownEnemiesSpeedUpPlayer(2f, true);
        }

        if (_tmpSmash != null)
        {
            _tmpSmash.transform.position = transform.position;
        }

        if (transform.position.y < -10)
        {
            _gameOver = true;
            if (!_gameOverInvoked)
            {
                OnGameOver?.Invoke();
                _gameOverInvoked = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUp powerUp = other.gameObject.GetComponent<PowerUp>();
        
        if (powerUp)
        {
            SetPostProcessingValues();
            DeactivatePowerUp();
            currentPowerUp = powerUp.powerUpType;
            Destroy(other.gameObject);
            _tmpPowerUpIndicator = Instantiate(powerUpIndicator, transform.position, Quaternion.identity);
            _tmpPowerUpIndicator.GetComponent<MeshFilter>().mesh = powerUp.GetPowerUpIndicatorMesh;
            powerUpAudioSource.clip = powerUp.GetPickUpSound;
            powerUpAudioSource.Play();
            _skillButton = powerUp.GetKeyCode;
            _skillName = powerUp.GetPowerUpName;
            
            if (_skillButton != KeyCode.None)
            {
                powerUpText.SetText(_skillButton,_skillName);
                _tmpPowerUpTextPrefab = Instantiate(powerUpTextPrefab, powerUpTextPrefab.transform.position, Quaternion.identity);
            }
            
            if (powerUp.GetPowerUpMaterial)
            {
                _pushBackMaterial = powerUp.GetPowerUpMaterial;
                _materials.Add(_pushBackMaterial);
                gameObject.GetComponent<Renderer>().materials = _materials.ToArray();
            }
            
            if (currentPowerUp == PowerUpType.PushBack)
            {
                effectAudioSource.clip = plasmaSfx;
                effectAudioSource.Play();
            }
            
            if (_powerUpCountdown != null)
            {
                StopCoroutine(_powerUpCountdown);
            }
            
            _powerUpCountdown = StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        
        if (enemy)
        {
            Instantiate(collideImpactParticle, transform.position, transform.rotation);
            
            if (currentPowerUp == PowerUpType.PushBack)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = enemy.transform.position - transform.position;
                enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
            
        }
    }

    private void LaunchRockets()
    {
        Instantiate(rocketTrailPrefab, transform.position + Vector3.up * 3, rocketTrailPrefab.transform.rotation);
        
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            _tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up * 60, Quaternion.identity);
            _tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
        
    }

    private IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        DeactivatePowerUp();
        postProcessingController.DeactivatePostProcessingEffect();
    }

    private void DeactivatePowerUp()
    {
        powerUpAudioSource.clip = powerUpRelease;
        powerUpAudioSource.Play();
        effectAudioSource.Stop();
        effectAudioSource.clip = null;
        Destroy(_tmpPowerUpTextPrefab);
        
        if (currentPowerUp == PowerUpType.SlowDownSpeedUp && _slowDownSpeedUp)
        {
            SlowDownEnemiesSpeedUpPlayer(0.5f,false);
        }
        
        currentPowerUp = PowerUpType.None;
        Destroy(_tmpPowerUpIndicator);
        _materials.Remove(_pushBackMaterial);
        gameObject.GetComponent<Renderer>().materials = _materials.ToArray();
    }

    private void SlowDownEnemiesSpeedUpPlayer(float multiplier,bool state)
    {
        _slowDownSpeedUp = state;
        if(state)
            _tmpGumusAura = Instantiate(gumusAura, transform.position, Quaternion.identity);
        else
            Destroy(_tmpGumusAura);    
        
        moveForce *= multiplier;
        
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.speed /= multiplier;
        }
        
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        _floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        
        while(Time.time < jumpTime)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, smashSpeed*4);
            yield return null;
        }
        
        while(transform.position.y > _floorY)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -smashSpeed * 8);
            yield return null;
        }
        
        _tmpSmash = Instantiate(smashParticleSystemPrefab, transform.position, Quaternion.identity);
        
        foreach (var enemy in enemies)
        {
            if(enemy != null)
                enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                    transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        
        _smashing = false;
    }

    private void SetPostProcessingValues()
    {
        postProcessingController.RandomizeEffects();
    }

    private void DeactivatePowerUpOnLose()
    {
        DeactivatePowerUp();
        if (_powerUpCountdown != null)
        {
            StopCoroutine(_powerUpCountdown);
        }
    }
}
