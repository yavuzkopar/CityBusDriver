using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    float timer;
    [SerializeField] Transform startPos, endPos;

    private void OnEnable()
    {
        transform.position = startPos.position;
        timer = 0;
    }

    void Update()
    {
        float speed = 1f;
        timer += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, timer);
        if (timer >= 1f)
            gameObject.SetActive(false);
    }
}
