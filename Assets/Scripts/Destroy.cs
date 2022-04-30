using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float minTime = 20;
    [SerializeField] private float maxTime = 40;

    private void Awake()
    {
        Destroy(this.gameObject, Random.Range(minTime, maxTime));
    }
}
