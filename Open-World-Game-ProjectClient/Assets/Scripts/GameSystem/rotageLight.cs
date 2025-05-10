using UnityEngine;

public class LotageLight : MonoBehaviour
{
    public float rotagespeed;
    private void Update()
    {
        transform.Rotate(rotagespeed * Time.deltaTime,0,0);
    }

}


