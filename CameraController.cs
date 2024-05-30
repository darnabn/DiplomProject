using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float maxYAngle = 80.0f;
    private float rotationX = 0.0f;
    private bool isLocked = false;
    private Transform focusPoint;

    void Update()
    {
        if (isLocked && focusPoint != null)
        {
            Vector3 direction = focusPoint.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * sensitivity);
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.parent.Rotate(Vector3.up * mouseX * sensitivity);

            rotationX -= mouseY * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -maxYAngle, maxYAngle);
            transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
        }
    }

    public void LockCamera(Transform focusPoint)
    {
        isLocked = true;
        this.focusPoint = focusPoint;
    }

    public void UnlockCamera()
    {
        isLocked = false;
        this.focusPoint = null;
    }
}
