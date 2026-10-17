using UnityEngine;

public class CarControl : MonoBehaviour
{
    public WheelCollider FrontRightWheelCollider;
    public WheelCollider FrontLeftWheelCollider;
    public WheelCollider BackRightWheelCollider;
    public WheelCollider BackLeftWheelCollider;

    public Transform FrontRightWheelTranssform;
    public Transform FrontLeftWheelTranssform;
    public Transform BackRightWheelTranssform;
    public Transform BackLeftWheelTranssform;

    public float motorForce = 100f;
    public float steeringAngle = 30f;
    float verticalInput;
    float horizontalInput;
    //[Range(0f, 1f)] public float LT;  // Left Trigger
    //[Range(0f, 1f)] public float RT;  // Right Trigger
    //float TriggerhorizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MotorForse();
        GetInput();
        Steering();
    }
    void GetInput()
    {
        verticalInput=Input.GetAxis("Vertical");  
        horizontalInput=Input.GetAxis("Horizontal");
        //TriggerhorizontalInput = Input.GetAxis("TriggerHorizontal");

    }
    void MotorForse()
    {
        FrontRightWheelCollider.motorTorque = motorForce * verticalInput;
        FrontLeftWheelCollider.motorTorque = motorForce * verticalInput;
    }
    void Steering()
    {
        FrontRightWheelCollider.steerAngle = steeringAngle * horizontalInput;
        FrontLeftWheelCollider.steerAngle = steeringAngle * horizontalInput;
    }
}