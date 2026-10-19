using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Форма налаштувань: Save зберігає значення в PlayerPrefs і застосовує їх до кімнати, Reset повертає стандартні.
public class SettingsForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Toggle lampToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private RoomSettings room;   // TabletController

    // Кожного разу, коли відкривається екран, показуємо збережені значення.
    private void OnEnable()
    {
        nameInput.text = PlayerPrefs.GetString("PlayerName", RoomSettings.DefaultName);
        lampToggle.isOn = PlayerPrefs.GetInt("Lamp", RoomSettings.DefaultLamp ? 1 : 0) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("Music", RoomSettings.DefaultMusic ? 1 : 0) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", RoomSettings.DefaultVolume);
        statusText.text = "";
    }

    public void Save()
    {
        string playerName = nameInput.text.Trim();
        if (playerName.Length == 0)
        {
            statusText.text = "Please enter a name";
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("Lamp", lampToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Music", musicToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();

        room.Apply(lampToggle.isOn, musicToggle.isOn, volumeSlider.value);
        statusText.text = "Saved, " + playerName + "!";
    }

    public void ResetToDefaults()
    {
        nameInput.text = RoomSettings.DefaultName;
        lampToggle.isOn = RoomSettings.DefaultLamp;
        musicToggle.isOn = RoomSettings.DefaultMusic;
        volumeSlider.value = RoomSettings.DefaultVolume;
        statusText.text = "Defaults restored, press Save";
    }
}