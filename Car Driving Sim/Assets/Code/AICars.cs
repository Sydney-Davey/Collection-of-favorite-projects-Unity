using UnityEngine;

public class AICars : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;       

     public float defaultSpeed;  

    void Start()
    {
        targetPoint = 0;
        defaultSpeed = speed;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);

        // move & call rotate to patrol point
        if (transform.position == patrolPoints[targetPoint].position)
        {
            targetPoint++;
            if (targetPoint >= patrolPoints.Length)
                targetPoint = 0;
        }

        RotateTowards();
    }

    void RotateTowards()
    {
        Vector3 targetDirection = patrolPoints[targetPoint].position - transform.position;
        float rotSpeed = 1.0f;
        float step = rotSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, step, 0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
