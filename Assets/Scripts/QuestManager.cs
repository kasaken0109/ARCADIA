using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] Quest[] m_quests = null;
    [SerializeField] QuestPanel[] m_questPanels = null;
    int[] m_questIndex;
    // Start is called before the first frame update
    void Awake()
    {
        m_questIndex = new int[m_quests.Length];
        for (int i = 0; i < m_questPanels.Length; i++)
        {
            m_questPanels[i].SetQuest(m_quests[i]);
            m_questIndex[i] = i;
        }
    }

    int index;
    public void Prev()
    {
        for (int i = 0; i < m_questPanels.Length; i++)
        {
            int index = m_questIndex[i];
            index = index == 0 ? m_quests.Length - 1 : index - 1;
            m_questPanels[i].SetQuest(m_quests[index]);
            m_questIndex[i] = index;
        }
    }
    public void Next()
    {
        for (int i = 0; i < m_questPanels.Length; i++)
        {
            int index = m_questIndex[i];
            index = index == m_quests.Length - 1 ? 0 : index + 1;
            m_questPanels[i].SetQuest(m_quests[index]); ;
            m_questIndex[i] = index;
        }
    }
}
