using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    public Transform player;
    public EnemySO enemySO;
    public PlayerController playerController;
    public bool isPlayerAttack;
    public bool isPlayer;

    private void Start()
    {
       playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        EnemyMove();
    }

    //-----------------------------------------------------------------------------------------------------------------------
    // �÷��̾� ���� �ٴϱ�
    void EnemyMove()
    {
        if (isPlayer)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * enemySO.enemy_Speed * Time.deltaTime;

            // ���� �÷��̾ �ٶ󺸰�
            transform.LookAt(player.position);
        }
        if (playerController != null && playerController.isPlayerzon)
        {
            isPlayer = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ����");
           isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }

        if (other.gameObject.CompareTag("Playerzon"))
        {
            isPlayer = false;
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------

}
