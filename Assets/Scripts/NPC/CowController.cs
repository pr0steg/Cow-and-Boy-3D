using UnityEngine;

public class CowController : MonoBehaviour
{
    // Параметри кола з радіусом 46 одиниць
    private const float circleRadius = 46f;
    private const float circleRadiusSquared = circleRadius * circleRadius;
    private readonly Vector3 center = Vector3.zero;

    // Параметри руху тварини
    public float movementSpeed = 1f;
    public float rotationSpeed = 2f;

    // Параметри для випадкового вибору точок
    private Vector3 targetPoint;

    // Компонент аніматора
    private Animator animator;

    private void Start()
    {
        // Отримуємо компонент аніматора
        animator = GetComponent<Animator>();

        // Випадково вибираємо початкову точку
        ChooseNewTargetPoint();
    }

    private void Update()
    {
        // Обчислюємо швидкість руху тварини
        float speed = Mathf.Abs(movementSpeed * Time.deltaTime / (targetPoint - transform.position).magnitude);

        // Встановлюємо параметр швидкості аніматора
        animator.SetFloat("Speed", speed);

        // Встановлюємо стани аніматора
        if (speed > 0.00001f)
        {
            animator.Play("Walk", -1);
        }
        else
        {
            animator.Play("Idle", -1);
        }

        // Обчислюємо відстань між твариною і цільовою точкою
        Vector3 distanceToTarget = targetPoint - transform.position;
        float distanceToTargetSquared = distanceToTarget.sqrMagnitude;

        // Якщо тварина дісталась до цільової точки, вибираємо нову
        if (distanceToTargetSquared < 0.1f)
        {
            ChooseNewTargetPoint();
        }
        else
        {
            // Рухаємо тварину до цільової точки
            Vector3 movementDirection = distanceToTarget.normalized;
            transform.position += movementDirection * movementSpeed * Time.deltaTime;

            // Повертаємо тварину в напрямку руху
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            targetRotation *= Quaternion.Euler(0f, 180f, 0f); // Повертаємо тварину на 180 градусів
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void ChooseNewTargetPoint()
    {
        // Випадково вибираємо точку на колі з радіусом 30 одиниць
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomRadius = Mathf.Sqrt(Random.Range(0f, circleRadiusSquared));
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle));
        targetPoint = center + randomDirection * randomRadius;

        // Обчислюємо відстань між твариною і цільовою точкою
        Vector3 distanceToTarget = targetPoint - transform.position;
        float distanceToTargetSquared = distanceToTarget.sqrMagnitude;

        // Встановлюємо відповідні параметри для аніматора, в залежності від швидкості руху
        Animator animator = GetComponent<Animator>();
        float speed = distanceToTargetSquared > 0.1f ? movementSpeed : 0f;
        animator.SetFloat("Speed", speed);
        // Встановлюємо стани аніматора
        if (speed > 0.00001f)
        {
            animator.Play("Walk", -1);
        }
        else
        {
            animator.Play("Idle", -1);
        }
    }
}
