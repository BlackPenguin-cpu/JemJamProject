using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [Header("Coin Info")]
    public long Coin;
    public long ClickCoinUp;
    public long secCoinup;
    public float CoinBouce;

    [Header("Wait Reward")]
    public DateTime dateTime;
    public DateTime beforeDateTime;
    public GameObject waitBoard;
    public TextMeshProUGUI waitRewardGoldText;
    float waitGoldValue;

    float Coolodwn;
    private void Start()
    {
        dateTime = DateTime.Now;
        RestartRewardBoardOn();

        SoundManager.Instance.PlaySound("Ingame", SoundType.BGM, 1, 1);
    }
    private void Update()
    {
        Coolodwn += Time.deltaTime;
        DateTime date = DateTime.Now;

        dateTime = DateTime.Now;
        if (Coolodwn >= 1)
        {
            Coolodwn = 0;
            Coin += secCoinup;
        }
    }
    void RestartRewardBoardOn()
    {
        if (secCoinup != 0)
        {
            TimeSpan value = dateTime - beforeDateTime;
            waitGoldValue = Mathf.Clamp((float)value.TotalSeconds, 0, 24000);
            waitGoldValue *= secCoinup / 5;

            waitRewardGoldText.text = GetThousandCommaText((long)waitGoldValue);
            waitBoard.SetActive(true);
        }
    }
    public void restartReward()
    {
        Coin += (long)waitGoldValue;
    }
    public void ClickAction()
    {
        Coin += ClickCoinUp;
        SoundManager.Instance.PlaySound("Coin", SoundType.SE, 1, 1);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        var obj = Instantiate(Resources.Load<GameObject>("Coin"), pos, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector3(UnityEngine.Random.Range(-2, 2), CoinBouce, 0), ForceMode2D.Impulse);
        obj.GetComponent<SpriteRenderer>().DOFade(0, 1);
        Destroy(obj, 2);
    }
    public string GetThousandCommaText(long data)
    {
        return string.Format("{0:#,###}", data);
    }
}
