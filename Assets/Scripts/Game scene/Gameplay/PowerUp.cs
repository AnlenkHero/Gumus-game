using UnityEngine;
public enum PowerUpType
{
    None, 
    PushBack, 
    Rockets,
    Smash,
    SlowDownSpeedUp
}
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private string powerUpName;
    [SerializeField]
    private Mesh meshToUse;
    [SerializeField]
    private Material materialToUse;
    [SerializeField]
    private AudioClip pickUpSound;
    [SerializeField]
    private KeyCode keyCode;
    public PowerUpType powerUpType;

    public Material GetPowerUpMaterial => materialToUse;
    public Mesh GetPowerUpIndicatorMesh => meshToUse;
    public AudioClip GetPickUpSound => pickUpSound;
    
    public KeyCode GetKeyCode => keyCode;
    
    public string GetPowerUpName => powerUpName;
}
