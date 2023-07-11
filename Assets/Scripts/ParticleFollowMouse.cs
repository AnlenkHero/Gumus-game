using UnityEngine;

public class ParticleFollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
