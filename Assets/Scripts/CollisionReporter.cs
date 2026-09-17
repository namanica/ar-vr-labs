using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionReporter : MonoBehaviour
{
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float minImpulse = 0.3f;
    [SerializeField] private float flashDuration = 0.4f;

    private Renderer[] renderers;
    private Color[] baseColors;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        baseColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            baseColors[i] = renderers[i].material.color;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        float impulse = collision.impulse.magnitude;
        if (impulse < minImpulse) return;

        Debug.Log($"[Collision] {name} ← {collision.gameObject.name}, імпульс {impulse:F2}");

        foreach (var r in renderers)
        {
            r.material.color = hitColor;
        }

        CancelInvoke(nameof(ResetColors));
        Invoke(nameof(ResetColors), flashDuration);
    }

    void ResetColors()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = baseColors[i];
        }
    }
}