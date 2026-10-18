using UnityEngine;
using UnityEngine.InputSystem;

// Показує/ховає планшет-меню перед гравцем (кнопка Menu на контролері або клавіша M).
public class TabletToggle : MonoBehaviour
{
    [SerializeField] private GameObject tablet;        // Canvas "Tablet"
    [SerializeField] private Transform head;           // CenterEyeAnchor
    [SerializeField] private float distance = 0.6f;    // наскільки далеко перед очима
    [SerializeField] private float heightOffset = -0.15f; // трохи нижче рівня очей
    [SerializeField] private float fallbackEyeHeight = 1.6f; // висота очей, якщо шолома немає

    private void Start()
    {
        tablet.SetActive(false);
    }

    private void Update()
    {
        bool menuButton = OVRInput.GetDown(OVRInput.Button.Start);
        bool keyboardM = Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame;

        if (menuButton || keyboardM)
            Toggle();
    }

    public void Toggle()
    {
        bool show = !tablet.activeSelf;
        if (show)
            PlaceInFront();
        tablet.SetActive(show);

        Debug.Log("Tablet " + (show ? "shown" : "hidden") + " at " + tablet.transform.position);
    }

    private void PlaceInFront()
    {
        // Напрямок погляду без нахилу вгору/вниз.
        Vector3 forward = head.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude < 0.001f)
            forward = Vector3.forward;
        forward.Normalize();

        // Без шолома голова може бути на рівні підлоги, тоді беремо 1.6 м.
        Vector3 eyes = head.position;
        float floorY = head.root.position.y;
        if (eyes.y - floorY < 1f)
            eyes.y = floorY + fallbackEyeHeight;

        tablet.transform.position = eyes + forward * distance + Vector3.up * heightOffset;
        tablet.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }
}
