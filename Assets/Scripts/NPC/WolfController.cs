using UnityEngine;
using UnityEngine.SceneManagement;

public class WolfController : MonoBehaviour
{
    private const float circleRadius = 46f;
    private const float circleRadiusSquared = circleRadius * circleRadius;
    private readonly Vector3 center = Vector3.zero;

    public float movementSpeed = 10f;
    public float rotationSpeed = 5f;

    private Vector3 targetPoint;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ChooseNewTargetPoint();
    }

    private void Update()
    {
        float speed = Mathf.Abs(movementSpeed * Time.deltaTime / (targetPoint - transform.position).magnitude);
        animator.SetFloat("Speed", speed);
        if (speed > 0.00001f)
        {
            animator.Play("Walk", -1);
        }
        else
        {
            animator.Play("Idle", -1);
        }
        Vector3 distanceToTarget = targetPoint - transform.position;
        float distanceToTargetSquared = distanceToTarget.sqrMagnitude;

        if (distanceToTargetSquared < 0.1f)
        {
            ChooseNewTargetPoint();
        }
        else
        {
            Vector3 movementDirection = distanceToTarget.normalized;
            transform.position += movementDirection * movementSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            // targetRotation *= Quaternion.Euler(0f, 0f, 0f); // Повертаємо тварину на 180 градусів
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ChooseNewTargetPoint()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomRadius = Mathf.Sqrt(Random.Range(0f, circleRadiusSquared));
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle));
        targetPoint = center + randomDirection * randomRadius;

        Vector3 distanceToTarget = targetPoint - transform.position;
        float distanceToTargetSquared = distanceToTarget.sqrMagnitude;

        Animator animator = GetComponent<Animator>();
        float speed = distanceToTargetSquared > 0.1f ? movementSpeed : 0f;
        animator.SetFloat("Speed", speed);
        if (speed > 0.00001f)
        {
            animator.Play("Walk", -1);
        }
        else
        {
            animator.Play("Idle", -1);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Scene0");
        }
    }
}
