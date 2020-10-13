using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyTabInputHandler : MonoBehaviour
{
    public GameObject[] tabList;
    public GameObject tabMarks;
    public TextMeshProUGUI tabName;
    public Sprite fullMark;
    public Sprite emptyMark;
    private int activeTabIndex = 0;

    public float timeToWaitUntilFastScrolling = 0.5f;
    public float scrollSpeed = 0.1f;
    private float cooldown = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        tabMarks = transform.Find("TabMarks").gameObject;
        tabName = transform.Find("PartyTabtext").GetComponent<TextMeshProUGUI>();

        // Sets only the first tab active
        foreach (var el in tabList)
            el.SetActive(false);

        tabList[0].SetActive(true);
        tabMarks.transform.GetChild(activeTabIndex).GetComponent<Image>().sprite = fullMark;
        tabName.text = tabList[activeTabIndex].name;
    }


    /// <summary>
    /// This nice chunk of shit sets "Conditon tab" active when Entermenu is opened
    /// </summary>
    private void OnEnable()
    {

        tabList[activeTabIndex].SetActive(false);
        tabMarks.transform.GetChild(activeTabIndex).GetComponent<Image>().sprite = emptyMark;

        activeTabIndex = 0;

        tabList[activeTabIndex].SetActive(true);
        tabMarks.transform.GetChild(activeTabIndex).GetComponent<Image>().sprite = fullMark;
        tabName.text = tabList[activeTabIndex].name;
    }

    // Update is called once per frame
    void Update()
    {
        // Menu scrolling speed handler
        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveMenu(1);
            cooldown = timeToWaitUntilFastScrolling;
        }
        if (Input.GetKey(KeyCode.RightArrow) && cooldown < 0)
        {
            MoveMenu(1);
            cooldown = scrollSpeed;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveMenu(-1);
            cooldown = timeToWaitUntilFastScrolling;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && cooldown < 0)
        {
            MoveMenu(-1);
            cooldown = scrollSpeed;
        }
    }
 
    /// <summary>
    /// Move through the submenu
    /// </summary>
    /// <param name="direction">decides the direction of movement</param>
    void MoveMenu(int direction)
    {
        tabList[activeTabIndex].SetActive(false);

        tabMarks.transform.GetChild(activeTabIndex).GetComponent<Image>().sprite = emptyMark;

        activeTabIndex += direction;

        if (activeTabIndex >= tabList.Length)
            activeTabIndex = 0;

        if (activeTabIndex < 0)
            activeTabIndex = tabList.Length - 1;

        tabList[activeTabIndex].SetActive(true);

        tabMarks.transform.GetChild(activeTabIndex).GetComponent<Image>().sprite = fullMark;
        if (tabList[activeTabIndex].name == "Performance1" ^ tabList[activeTabIndex].name == "Performance2")
        {
            tabName.text = "Performance";
        }
        else
        {
            tabName.text = tabList[activeTabIndex].name;
        }
    }
}
