using UnityEngine;

public class TouchAndGo : MonoBehaviour
{
    [Header("Player statistics:")]
    public float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody rb;
    public Animator animator;
    public Camera mainCamera;

    private Touch touch;
    private Vector3 touchPosition;
    private bool isMoving = false;

    private void Update()
    {
        Move();
        Animate();
        FollowPlayer();
    }

    private void Move()
    {
        if (isMoving)
        {
            Vector3 currentPosition = transform.position;
            currentPosition.y = touchPosition.y;
            transform.position = Vector3.MoveTowards(currentPosition, touchPosition, movementSpeed * Time.deltaTime);

            // поворот гравц€ в напр€мку дотику
            Vector3 direction = touchPosition - transform.position;
            direction.y = 0f;
            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            if (Vector3.Distance(currentPosition, touchPosition) < 0.01f)
            {
                isMoving = false;
                rb.velocity = Vector3.zero;
            }
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isMoving = true;
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.y = transform.position.y;
            }
        }
    }

    private void Animate()
    {
        if (isMoving)
        {
            animator.SetFloat("Speed", 1.0f);
            animator.SetFloat("Horizontal", touchPosition.x - transform.position.x);
            animator.SetFloat("Vertical", touchPosition.z - transform.position.z);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
    }

    private void FollowPlayer()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x = transform.position.x;
        cameraPosition.z = transform.position.z; // зм≥щенн€ камери по ос≥ Z
        mainCamera.transform.position = cameraPosition;
    }
}
