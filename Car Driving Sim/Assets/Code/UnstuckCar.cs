using UnityEngine;

public class UnstuckCar : MonoBehaviour
{
    float unstuckInput;
    Vector3 unstuckRotation = new Vector3(0, 0, 0); 

    void Update()
    {
        GetInput();
        TryUnstuck();
    }

    void GetInput()
    {
        unstuckInput = Input.GetAxis("Jump"); 
    }

    void TryUnstuck()
    {
        
        if (unstuckInput > 0f)
        {
            transform.rotation = Quaternion.Euler(unstuckRotation);
            transform.position += Vector3.up * 0.01f;
        }
    }
}