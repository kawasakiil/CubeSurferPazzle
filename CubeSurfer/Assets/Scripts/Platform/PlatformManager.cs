using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject platformPrefab; // ������ ���������
    public float platformLength = 10f; // ����� ����� ���������
    public int initialPlatformCount = 5; // ���������� �������� ��� ��������� ���������

    private Transform playerTransform; // ������ �� ������
    private float spawnZ = 0f; // ������� ������� ��� ������ ��������
    private float playerZ = 0f; // ������� ������ �� ��� Z
    private Transform[] platforms; // ������ ��������

    void Start()
    {
        playerTransform = Camera.main.transform; // ����� �������� �� ������ �� ������ ������
        platforms = new Transform[initialPlatformCount];

        // ��������� ��������� ��������
        for (int i = 0; i < initialPlatformCount; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        playerZ = playerTransform.position.z;

        // ��������, ����� �� �������� ����� ���������
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