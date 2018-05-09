using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Linq;
using System.IO;

public enum Items
{
    XP= 0,
    Gold,
    Food,
    COUNT
}
[System.Serializable]
public struct ItemDetail
{
    public Items item;
    public int amt;
}

public class GameManager : MonoBehaviour {
    private enum State
    {
        Menu,
        Game
    }


    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                GameObject gm = new GameObject();
                gm.AddComponent<GameManager>();
                m_instance = gm.GetComponent<GameManager>();

                
            }
            return m_instance;
        }
    }

    private State m_currentState;

    [SerializeField] private int m_xpToNextLevel = 10;
    private int m_xpPerLevel;
    [SerializeField] private int m_level = 0;

    [Header("Inventory")]
    [SerializeField] ItemDetail[]  m_items;
    [Header("UI")]


    //Change to CUI canvas
    [Header("CUI elements")]
    [SerializeField] private CUI.CUICanvas m_menuCanvas;
    [SerializeField] private CUI.CUICanvas m_gameCanvas;
    [SerializeField] private CUI.CUINumber m_foodUI;
    [SerializeField] private CUI.CUINumber m_goldUI;
    [SerializeField] private CUI.CUIFillImage m_xpUI;
    [SerializeField] private CUI.CUINumber m_levelUI;
    [SerializeField] private CUI.CUINumber m_currentXPUI;
    [SerializeField] private CUI.CUINumber m_nextLevelXPUI;

    [SerializeField] private CUI.CUIButton m_levelUpButton;
    [SerializeField] private CUI.CUIButton m_loadButton;
    [SerializeField] private CUI.CUIButton m_clearButton;

    [Header("Click effects")]
    [SerializeField] private GameObject m_xPEffect;
    [SerializeField] private GameObject m_foodEffect;
    [SerializeField] private GameObject m_coinEffect;
    



    private bool m_changing = false;


    private void Awake()
    {
        m_xpPerLevel = m_xpToNextLevel;
        if (m_instance == null) m_instance = this;
        m_items = new ItemDetail[(int)Items.COUNT];
        //Set items from enum :) 
        Items thing = 0;
        for(int i = 0; i < m_items.Length; i++)
        {
            m_items[i].item = thing++;
            m_items[i].amt = 0;
        }
        
    }
    // Use this for initialization
    void Start () { 
        m_currentState = State.Game;
        m_gameCanvas.Enable();
        m_menuCanvas.Disable();

        //If save data 
        Load();

        UpdateUI();
    }
	
	// Update is called once per frame
	void Update () {
        switch (m_currentState)
        {
            case (State.Game):
                {
                    //Click inputs
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 1000))
                        {
                            IClickable click = hit.transform.GetComponent<IClickable>();
                            if (click != null)
                            {
                                click.Click(hit.point);
                            }
                        }
                    }

                    if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                    {
                        Pause();
                    }

                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        m_gameCanvas.ApplyActionToChildren();
                    }
                    //UpdateUI();
                }
                break;
            case State.Menu:
                {
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                    {
                        UnPause();
                    }
                }
                break;
        }
        

    }

    public void AddItem(ItemDetail thing)
    {
        for(int i = 0; i < m_items.Length; i++)
        {
            if(m_items[i].item == thing.item)
            {
                m_items[i].amt += thing.amt;
            }
        }
        CheckXP();

        //UpdateUI();
    }

    private void CheckXP()
    {
        if(m_items[(int)Items.XP].amt >= m_xpToNextLevel)
        {
            m_levelUpButton.Enable();
        }else
        {
            m_levelUpButton.Disable();
        }

    }
    public GameObject GetEffect(Items item)
    {
        switch (item)
        {
            case Items.XP:
                return m_xPEffect;
            case Items.Gold:
                return m_coinEffect;
            case Items.Food:
                return m_foodEffect;
            case Items.COUNT:
                return null;
        }
        return null;
    }

    public void LevelUp()
    {
        if(m_items[(int)Items.XP].amt >= m_xpToNextLevel)
        {
            m_level++;
            m_items[(int)Items.XP].amt -= m_xpToNextLevel;
            m_xpToNextLevel += m_xpToNextLevel;
        }
        m_levelUpButton.Disable();
        UpdateUI();
    }

    public void UpdateUI()
    {
        m_xpToNextLevel =(m_level+1) * m_xpPerLevel;
        m_xpUI.SetMax(m_xpToNextLevel);
        m_foodUI.SetValue(m_items[(int)Items.Food].amt);
        m_goldUI.SetValue(m_items[(int)Items.Gold].amt);
        m_xpUI.SetValue(m_items[(int)Items.XP].amt);
        m_currentXPUI.SetValue(m_items[(int)Items.XP].amt);
        m_nextLevelXPUI.SetValue(m_xpToNextLevel);
        m_levelUI.SetValue(m_level);
        CheckXP();
        
        
    }

    #region Save&Load
    
    public void Save()
    {
        XDocument document = new XDocument();
        XElement root = new XElement("PlayerInv");

        //Save date
        root.SetAttributeValue("Level", m_level);
        foreach (ItemDetail item in m_items)
        {
            root.SetAttributeValue(item.item.ToString(), item.amt);
        }
        
        document.Add(root);
        document.Save("savegame.xml");
        
    }

    public void Load()
    {
        if (File.Exists("savegame.xml"))
        {
            XElement root = XElement.Load("savegame.xml");
            
                //load should do more checks
                for (int i = 0; i < m_items.Length; i++)
                {
                    m_items[i].amt = int.Parse(root.Attribute(m_items[i].item.ToString()).Value);
                }
                m_level = int.Parse(root.Attribute("Level").Value);
        }
        else
        {
           // Save();
        }
        UpdateUI();
    }

    public void ClearSave()
    {
        File.Delete("savegame.xml");
    }
    
    #endregion

    #region States
    public void Pause()
    {
        Pause(true);
    }

    public void Pause(bool pause)
    {
        if (!m_changing)
        {
            if (pause)
            {
                //m_menuCanvas.enabled = true;//CUI menucanvas.Enable();
                //m_gameCanvas.enabled = false; //CUI gameCanvas.Disable();

                StartCoroutine(CallFunctionAfter(m_gameCanvas.Disable(), m_menuCanvas.Enable));
                m_currentState = State.Menu;
                
            }
            else
            {
                //m_menuCanvas.enabled = false;//CUI menucanvas.Enable();
                //m_gameCanvas.enabled = true; //CUI gameCanvas.Disable();

                StartCoroutine(CallFunctionAfter(m_menuCanvas.Disable(), m_gameCanvas.Enable));
                m_currentState = State.Game;
            }
        }
    }

    public delegate float Function();

    IEnumerator CallFunctionAfter(float time, Function fun)
    {
        m_changing = true;
        yield return new WaitForSeconds(time);
        fun();
        m_changing = false;
    }

    public void UnPause()
    {
        Pause(false);
    }

    public void Quit()
    {
        Save();
        Application.Quit();
    }
    #endregion

    #region StatGetterSetters
    public ItemDetail GetXP()
    {
        return m_items[(int)Items.XP];
    }
    public bool ChangeXP(int amt)
    {
        if (m_items[(int)Items.XP].amt + amt >= 0)
        {
            m_items[(int)Items.XP].amt += amt;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }

    public ItemDetail GetFood()
    {
        return m_items[(int)Items.Food];
    }
    public bool ChangeFood(int amt)
    {
        if (m_items[(int)Items.Food].amt + amt >= 0)
        {
            m_items[(int)Items.Food].amt += amt;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    public ItemDetail GetGold()
    {
        return m_items[(int)Items.Gold];
    }
    public bool ChangeGold(int amt)
    {
        if (m_items[(int)Items.Gold].amt + amt >= 0)
        {
            m_items[(int)Items.Gold].amt += amt;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
