using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraController : MonoBehaviour
{
    [Header("카메라 회정 설정")]
    [SerializeField] private float rotationSpeed = 5f;
    private Vector2 previousTouchPosition;

    private void Update()
    {
        PlayerMouse();
        PlayerMobile();
    }

    public void PlayerMouse()
    {
        // 마우스 좌 클릭을 눌렸을 경우
        if (Input.GetMouseButton(0))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;

            // 회전 적용
            transform.Rotate(Vector3.up * horizontalRotation, Space.World);
            transform.Rotate(Vector3.right * verticalRotation, Space.Self);
        }
    }

    public void PlayerMobile()
    {
        if (Input.touchCount == 2)  // 두 손 터치
        {
            UnityEngine.Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float horizontalRotation = touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                float verticalRotation = -touch.deltaPosition.y * rotationSpeed * Time.deltaTime;

                // 회전 적용
                transform.Rotate(Vector3.up * horizontalRotation, Space.World);  // Y축 회전
                transform.Rotate(Vector3.right * verticalRotation, Space.Self);  // X축 회전
            }
        }
    }
}
