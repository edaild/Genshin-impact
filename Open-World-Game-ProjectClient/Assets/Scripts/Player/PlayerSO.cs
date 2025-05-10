using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "GameObject/Player" )]
public class PlayerSO : ScriptableObject
{
    // �ʱ� �÷��̾� ����
    //-----------------------------------------------
    public int player_Uid;
    public string player_Id;
    public string player_Password;
    public string player_Name;
    public float player_Speed = 5f;
    public float playre_Jump = 5f;
    public int player_Lever = 1;
    public float player_Attack = 10f;
    public float player_CurrHp = 100f;
    public float player_MaxHp = 100f;
    public float player_MinHp = 10f;
    public Rigidbody player_Rigidbody;
    public Animator player_Animator;
    //-----------------------------------------------
}
