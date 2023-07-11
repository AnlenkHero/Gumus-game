using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * horizontalInput * speed * Time.deltaTime);
    }
}
