using UnityEngine;

public class ArmController : MonoBehaviour
{
    public GameObject Arm1; // Reference to Arm 1
    public GameObject Arm2; // Reference to Arm 2
    public GameObject Brick1; // Brick for Arm 1
    public GameObject Brick2; // Brick for Arm 2

    private Vector3 targetPosition; // Target position for placing the bricks
    private Quaternion targetRotation;

    private bool moveArm2 = false;
    private bool mimicArm1 = false;

    // This function will be called to start the sequence of actions
    public void SetTargetPosition(Vector3 position, Quaternion rotation)
    {
        targetPosition = position;
        targetRotation = rotation;

        // Start moving Arm 2 first
        moveArm2 = true;
        Debug.Log("Arm 2 will move first to pick up and place Brick 2.");
    }

    private void Update()
    {
        if (moveArm2)
        {
            // Move Arm 2 to Brick 2's position to pick it up
            MoveArmToTarget(Arm2, Brick2.transform.position, Brick2.transform.rotation);

            // Check if Arm 2 has reached Brick 2's position
            if (HasReachedTarget(Arm2.transform, Brick2.transform.position))
            {
                // Simulate picking up Brick 2 by parenting it to Arm 2
                Brick2.transform.parent = Arm2.transform;

                // Move Arm 2 to the target position to place Brick 2
                MoveArmToTarget(Arm2, targetPosition, targetRotation);

                // Check if Arm 2 has reached the target position
                if (HasReachedTarget(Arm2.transform, targetPosition))
                {
                    // Simulate placing Brick 2 by unparenting it
                    Brick2.transform.parent = null;

                    moveArm2 = false; // Arm 2 finished
                    mimicArm1 = true; // Now start mimicking with Arm 1
                    Debug.Log("Arm 2 has placed Brick 2. Now Arm 1 will mimic with Brick 1.");
                }
            }
        }

        if (mimicArm1)
        {
            // Move Arm 1 to Brick 1's position to pick it up
            MoveArmToTarget(Arm1, Brick1.transform.position, Brick1.transform.rotation);

            // Check if Arm 1 has reached Brick 1's position
            if (HasReachedTarget(Arm1.transform, Brick1.transform.position))
            {
                // Simulate picking up Brick 1 by parenting it to Arm 1
                Brick1.transform.parent = Arm1.transform;

                // Move Arm 1 to the target position to place Brick 1
                MoveArmToTarget(Arm1, targetPosition, targetRotation);

                // Check if Arm 1 has reached the target position
                if (HasReachedTarget(Arm1.transform, targetPosition))
                {
                    // Simulate placing Brick 1 by unparenting it
                    Brick1.transform.parent = null;

                    mimicArm1 = false;
                    Debug.Log("Arm 1 has mimicked Arm 2's action with Brick 1 successfully.");
                }
            }
        }
    }

    // Helper function to move an arm to a target position and rotation
    private void MoveArmToTarget(GameObject arm, Vector3 position, Quaternion rotation)
    {
        arm.transform.position = Vector3.Lerp(arm.transform.position, position, Time.deltaTime * 2);
        arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation, rotation, Time.deltaTime * 2);
    }

    // Helper function to check if an arm has reached the target position
    private bool HasReachedTarget(Transform armTransform, Vector3 position)
    {
        return Vector3.Distance(armTransform.position, position) < 0.01f;
    }
}
