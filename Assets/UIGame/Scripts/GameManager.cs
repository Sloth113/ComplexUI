using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private int m_xpToNextLevel = 100;
    [SerializeField] private int m_level = 0;

    [Header("Inventory")]
    [SerializeField] ItemDetail[]  m_items;
    [Header("UI")]


    //Change to CUI canvas
    [SerializeField] private CUI.CUICanvas m_menuCanvas;
    [SerializeField] private CUI.CUICanvas m_gameCanvas;
    //
    [SerializeField] private CUI.CUINumber m_foodUI;
    [SerializeField] private CUI.CUINumber m_goldUI;
    [SerializeField] private Image m_xpUI;//CUI.CUIImage m_xpUI;
    [SerializeField] private CUI.CUINumber m_levelUI;
    [SerializeField] private CUI.CUINumber m_currentXPUI;
    [SerializeField] private CUI.CUINumber m_nextLevelXPUI;



    private bool m_changing = false;


    private void Awake()
    {
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
        

        //UpdateUI();
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

                    UpdateUI();
                }
                break;
            case State.Menu:
                {
                    
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

        UpdateUI();
    }

    public void LevelUp()
    {
        if(m_items[(int)Items.XP].amt >= m_xpToNextLevel)
        {
            m_level++;
            m_items[(int)Items.XP].amt -= m_xpToNextLevel;
            m_xpToNextLevel += m_xpToNextLevel;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        m_foodUI.SetValue(m_items[(int)Items.Food].amt);
        m_goldUI.SetValue(m_items[(int)Items.Gold].amt);
        m_xpUI.fillAmount = m_items[(int)Items.XP].amt / (float)m_xpToNextLevel;
        m_currentXPUI.SetValue(m_items[(int)Items.XP].amt);
        m_nextLevelXPUI.SetValue(m_xpToNextLevel);
        m_levelUI.SetValue(m_level);
    }

    #region Save&Load
    //TODO
    public void Save()
    {
        Debug.Log("Save a thing..");
    }

    public void Load()
    {
        Debug.Log("Load a thing!"); UpdateUI();
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
