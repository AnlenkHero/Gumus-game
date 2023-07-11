using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem rocketTrailPrefab;
    [SerializeField]
    private GameObject rocketPrefab;
    [SerializeField]
    private ParticleSystem launchParticleSystem;
    private ParticleSystem _tmpLaunchParticleSystem;
    private GameObject _tmpRocket;
    private float _skillDelayTime = 7f;
    private float _skillTime = 1f;
    private GameObject _player;

    private void Start()
    {
        _player = PlayerController.Instance;
    }

    private void Update()
    {
        if (Time.time>_skillTime)
        {
            _skillTime = Time.time + _skillDelayTime;
            LaunchRockets();
        }

        if (_tmpLaunchParticleSystem)
        {
            _tmpLaunchParticleSystem.transform.position = transform.position - Vector3.up/2;
        }
    }
    
    void LaunchRockets()
    {
        _tmpLaunchParticleSystem = Instantiate(launchParticleSystem, transform.position, 
            launchParticleSystem.transform.rotation);
        Instantiate(rocketTrailPrefab, transform.position + Vector3.up, rocketTrailPrefab.transform.rotation);
        _tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up*60, Quaternion.identity);
        _tmpRocket.GetComponent<RocketBehaviour>().Fire(_player.transform);
    }
}