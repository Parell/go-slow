using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    public int popUpIndex;
    [SerializeField] private Movement movement;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("tutorial"))
        {
            PlayerPrefs.SetInt("tutorial", 0);
        }

        popUpIndex = PlayerPrefs.GetInt("tutorial");
    }

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (movement.moveDirection.x == -1 || movement.moveDirection.x == 1 || movement.moveDirection.z == -1 || movement.moveDirection.z == 1)
            {
                popUpIndex++;
                Save();
            }
        }
        else if (popUpIndex == 1)
        {
            if (movement.yaw == -1 || movement.yaw == 1)
            {
                popUpIndex++;
                Save();
            }
        }
        else if (popUpIndex == 2)
        {
            if (movement.mainThruster)
            {
                popUpIndex++;
                Save();
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.mouseScrollDelta.y >= 1 || Input.mouseScrollDelta.y <= -1)
            {
                popUpIndex++;
                Save();
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetMouseButtonUp(0))
            {
                popUpIndex++;
                Save();
            }
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("tutorial", popUpIndex);
    }
}
