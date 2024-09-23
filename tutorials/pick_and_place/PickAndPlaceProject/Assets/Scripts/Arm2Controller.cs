using UnityEngine;

public class Arm2Controller : MonoBehaviour
{
    public GameObject Arm2; // Reference to Arm 2
    public GameObject Brick2; // Reference to Brick 2
    public Transform TargetPlacement2; // Target position for Brick 2

    private bool moveArm2 = false;

    void Start()
    {
        // Begin by moving Arm 2 to pick up Brick 2
        moveArm2 = true;
        Debug.Log("Starting Arm 2 movement.");
    }

    void Update()
    {
        if (moveArm2)
        {
            PerformAction();
        }
    }

    void PerformAction()
    {
        Vector3 brickPosition = Brick2.transform.position;
        Quaternion brickRotation = Brick2.transform.rotation;

        // Step 1: Move Arm 2 to Brick 2
        MoveArmToTarget(Arm2, brickPosition, brickRotation);

        if (HasReachedTarget(Arm2.transform, brickPosition))
        {
            Debug.Log("Arm 2 reached Brick 2. Picking it up.");

            // Pick up Brick 2 by parenting it to Arm 2
            Brick2.transform.parent = Arm2.transform;

            // Step 2: Move Arm 2 to the target placement
            MoveArmToTarget(Arm2, TargetPlacement2.position, TargetPlacement2.rotation);

            if (HasReachedTarget(Arm2.transform, TargetPlacement2.position))
            {
                Debug.Log("Arm 2 reached TargetPlacement2. Placing Brick 2.");

                // Place Brick 2 by unparenting it
                Brick2.transform.parent = null;
                Brick2.transform.position = TargetPlacement2.position;
                Brick2.transform.rotation = TargetPlacement2.rotation;

                moveArm2 = false; // Stop further movement
                Debug.Log("Arm 2 has completed its action.");
            }
        }
    }

    void MoveArmToTarget(GameObject arm, Vector3 targetPosition, Quaternion targetRotation)
    {
        // Move arm smoothly to target
        arm.transform.position = Vector3.Lerp(arm.transform.position, targetPosition, Time.deltaTime * 1);
        arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation, targetRotation, Time.deltaTime * 1);
    }

    bool HasReachedTarget(Transform armTransform, Vector3 targetPosition)
    {
        // Check if arm has reached the target position
        return Vector3.Distance(armTransform.position, targetPosition) < 0.01f;
    }
}

