using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject[] popUps;
    public int popUpIndex;

    private bool w, a, s, d, q, e;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("tutorial"))
        {
            PlayerPrefs.SetInt("tutorial", 0);
        }

        popUpIndex = PlayerPrefs.GetInt("tutorial");
    }

    public void ResetTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 0);
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
            if (player.moveDirection.x == 1 || player.moveDirection.x == -1 || player.moveDirection.z == 1 || player.moveDirection.z == -1)
            {
                if (player.moveDirection.x == 1) { w = true; }
                if (player.moveDirection.x == -1) { s = true; }
                if (player.moveDirection.z == 1) { d = true; }
                if (player.moveDirection.z == -1) { a = true; }

                if (w && a && s && d)
                {
                    popUpIndex++;
                    Save();
                }
            }
        }
        else if (popUpIndex == 1)
        {
            if (player.yaw == 1 || player.yaw == -1)
            {
                if (player.yaw == 1) { q = true; }
                if (player.yaw == -1) { e = true; }

                if (q && e)
                {
                    popUpIndex++;
                    Save();
                }
            }
        }
        else if (popUpIndex == 2)
        {
            if (player.mainThruster)
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
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("tutorial", popUpIndex);
    }
}
