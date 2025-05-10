using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.UI;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 움직임 컨트럴")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerSO playerSO;
    private Vector3 moveInput; // 조이스틱 방향 저장용
    private bool isJump;
    private bool isJumpButton;          // 모바일에서 사용
    private bool isMove;

    public bool isPlayerzon;

    private Vector3 forward;
    private Vector3 right;
    [Header("카메라 컨트럴러")]
   [SerializeField] private CameraController cameraController;
    private CharacterController controller_P;
    private CharacterController controller_M;


    private void Start()
    {
        playerSO.player_Rigidbody = GetComponent<Rigidbody>();
        playerSO.player_Animator = GetComponent<Animator>();
        cameraController = cameraController.GetComponent<CameraController>();
    }

    private void Update()
    {
        Jump();
        HandleMoblieInput();
        HandlePCInput();
        Attack();
        MInHP();
        Die();

        // 카메라 기준 방향 계산
        forward = cameraTransform.forward;
        right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();


    }

    private void FixedUpdate()
    {
        // 모바일 조이스틱 입력
        JoystickMove();
        // 키보드 입력
        keybordMove();

    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 움직임 로직

    // PC 키보드
    public void keybordMove()
    {
        float P_HorizontalInput = Input.GetAxis("Horizontal");
        float P_VerticalInput = Input.GetAxis("Vertical");

        Vector3 P_DST = forward * P_VerticalInput + right * P_HorizontalInput;
        MovePlayer(P_DST);

        // 이동 상태 업데이트 PC 모바일 전부 포함
        isMove = P_HorizontalInput != 0 || P_VerticalInput != 0 || joystick.Horizontal != 0 || joystick.Vertical != 0;

        // 애니매이션
        playerSO.player_Animator.SetBool("isRun", isMove);

   

        if (P_DST.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(P_DST);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * cameraController.rotationSpeed);
        }
    }

    // 모바일 조이스틱
    public void JoystickMove()
    {
        float M_HorizontalInput = joystick.Horizontal;
        float M_VerticalInput = joystick.Vertical;

        Vector3 M_DST = forward * M_VerticalInput + right * M_HorizontalInput;
        MovePlayer(M_DST);

        if (M_DST.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(M_DST);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * cameraController.rotationSpeed);
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 점프 관련 로직
    public void Jump()
    {
        // 점프 처리
        if ((Input.GetKeyDown(KeyCode.Space) || isJumpButton) && !isJump)
        {
            playerSO.player_Rigidbody.AddForce(Vector3.up * playerSO.player_Speed, ForceMode.Impulse);
            playerSO.player_Animator.SetBool("isJump", true);
            isJump = true;
            isJumpButton = false;
            Debug.Log("뛰는 동작과 뛰는 에니매이션 활성화");
        }

    }

    // 착지 처리
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            playerSO.player_Animator.SetBool("isJump", false);
            Debug.Log("착지 완료, 점프 해제");
        }
    }

    public void JumpButton()
    {
        if (!isJumpButton && !isJump)
        {
            isJumpButton = true;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 카메라 회전 관련 로직
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

    private void HandleMoblieInput()
    {
        if (cameraController != null)
        {
            cameraController.PlayerMobile();
        }
        else
        {
            Debug.LogWarning("cameraController 이 활당 되지 않았습니다.");
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 공격 로직
    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("일반 공격");
            playerSO.player_Animator.SetBool("IsNoallAttack", true);

        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q 스킬 공격");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W 스킬 공격");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 스킬 공격");
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 최소 HP 도달 처리
    private void MInHP()
    {
        if (playerSO != null && playerSO.player_CurrHp <= playerSO.player_MinHp)
        {
            Debug.Log("조심하세요 한번 더 공격을 받으면 사망합니다.");
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 사망 처리
    private void Die()
    {
        if (playerSO != null && playerSO.player_CurrHp <= 0)
        {
            Debug.Log("사망 처리");
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 이동 처리
    private void MovePlayer(Vector3 moveDirection)
    {
        if (playerSO != null && playerSO.player_Rigidbody != null)
        {
            // 기존의 이동을 Rigidbody를 이용하여 처리
            Vector3 velocity = moveDirection * playerSO.player_Speed;
            playerSO.player_Rigidbody.linearVelocity = new Vector3(velocity.x, playerSO.player_Rigidbody.linearVelocity.y, velocity.z);


            if (moveInput.magnitude > 0.1f)
            {
                Vector3 move = moveInput * playerSO.player_Speed * Time.fixedDeltaTime;
                playerSO.player_Rigidbody.MovePosition(playerSO.player_Rigidbody.position + move);
            }

            // 이동 테스트 (물리 무시)
            transform.position += moveDirection * playerSO.player_Speed * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("playerSO 또는 player_Rigidbody가 null입니다. 확인이 필요합니다.");
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------
    // 플레이어 안전 구역
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Playerzon"))
        {
            isPlayerzon = true;
        }
        else
        {
            isPlayerzon = false;
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------
}
