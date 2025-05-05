using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerMoveMent : MonoBehaviour
{
    [Header("플레이어 움직임 컨트럴")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float Jump;
    [SerializeField] private Animation Animate;
    private bool isJump;
    private bool isJumpButton;

    public Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animate = GetComponent<Animation>();
    }

    private void FixedUpdate()
    {
        // 모바일 조이스틱
        JoystickMove();

        // 키보드
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 DST = new Vector3(hInput, 0, vInput).normalized * speed * Time.deltaTime;
        transform.position += DST;

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) || isJumpButton )
        {
            Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }
    }
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
        else
        {
            isJumpButton = false;
        }
    }
}
