using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Penguin : MonoBehaviour
{

    [SerializeField] float ChatTime;
    [SerializeField] GameObject Chat;
    public int Penguinidx;
    private void Start()
    {
        StartCoroutine("Chatting");
    }
    IEnumerator Chatting()
    {
        yield return new WaitForSeconds(ChatTime);
        if(Random.Range(0,3)==0)
        {
            float timer = 1;
            GameObject RandomChat = Chat.transform.GetChild(Random.Range(0, Chat.transform.childCount)).gameObject;
            RandomChat.transform.position = transform.position + new Vector3(2.1f, 2.3f, 0);
            Color color = RandomChat.GetComponent<Image>().color;
            color.a = timer;
            RandomChat.GetComponent<Image>().color = color;
            RandomChat.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, timer);
            yield return new WaitForSeconds(1);
            while (timer >= 0)
            {
                color.a = timer;
                RandomChat.GetComponent<Image>().color = color;
                RandomChat.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, timer);
                timer -= Time.deltaTime / 2;
                yield return null;
            }
        }
        StartCoroutine("Chatting");
        yield return null;
    }
}
