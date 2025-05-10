using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;


public class CameraController : MonoBehaviour
{
    [Header("카메라 회명 설정")]
    public float rotationSpeed = 5f;
    public Vector3 offset = new Vector3(0, 5, -7); // 카메라 위치 오프셋
    public float smoothSpeed = 5f;
    public Transform targetPlayer;
    public float zoomSpeed = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    private Vector2 previousTouchPosition;

    public float currentDistance;

    private void Start()
    {
        currentDistance = Vector3.Distance(transform.position, targetPlayer.position);
    }

    private void Update()
    {
        PlayerMouse();
        PlayerMobile();
        P_LateUpdate();
        M_LateUpdate();
    }

    // ------------------------------------------------------------------------------------------------------------
    // 카메라 회전 관련 로직 함수
    public void PlayerMouse()
    {
        // 마우스 좌 클릭을 눌렸을 경우
        if (Input.GetMouseButton(1))
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
    // ------------------------------------------------------------------------------------------------------------
    // 플레이어 거리 조절
    private void P_LateUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // 카메라 위치 조절
        Vector3 dir = (transform.position - targetPlayer.position).normalized;
        transform.position = targetPlayer.position + dir * currentDistance;

        // 카메라가 항상 플레이어를 바라보게
        Vector3 lookDirection = (targetPlayer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
    }

    private void M_LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            UnityEngine.Touch touchZero = Input.GetTouch(0);
            UnityEngine.Touch touchOne = Input.GetTouch(1);

            // 이전 프레임의 터치 위치
            Vector2 prevTouchZero = touchZero.position - touchZero.deltaPosition;
            Vector2 prevTouchOne = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (prevTouchZero - prevTouchOne).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            currentDistance -= difference * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            // 카메라가 항상 플레이어를 바라보게
            Vector3 lookDirection = (targetPlayer.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, smoothSpeed * Time.deltaTime);
        }
    }
    // ------------------------------------------------------------------------------------------------------------
}
