using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Jobbutton : ItemCard
{
    [Header("구매 정보")]
    public int Penguinidx;
    public bool Buy;
    [SerializeField] int BuyincrementMoney;
    [Space(10)]
    [Header("레벨업 정보")]
    public int Level;
    [SerializeField] int MaxLevel;
    [SerializeField] int firstLevelUpMoney;
    [SerializeField] int LevelUpMoney;
    [SerializeField] int incrementMoney;

    private TextMeshProUGUI LevelText;
    protected override void Start()
    {
        base.Start();
        LevelText = gameObject.transform.Find("Level").GetComponent<TextMeshProUGUI>();
        if (Buy == false)
        {
            buttonText.text = "구매" + "\n" + $"({GetThousandCommaText(BuyMoney)})";
            buttonText.text = "고용하기" + "\n" + $"({GetThousandCommaText(BuyMoney)})";
            desc.text = "초당 흭득 골드" + "\n" + $"{GetThousandCommaText(BuyincrementMoney + incrementMoney * Level)}";
        }
        else
        {
            buttonText.text = "레벨업" + "\n" + $"({GetThousandCommaText(firstLevelUpMoney + LevelUpMoney * Level)})";
            desc.text = "초당 흭득 골드" + "\n" + $"{GetThousandCommaText(BuyincrementMoney + incrementMoney * Level)} -> {GetThousandCommaText(BuyincrementMoney + incrementMoney * (Level + 1))}";
            LevelText.text = $"Lv.{Level}";
            for (int i = 0; i < 4; i++)
            {
                if (GameObject.Find("Penguins").transform.GetChild(i).GetComponent<Penguin>().Penguinidx == Penguinidx)
                    GameObject.Find("Penguins").transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    protected override void Action()
    {
        if (GameManager.Instance.Coin >= BuyMoney && Buy == false)
        {
            for (int i = 0; i < 4; i++)
            {
                if (GameObject.Find("Penguins").transform.GetChild(i).GetComponent<Penguin>().Penguinidx == Penguinidx)
                    GameObject.Find("Penguins").transform.GetChild(i).gameObject.SetActive(true);
            }

            GameManager.Instance.Coin -= BuyMoney;
            GameManager.Instance.secCoinup += BuyincrementMoney;
            Buy = true;
            SoundManager.Instance.PlaySound("Buy", SoundType.SE, 1, 1);
            buttonText.text = "레벨업" + "\n" + $"({GetThousandCommaText(firstLevelUpMoney + LevelUpMoney * Level)})";
            desc.text = "초당 흭득 골드" + "\n" + $"{GetThousandCommaText(BuyincrementMoney + incrementMoney * Level)} -> {GetThousandCommaText(BuyincrementMoney + incrementMoney * (Level + 1))}";
        }
        else if (GameManager.Instance.Coin >= firstLevelUpMoney + LevelUpMoney * Level && Level != MaxLevel)
        {
            GameManager.Instance.Coin -= firstLevelUpMoney + LevelUpMoney * Level;
            GameManager.Instance.secCoinup += BuyincrementMoney + incrementMoney * Level;
            Level++;
            buttonText.text = "레벨업" + "\n" + $"({GetThousandCommaText(firstLevelUpMoney + LevelUpMoney * Level)})";
            desc.text = "초당 흭득 골드" + "\n" + $"{GetThousandCommaText(BuyincrementMoney + incrementMoney * Level)} -> {GetThousandCommaText(BuyincrementMoney + incrementMoney * (Level + 1))}";
            LevelText.text = $"Lv.{Level + 1}";
            SoundManager.Instance.PlaySound("Buy", SoundType.SE, 1, 1);
        }
        else if (GameManager.Instance.Coin <= firstLevelUpMoney + LevelUpMoney * Level || GameManager.Instance.Coin <= BuyMoney)
        {
            SoundManager.Instance.PlaySound("Don_t_Buy", SoundType.SE, 1, 1);
        }
    }
    public string GetThousandCommaText(long data)
    {
        return string.Format("{0:#,###}", data);
    }
}
