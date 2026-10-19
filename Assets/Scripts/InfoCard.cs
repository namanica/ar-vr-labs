using UnityEngine;

// Інфо-картка: коли з'являється, стає над своїм предметом і повертається лицем до гравця.
public class InfoCard : MonoBehaviour
{
    [SerializeField] private Transform target;   // предмет, про який картка
    [SerializeField] private Transform head;     // CenterEyeAnchor
    [SerializeField] private float gap = 0.1f;   // відступ між верхом предмета і низом картки

    private void OnEnable()
    {
        // Межі всіх мешів предмета, щоб знайти його верх.
        Bounds bounds = new Bounds(target.position, Vector3.zero);
        foreach (Renderer r in target.GetComponentsInChildren<Renderer>())
            bounds.Encapsulate(r.bounds);

        // Половина висоти картки у метрах (висота Canvas * масштаб).
        RectTransform rect = (RectTransform)transform;
        float halfHeight = rect.rect.height * transform.lossyScale.y * 0.5f;

        transform.position = new Vector3(bounds.center.x, bounds.max.y + gap + halfHeight, bounds.center.z);
    }

    private void LateUpdate()
    {
        // Повертаємо картку до гравця тільки навколо вертикальної осі.
        Vector3 fromHead = transform.position - head.position;
        fromHead.y = 0f;
        if (fromHead.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(fromHead, Vector3.up);
    }
}