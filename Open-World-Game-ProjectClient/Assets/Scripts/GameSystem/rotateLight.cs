using UnityEngine;

public class Rotageobject : MonoBehaviour
{
    public float rotagespeed;
    private void Update()
    {
        transform.Rotate(rotagespeed * Time.deltaTime,0,0);
    }

}


