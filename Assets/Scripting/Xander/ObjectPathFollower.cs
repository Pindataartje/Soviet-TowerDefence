using System.Collections;
using UnityEngine;

public class ObjectPathFollower : MonoBehaviour
{
    // Array of objects to move
    public GameObject[] objectsToMove;

    // Array of checkpoints (positions) to follow
    public Transform[] checkpoints;

    // Speed of movement towards checkpoints
    public float moveSpeed = 5f;

    // Speed of rotation towards checkpoints
    public float rotateSpeed = 2f;

    // Time delay between reaching a checkpoint and moving to the next one
    public float delayAtCheckpoint = 1f;

    // Loop the movement (always true, but added for flexibility)
    public bool loopPath = true;

    private int[] currentCheckpointIndex;

    void Start()
    {
        // Initialize the current checkpoint index for each object
        currentCheckpointIndex = new int[objectsToMove.Length];

        // Start moving each object
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            StartCoroutine(MoveObjectToCheckpoint(objectsToMove[i], i));
        }
    }

    IEnumerator MoveObjectToCheckpoint(GameObject obj, int objectIndex)
    {
        while (loopPath)
        {
            // Get the current checkpoint for this object
            Transform targetCheckpoint = checkpoints[currentCheckpointIndex[objectIndex]];

            // Move towards the checkpoint
            while (Vector3.Distance(obj.transform.position, targetCheckpoint.position) > 0.1f)
            {
                // Rotate towards the checkpoint
                RotateTowards(obj, targetCheckpoint.position);

                // Move towards the checkpoint
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetCheckpoint.position, moveSpeed * Time.deltaTime);

                yield return null;
            }

            // Wait at the checkpoint before moving to the next
            yield return new WaitForSeconds(delayAtCheckpoint);

            // Update to the next checkpoint
            currentCheckpointIndex[objectIndex] = (currentCheckpointIndex[objectIndex] + 1) % checkpoints.Length;
        }
    }

    void RotateTowards(GameObject obj, Vector3 targetPosition)
    {
        // Get the direction to the target checkpoint
        Vector3 direction = (targetPosition - obj.transform.position).normalized;

        // Calculate the target rotation to face the checkpoint
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Smoothly rotate towards the target rotation
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }
}