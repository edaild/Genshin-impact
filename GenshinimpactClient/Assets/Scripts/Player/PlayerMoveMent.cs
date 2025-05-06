using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class PlayerMoveMent : MonoBehaviour
{
    [Header("�÷��̾� ������ ��Ʈ��")]
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float Jump;
    [SerializeField] private Animator animator;
    private bool isJump;
    private bool isJumpButton;          // ����Ͽ��� ���
    private bool isMove;
    private Rigidbody Rigidbody;
    [Header("ī�޶� ��Ʈ����")]
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

        // ����� ���̽�ƽ �Է�
        JoystickMove();

        // Ű���� �Է�
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 DST = new Vector3(hInput, 0, vInput).normalized * speed * Time.deltaTime;
        transform.position += DST;

        // �̵� ���� ������Ʈ PC ����� ���� ����
        isMove = hInput != 0 || vInput != 0 || joystick.Horizontal != 0 || joystick.Vertical != 0;

        // �ִϸ��̼�
        animator.SetBool("isRun", isMove);

        // ���� ó��
        if ((Input.GetKeyDown(KeyCode.Space) || isJumpButton) && !isJump)
        {
            Rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
            animator.SetBool("isJump", true);
            isJump = true;
            isJumpButton = false;
            Debug.Log("�ٴ� ���۰� �ٴ� ���ϸ��̼� Ȱ��ȭ");
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
            Debug.LogWarning("cameraController �� Ȱ�� ���� �ʾҽ��ϴ�.");
        }
    }

    // ���� �� ���� ���� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            animator.SetBool("isJump", false);
            Debug.Log("���� �Ϸ�, ���� ����");
        }
    }

    // ����� ���̽�ƽ ����
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
