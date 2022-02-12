using UnityEngine;
using UnityEngine.UI;

public class Version: MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();

        text.text = "ver " + Application.version;
    }
}
