using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.Localization;
//using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Vector2[] resolutions;
    //[SerializeField] private GameObject options;
    [Space]
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle vsyncToggle;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectsSlider;
    [Space]
    [SerializeField] private AudioMixer audioMixer;
    //[Space]
    //[SerializeField] private Dropdown languageDropdown;
    //[SerializeField] private string playerPreferenceKey = "selected_locale";

    //private string code;

    private void Awake()
    {
        ResolutionUI();

        fullscreenToggle.onValueChanged.AddListener(Fullscreen);
        resolutionDropdown.onValueChanged.AddListener(Resolution);
        vsyncToggle.onValueChanged.AddListener(Vsync);

        masterSlider.onValueChanged.AddListener(MasterVolume);
        musicSlider.onValueChanged.AddListener(MusicVolume);
        effectsSlider.onValueChanged.AddListener(EffectsVolume);

        if (!PlayerPrefs.HasKey("resolution"))
        {
            ResetSettings();
        }

        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Load();

        //    // Wait for the localization system to initialize
        //    yield return LocalizationSettings.InitializationOperation;

        //    // Generate list of available Locales
        //    var options = new List<Dropdown.OptionData>();
        //    int selected = 0;
        //    for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        //    {
        //        var locale = LocalizationSettings.AvailableLocales.Locales[i];
        //        if (LocalizationSettings.SelectedLocale == locale)
        //            selected = i;
        //        options.Add(new Dropdown.OptionData(locale.Identifier.Code.ToUpper()));
        //    }
        //    languageDropdown.options = options;

        //    languageDropdown.value = selected;
        //    languageDropdown.onValueChanged.AddListener(LocaleSelected);
    }

    #region Language
    //public void PostInitialization(LocalizationSettings settings)
    //{
    //    if (Application.isPlaying)
    //    {
    //        // Record the new selected locale so it can persist between runs
    //        var selectedLocale = settings.GetSelectedLocale();
    //        if (selectedLocale != null)
    //            PlayerPrefs.SetString(playerPreferenceKey, selectedLocale.Identifier.Code);
    //    }
    //}

    //public Locale GetStartupLocale(ILocalesProvider availableLocales)
    //{
    //    if (PlayerPrefs.HasKey(playerPreferenceKey))
    //    {
    //        code = PlayerPrefs.GetString(playerPreferenceKey);
    //        if (!string.IsNullOrEmpty(code))
    //        {
    //            return availableLocales.GetLocale(code);
    //        }
    //    }

    //    // No locale could be found
    //    return null;
    //}

    //private static void LocaleSelected(int index)
    //{
    //    LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    //}
    #endregion

    #region Settings
    private void ResolutionUI()
    {
        var options = new List<Dropdown.OptionData>();
        int selected = 0;
        foreach (var res in resolutions)
        {
            string text = res.x + "x" + res.y/* + " @" + res.refreshRate*/;

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

    public void EffectsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }
    #endregion

    #region Save and Load
    public void ResetSettings()
    {
        PlayerPrefs.SetInt("resolution", 0);
        PlayerPrefs.SetInt("fullscreen", boolToInt(true));
        PlayerPrefs.SetInt("vsync", 1);
        PlayerPrefs.SetFloat("master", 1);
        PlayerPrefs.SetFloat("music", 0.5f);
        PlayerPrefs.SetFloat("effects", 0.5f);

        Debug.Log("Reset settings");
    }

    public void Load()
    {
        int resolution = PlayerPrefs.GetInt("resolution");
        bool fullscreen = intToBool(PlayerPrefs.GetInt("fullscreen", 0));
        bool vsync = intToBool(PlayerPrefs.GetInt("vsync", 0));

        float master = PlayerPrefs.GetFloat("master");
        float music = PlayerPrefs.GetFloat("music");
        float effects = PlayerPrefs.GetFloat("effects");

        masterSlider.value = master;
        MasterVolume(master);

        musicSlider.value = music;
        MusicVolume(music);

        effectsSlider.value = effects;
        EffectsVolume(effects);

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
        PlayerPrefs.SetFloat("effects", effectsSlider.value);

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
