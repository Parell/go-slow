using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public Image questItem;
    public Color completeColor;
    public Color activeColor;
    public Color currentColor;
    public bool isActive;

    public Quest[] allQuests;

    private void Start()
    {
        allQuests = FindObjectsOfType<Quest>();
        currentColor = questItem.color;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && isActive)
        {
            FinishQuest();
            Destroy(gameObject);
        }
    }

    private void FinishQuest()
    {
        questItem.GetComponent<Button>().interactable = false;
        currentColor = completeColor;
        questItem.color = completeColor;
        isActive = false;
    }

    public void OnQuestClick()
    {
        foreach (Quest quest in allQuests)
        {
            quest.questItem.color = quest.currentColor;
        }
        questItem.color = activeColor;
        isActive = true;
    }
}
