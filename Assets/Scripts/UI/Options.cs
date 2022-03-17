using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Vector2[] resolutions;
    [Space]
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [Space]
    [SerializeField] private AudioMixer audioMixer;

    private void Awake()
    {
        ResolutionUI();

        fullscreenToggle.onValueChanged.AddListener(Fullscreen);
        resolutionDropdown.onValueChanged.AddListener(Resolution);
        vsyncToggle.onValueChanged.AddListener(Vsync);

        masterSlider.onValueChanged.AddListener(MasterVolume);
        musicSlider.onValueChanged.AddListener(MusicVolume);

        if (!PlayerPrefs.HasKey("resolution"))
        {
            ResetSettings();
        }

        //Application.targetFrameRate = 60;

        Load();
    }

    #region Settings
    private void ResolutionUI()
    {
        var options = new List<Dropdown.OptionData>();
        int selected = 0;
        foreach (var res in resolutions)
        {
            string text = res.x + "x" + res.y;

            options.Add(new Dropdown.OptionData(text));
        }
        resolutionDropdown.options = options;

        resolutionDropdown.value = selected;
    }

    public void Resolution(int value)
    {
        var resolution = resolutions[value];
        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreen);
    }

    public void Fullscreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void Vsync(bool value)
    {
        if (!value) { QualitySettings.vSyncCount = 0; }
        else { QualitySettings.vSyncCount = 1; }
    }

    public void MasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void MusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    #endregion

    #region Save and Load
    public void ResetSettings()
    {
        PlayerPrefs.SetInt("resolution", 0);
        PlayerPrefs.SetInt("fullscreen", boolToInt(true));
        PlayerPrefs.SetInt("vsync", 1);
        PlayerPrefs.SetFloat("master", 1);
        PlayerPrefs.SetFloat("music", 1);

        Debug.Log("Reset settings");
    }

    public void Load()
    {
        int resolution = PlayerPrefs.GetInt("resolution");
        bool fullscreen = intToBool(PlayerPrefs.GetInt("fullscreen", 0));
        bool vsync = intToBool(PlayerPrefs.GetInt("vsync", 0));

        float master = PlayerPrefs.GetFloat("master");
        float music = PlayerPrefs.GetFloat("music");

        masterSlider.value = master;
        MasterVolume(master);

        musicSlider.value = music;
        MusicVolume(music);

        resolutionDropdown.value = resolution;
        Resolution(resolution);

        fullscreenToggle.isOn = fullscreen;
        Fullscreen(fullscreen);

        vsyncToggle.isOn = vsync;
        Vsync(vsync);

        Debug.Log("Load settings");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("fullscreen", boolToInt(fullscreenToggle.isOn));
        PlayerPrefs.SetInt("vsync", boolToInt(vsyncToggle.isOn));

        PlayerPrefs.SetFloat("master", masterSlider.value);
        PlayerPrefs.SetFloat("music", musicSlider.value);

        Debug.Log("Save settings");
    }

    private int boolToInt(bool value)
    {
        if (value)
            return 1;
        else
            return 0;
    }

    private bool intToBool(int value)
    {
        if (value != 0)
            return true;
        else
            return false;
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion
}
