using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab; // Префаб платформы
    public float platformLength = 10f; // Длина одной платформы
    public int initialPlatformCount = 5; // Количество платформ для начальной генерации

    private Transform playerTransform; // Ссылка на игрока
    private float spawnZ = 0f; // Текущая позиция для спауна платформ
    private float playerZ = 0f; // Позиция игрока по оси Z
    private Transform[] platforms; // Массив платформ

    void Start()
    {
        playerTransform = Camera.main.transform; // Можно заменить на ссылку на объект игрока
        platforms = new Transform[initialPlatformCount];

        // Начальная генерация платформ
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        playerZ = playerTransform.position.z;

        // Проверка, нужно ли спаунить новые платформы
        if (playerZ > (spawnZ - (initialPlatformCount * platformLength)))
        {
            SpawnPlatform();
            RemovePlatform();
        }
    }

    void SpawnPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        newPlatform.transform.SetParent(transform);
        platforms[(int)(spawnZ / platformLength) % initialPlatformCount] = newPlatform.transform;
        spawnZ += platformLength;
    }

    void RemovePlatform()
    {
        int removeIndex = (int)((spawnZ - (initialPlatformCount * platformLength)) / platformLength) % initialPlatformCount;
        Destroy(platforms[removeIndex].gameObject);
    }
}