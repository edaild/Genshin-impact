using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraController : MonoBehaviour
{
    [Header("ī�޶� ȸ�� ����")]
    [SerializeField] private float rotationSpeed = 5f;
    private Vector2 previousTouchPosition;

    private void Update()
    {
        PlayerMouse();
        PlayerMobile();
    }

    public void PlayerMouse()
    {
        // ���콺 �� Ŭ���� ������ ���
        if (Input.GetMouseButton(0))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            // ȸ�� ����
            transform.Rotate(Vector3.up * horizontalRotation, Space.World);
            transform.Rotate(Vector3.right * verticalRotation, Space.Self);
        }
    }

    public void PlayerMobile()
    {
        if (Input.touchCount == 2)  // �� �� ��ġ
        {
            UnityEngine.Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float horizontalRotation = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                float verticalRotation = -touch.deltaPosition.y * rotationSpeed * Time.deltaTime;

                // ȸ�� ����
                transform.Rotate(Vector3.up * horizontalRotation, Space.World);  // Y�� ȸ��
                transform.Rotate(Vector3.right * verticalRotation, Space.Self);  // X�� ȸ��
            }
        }
    }
}
