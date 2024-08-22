using UnityEngine;

public class Missile : MonoBehaviour, IMissile
{
    private Vector3 target;
    private float speed;
    private bool isLaunched = false;
    private Quaternion targetRotation;

    [SerializeField] private float targetReachedThreshold = 0.1f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject explosionColliderPrefab;

    public void SetTarget(Vector3 targetPosition, float missileSpeed)
    {
        target = targetPosition;
        speed = missileSpeed;
        isLaunched = true;
        targetRotation = Quaternion.LookRotation(target);
        Debug.Log("Missile Launched: Target Position = " + target + ", Speed = " + speed);
    }

    private void Update()
    {
        if (isLaunched)
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            Debug.Log("Missile Position: " + transform.position + ", Moving towards: " + target);

            if (Vector3.Distance(transform.position, target) < targetReachedThreshold)
            {
                Debug.Log("Missile reached the target. Destroying missile.");
                Explode();
                Destroy(gameObject);
            }
        }
    }


    private void Explode()
    {
        Debug.Log("Missile exploded at position: " + transform.position);

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (explosionColliderPrefab != null)
        {
            Instantiate(explosionColliderPrefab, transform.position, Quaternion.identity);
        }
    }
}
