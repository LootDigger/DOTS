using UnityEngine;

public class ArcSpawner : MonoBehaviour
{
    [SerializeField] private float radius = 5f; // Радиус дуги
    [SerializeField] private float angle = 90f; // Угол дуги в градусах
    [SerializeField] private int density = 10; // Количество кубов на дуге
    [SerializeField] private GameObject cubePrefab; // Префаб куба

    private void Start()
    {
       // SpawnArc();
        SpawnCircle();
    }

    private void SpawnArc()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab is not assigned!");
            return;
        }

        // Угол между соседними кубами
        float angleStep = angle / (density - 1);

        for (int i = 0; i < density; i++)
        {
            // Текущий угол в радианах
            float currentAngle = Mathf.Deg2Rad * (-angle / 2 + i * angleStep);

            // Координаты куба
            float x = Mathf.Cos(currentAngle) * radius;
            float z = Mathf.Sin(currentAngle) * radius;

            // Создаем куб на рассчитанных координатах
            Vector3 position = new Vector3(x, 0, z);
            Instantiate(cubePrefab, position, Quaternion.identity, transform);
        }
    }
    
    private void SpawnCircle()
    {
        if (cubePrefab == null)
        {
            Debug.LogError("Cube Prefab is not assigned!");
            return;
        }

       

        for (int i = 0; i < 360; i++)
        {
            // Текущий угол в радианах
            float currentAngle = Mathf.Deg2Rad * i;

            // Координаты куба
            float x = Mathf.Cos(currentAngle) * radius;
            float z = Mathf.Sin(currentAngle) * radius;

            // Создаем куб на рассчитанных координатах
            Vector3 position = new Vector3(x, 0, z);
            Instantiate(cubePrefab, position, Quaternion.identity, transform);
        }
    }
}
