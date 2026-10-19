using UnityEngine;

// Застосовує налаштування до кімнати: лампи та музика з грамофона.
// Висить на завжди активному об'єкті, тож працює одразу після старту, навіть коли планшет закритий.
public class RoomSettings : MonoBehaviour
{
    // Стандартні значення для першого запуску і кнопки Reset.
    public const string DefaultName = "Player";
    public const bool DefaultLamp = true;
    public const bool DefaultMusic = false;
    public const float DefaultVolume = 0.7f;

    [SerializeField] private Light[] lamps;     // Lamp_1, Lamp_2
    [SerializeField] private AudioSource music; // Audio Source на грамофоні

    private void Start()
    {
        Apply(PlayerPrefs.GetInt("Lamp", DefaultLamp ? 1 : 0) == 1,
              PlayerPrefs.GetInt("Music", DefaultMusic ? 1 : 0) == 1,
              PlayerPrefs.GetFloat("Volume", DefaultVolume));
    }

    public void Apply(bool lampOn, bool musicOn, float volume)
    {
        foreach (Light lamp in lamps)
            lamp.enabled = lampOn;

        music.volume = volume;
        if (musicOn && !music.isPlaying)
            music.Play();
        else if (!musicOn && music.isPlaying)
            music.Stop();
    }
}