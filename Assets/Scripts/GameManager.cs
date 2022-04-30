using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool menuOpen = true;
    [Space]
    [SerializeField] private float _gravityConstant = 2.5f;
    public static float gravityConstant
    {
        get => Instance._gravityConstant;
        set => Instance._gravityConstant = value;
    }

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("More then one instances exist, only one can be used");
            return;
        }

        Instance = this;
    }
}
