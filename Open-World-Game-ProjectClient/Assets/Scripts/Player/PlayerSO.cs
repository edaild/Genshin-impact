using UnityEngine;
//----------------------------------------------------------------------------------
// 초기 플레이어 관련
[CreateAssetMenu(fileName = "Player", menuName = "GameObject/Player" )]
public class PlayerSO : ScriptableObject
{
    public int player_Uid;
    public string player_Id;
    public string player_Password;
    public string player_Name;
    public float player_Speed = 5f;
    public float playre_Jump = 5f;
    public int player_Lever = 1;
    public float player_CurrStamina = 100f;
    public float player_MaxStamina = 100f;
    public float player_CurrHp = 100f;
    public float player_MaxHp = 100f;
    public float player_MinHp = 10f;
    public Rigidbody player_Rigidbody;
    public Animator player_Animator;

}
//----------------------------------------------------------------------------------
// Item Type 관련
public enum ItemType
{
    Flashlight,         // 손전등
    MedicalKit,         // 메디컬 키트
    ChocolateBar,       // 초코바
    SleepingBag,        // 침냥
    Tent,               // 텐트
    MRE,                // 전투식량
}


// Item 관련
[CreateAssetMenu(fileName = "Item", menuName = "GameObject/Item")]
public class ItemSO : ScriptableObject
{
    public int item_Id;
    public string item_Name;
    public int item_Lever = 1;
    public ItemType ItemType;
}

//----------------------------------------------------------------------------------




