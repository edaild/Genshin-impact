using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public PlayerSO playerSO;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
