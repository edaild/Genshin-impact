using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerMoveMent : MonoBehaviour
{
    [Header("플레이어 움직임 컨트럴")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float Jump;
    [SerializeField] private Animator animator;
    private bool isJump;
    private bool isJumpButton;          // 모바일에서 사용
    private bool isMove;
    private Rigidbody Rigidbody;
    [Header("카메라 컨트럴러")]
   [SerializeField] private CameraController cameraController;
    

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraController = cameraController.GetComponent<CameraController>();
    }

    private void Update()
    {
        HandlePCInput();
    }

    private void FixedUpdate()
    {

        // 모바일 조이스틱 입력
        JoystickMove();

        // 키보드 입력
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 DST = new Vector3(hInput, 0, vInput).normalized * speed * Time.deltaTime;
        transform.position += DST;

        // 이동 상태 업데이트 PC 모바일 전부 포함
        isMove = hInput != 0 || vInput != 0 || joystick.Horizontal != 0 || joystick.Vertical != 0;

        // 애니매이션
        animator.SetBool("isRun", isMove);

        // 점프 처리
        if ((Input.GetKeyDown(KeyCode.Space) || isJumpButton) && !isJump)
        {
            Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            animator.SetBool("isJump", true);
            isJump = true;
            isJumpButton = false;
            Debug.Log("뛰는 동작과 뛰는 에니매이션 활성화");
        }

    }

    private void HandlePCInput()
    {
        if (cameraController != null)
        {
            cameraController.PlayerMouse();
        }
        else
        {
            Debug.LogWarning("cameraController 이 활당 되지 않았습니다.");
        }
    }

    // 점프 후 착지 관련 로직
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            animator.SetBool("isJump", false);
            Debug.Log("착지 완료, 점프 해제");
        }
    }

    // 모바일 조이스틱 로직
    public void JoystickMove()
    {
        float hInputM = joystick.Horizontal;
        float vInputM = joystick.Vertical;

        Vector3 DSTM = new Vector3(hInputM, 0, vInputM).normalized * speed * Time.deltaTime;
        transform.position += DSTM;
    }


    public void JumpButton()
    {
        if (!isJumpButton && !isJump)
        {
            isJumpButton = true;
        }
    }
}
