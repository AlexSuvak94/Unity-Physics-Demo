using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(2f, 30f, -30f);
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (!target) return;

        // Follow the car
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }
}
