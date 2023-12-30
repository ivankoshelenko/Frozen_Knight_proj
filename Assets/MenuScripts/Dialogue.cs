using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using static UnityEngine.UI.Button;
using resources;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour
{
    public Button firstButton, secondButton, thirdButton;

    public TextMeshProUGUI mainDialogue;
    public static ResourceManager resources;
    public static int karma;
    public int giveNothing;
    [SerializeField] private TextMeshProUGUI _woodDisplay;
    [SerializeField] private TextMeshProUGUI _rockDisplay;
    [SerializeField] private TextMeshProUGUI _foodDisplay;
    [SerializeField] private Slider karmaSlider;

    public enum Stages {peopleFirst, upgradeFirst,peopleSecond,upgradeSecond}
    // Start is called before the first frame update
    void Start()
    {
        //Object.FindObjectOfType<DontDestroy>().GetComponent<DontDestroy>().gameObject.SetActive(false);
    }
    void Awake()
    {
        firstButton.onClick.AddListener(ChoosePeople); //subscribe to the onClick event
    }
    void RefreshButtons()
    {
        firstButton.enabled = true;
        secondButton.enabled = true;
        thirdButton.enabled = true;
    }
    public void ChoosePeople()
    {
        secondButton.gameObject.SetActive(true);
        RefreshButtons();
        thirdButton.gameObject.SetActive(true);
        firstButton.onClick.RemoveAllListeners();
        mainDialogue.text = "�� �������, ����� ��� ���";
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "� ��� ��� 50 ���";
        firstButton.onClick.AddListener(DisableThirdButton);
        firstButton.onClick.AddListener(GiveFood);       
        if (ResourceManager.food < 50)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);
        }


        secondButton.onClick.RemoveAllListeners();
        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "��� �� ������!(15 ��� � 5 ������)";
        secondButton.onClick.AddListener(GiveSawdust);
        secondButton.onClick.AddListener(DisableThirdButton);
        secondButton.onClick.AddListener(WoodRequest);
        if (ResourceManager.food < 15 || ResourceManager.wood < 5)
        {
            secondButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(thirdButton.gameObject);
        }

        thirdButton.onClick.RemoveAllListeners();
        thirdButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ��� ��� ������";
        thirdButton.onClick.AddListener(DisableThirdButton);
        thirdButton.onClick.AddListener(GiveNothing);    
        if (ResourceManager.food < 15 || ResourceManager.wood < 5)
            secondButton.enabled = false;
    }
    public void GiveSawdust()
    {
        LoseResources(5, 15, 0);
    }
    public void GiveNothing()
    {
        RefreshButtons();
        firstButton.onClick.RemoveAllListeners();
        karma -= 3;

        mainDialogue.text = "�� ��� ��� �������� �������, ���� �� ������ ��� �� ��������, ������, �������! ����� ��� ���� �� 20 ������, ����� ���������?";

        if (ResourceManager.wood <= 20)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);

        }
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "� ����� ��� 20 ������";
        firstButton.onClick.AddListener(Appreciation);
        firstButton.onClick.AddListener(PayWood);

        secondButton.onClick.RemoveAllListeners();
        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ���� ���� ��� ������";
        secondButton.onClick.AddListener(LetDown);
    }
    public void DisableThirdButton()
    {
        thirdButton.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
    }
    public void WoodRequest()
    {
        RefreshButtons();
        firstButton.onClick.RemoveAllListeners();
        secondButton.onClick.RemoveAllListeners();
        karma -= 1;

        if (ResourceManager.wood <= 20)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);

        }
        mainDialogue.text = "���� ���-��, � ������ �� �� �����, �� �������� ���� �������� � ����� �� ���������. ��� ��� ����� 20 ������, ����� ���������";
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "� ����� ��� 20 ������";
        firstButton.onClick.AddListener(Appreciation);
        firstButton.onClick.AddListener(PayWood);

        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ���� ���� ��� ������";
        secondButton.onClick.AddListener(LetDown);
    }
    public void GiveFood()
    {
        RefreshButtons();
        secondButton.gameObject.SetActive(true);
        firstButton.onClick.RemoveAllListeners();
        secondButton.onClick.RemoveAllListeners();
        karma += 2;
        ResourceManager.food -= 50;
        if (ResourceManager.wood <= 20)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);

        }
        else DisableThirdButton();
        mainDialogue.text = "������� �������� ���, ���� ��������� ������� �� ���. ��� ��� ����� 20 ������, ����� ���������";
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "� ����� ��� 20 ������";
        firstButton.onClick.AddListener(Appreciation);
        firstButton.onClick.AddListener(PayWood);
        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ���� ���� ��� ������";
        secondButton.onClick.AddListener(LetDown);
    }
    public void LetDown()
    {
        RefreshButtons();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        secondButton.gameObject.SetActive(false);
        karma -= 1;
        firstButton.onClick.RemoveAllListeners();
        if (karma >= -2)
        {
            mainDialogue.text = "�� �� � ������ ��� ����� ������ ������";
            firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "������ ���������� ������";
            firstButton.onClick.AddListener(UpgradeSword);
        }
        else
        {
            mainDialogue.text = "���� ������� �� ������ �������� ��� ��� � �����";
            firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "������ ����������� �������";
            firstButton.onClick.AddListener(NextDay);
        }
    }
    public void Appreciation()
    {
        RefreshButtons();
        secondButton.gameObject.SetActive(false);
        karma += 1;
        firstButton.onClick.RemoveAllListeners();
        if (karma >= 1)
        {
            mainDialogue.text = "��������� ��� �� ������ �������� � �����. �������� � ����� �� ���� ���������� 30 ����";
            firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �������� ����� 30 ����. ���� �� � �������� � ������� �� ��� ������?";
            GainResources(0, 0, 30f);
        }
        else
        {
            mainDialogue.text = "��������� ��� �� ���� �� ������ �������� � �����.";
            firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "���� �� � �������� ��� ������?";
        }
        firstButton.onClick.AddListener(UpgradeSword);
    }
    public void GainResources(float wood,float food,float rock)
    {
        ResourceManager.wood += wood;
        ResourceManager.rock += rock;
        ResourceManager.food += food;
        Debug.Log(ResourceManager.rock);
        //RefreshIcons();
    }
    public void LoseResources(float wood, float food, float rock)
    {
        ResourceManager.wood -= wood;
        ResourceManager.rock -= rock;
        ResourceManager.food -= food;
        Debug.Log(ResourceManager.rock);
        //RefreshIcons();
    }
    public void PayWood()
    {
        LoseResources(20, 0, 0);
    }
    public void UpgradeSword()
    {
        RefreshButtons();
        DisableThirdButton();
        firstButton.onClick.RemoveAllListeners();
        mainDialogue.text = "�������� 15 ���� �� ������ �������� ��� ���. (5 �����)";
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "��������� 15 ����";
        
        firstButton.onClick.AddListener(UpgradeArmor);
        firstButton.onClick.AddListener(PayForSword);
        if (ResourceManager.rock < 15)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);
        }

        secondButton.gameObject.SetActive(true);
        secondButton.onClick.RemoveAllListeners();
        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ���� ���������";

        secondButton.onClick.AddListener(UpgradeArmor);
    }
    public void PayForSword()
    {
        LoseResources(0, 0, 15);
        ResourceManager.attackDamage += 5f;
    }
    public void UpgradeArmor()
    {
        RefreshButtons();
        DisableThirdButton();
        firstButton.onClick.RemoveAllListeners();
        mainDialogue.text = "�������� 45 ���� �� ������ �������� ���� �������. (20 ��������)";
        firstButton.GetComponentInChildren<TextMeshProUGUI>().text = "��������� 45 ����";

        secondButton.gameObject.SetActive(true);
        if(ResourceManager.rock <45)
        {
            firstButton.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(secondButton.gameObject);
        }
        secondButton.onClick.RemoveAllListeners();
        secondButton.GetComponentInChildren<TextMeshProUGUI>().text = "� �� ���� ���������";
        secondButton.onClick.AddListener(NextDay);


        firstButton.onClick.AddListener(PayForArmor);
        firstButton.onClick.AddListener(NextDay);
    }
    void PayForArmor()
    {
        ResourceManager.rock -= 45;
        healthController.maxHealth += 20f;
    }
    public void NextDay()
    {
        //Object.FindObjectOfType<DontDestroy>().GetComponent<DontDestroy>().gameObject.SetActive(true);
        SceneManager.LoadSceneAsync(1);     
    }
    // Update is called once per frame
    void Update()
    {
        _woodDisplay.text = ((int)ResourceManager.wood).ToString();
        _rockDisplay.text = (((int)ResourceManager.rock).ToString());
        _foodDisplay.text = (((int)ResourceManager.food).ToString());
        karmaSlider.value = karma;
        if (karma <= -5)
            Debug.Log("Lose");
    }
    public static void Reset1()
    {
        karma = 0;
    }
}
