using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextFeedBackBehaviour : MonoBehaviour
{
    float timer;
    [SerializeField] Transform startPos, endPos;
    
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.position = startPos.position;
        timer = 0;
    }
    void Update()
    {
        float speed = 1f;
        timer += Time.deltaTime * speed;
        transform.localScale = Vector3.one * timer;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, timer);
        if(timer >= 1.2f)
            gameObject.SetActive(false);
    }
}
