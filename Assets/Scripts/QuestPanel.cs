using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : MonoBehaviour
{  
    [SerializeField]
    private Image m_questImage = null;
    [SerializeField]
    private Image m_questTitleImage = null;
    [SerializeField]
    private Image m_questBackGroundImage = null;
    [SerializeField]
    private Text m_questName = null;
    [SerializeField]
    private Text m_questText = null;

    string m_loadSceneName = null;

    private Quest m_quest = null;
    public void SetQuest(Quest quest)
    {
        m_quest = quest;
        m_questImage.sprite = m_quest.QuestImage();
        m_questTitleImage.color = m_quest.QuestColor();
        m_questBackGroundImage.sprite = m_quest.QuestBackGroundImage();
        m_loadSceneName = m_quest.LoadSceneName();
        m_questName.text = m_quest.QuestName();
        m_questText.text = m_quest.QuestText();
    }
    private void Update()
    {
        if (!m_quest) Debug.LogError("QuestDataがありません！");
        m_questImage.sprite = m_quest.QuestImage();
        m_questTitleImage.color = m_quest.QuestColor();
        m_questBackGroundImage.sprite = m_quest.QuestBackGroundImage();
        m_loadSceneName = m_quest.LoadSceneName();
        m_questName.text = m_quest.QuestName();
        m_questText.text = m_quest.QuestText();
    }

    public void LoadQuest()
    {
        SceneLoader.Instance.SceneLoad(m_loadSceneName);
    }
}
