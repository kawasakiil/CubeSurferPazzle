using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;
    public float sidewaysSpeed = 5f;
    public float leftBoundary = -5f;
    public float rightBoundary = 5f;

    [Header("Stack Settings")]
    public float cubeHeight = 1f; // Высота куба
    public int maxStackHeight = 10; // Максимальная высота башни
    public float platformHeight = 0.5f; // Высота платформы

    private int currentStackHeight = 0; // Текущая высота башни
    private Transform playerTransform;

    void Start()
    {
        playerTransform = transform;
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = Vector3.forward * forwardSpeed * Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        movement += Vector3.right * horizontalInput * sidewaysSpeed * Time.deltaTime;

        Vector3 newPosition = playerTransform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, leftBoundary, rightBoundary);

        playerTransform.position = newPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            if (currentStackHeight < maxStackHeight)
            {
                AddCubeToStack(other.transform);
            }
        }
    }

    void AddCubeToStack(Transform cube)
    {
        currentStackHeight++;
        cube.SetParent(playerTransform);
        float newYPosition = -(currentStackHeight * cubeHeight) + cubeHeight / 2;
        cube.localPosition = new Vector3(0, newYPosition, 0);
        cube.localRotation = Quaternion.identity;
        cube.GetComponent<Collider>().enabled = false; // Отключаем коллайдер куба

        // Поднимаем игрока на высоту одного куба
        Vector3 playerPosition = playerTransform.position;
        playerPosition.y = platformHeight + (currentStackHeight * cubeHeight);
        playerTransform.position = playerPosition;
    }

    void FixedUpdate()
    {
        if (currentStackHeight > 0)
        {
            Vector3 playerPosition = playerTransform.position;
            playerPosition.y = platformHeight + (currentStackHeight * cubeHeight);
            playerTransform.position = playerPosition;
        }
    }
}