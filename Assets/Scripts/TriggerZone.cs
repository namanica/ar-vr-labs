using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private string requiredTag = "Part";
    [SerializeField] private Color emptyColor = new Color(0.31f, 0.86f, 0.47f, 0.24f);
    [SerializeField] private Color filledColor = new Color(1f, 0.75f, 0.2f, 0.45f);

    private Renderer rend;
    private int count;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        GetComponent<Collider>().isTrigger = true;
        UpdateIndicator();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        count++;
        Debug.Log($"[Trigger] {other.name} увійшов у зону «{name}». Деталей у зоні: {count}");
        UpdateIndicator();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(requiredTag)) return;

        count--;
        Debug.Log($"[Trigger] {other.name} покинув зону «{name}». Деталей у зоні: {count}");
        UpdateIndicator();
    }

    void UpdateIndicator()
    {
        if (rend != null)
        {
            rend.material.color = count > 0 ? filledColor : emptyColor;
        }
    }
}