using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI contentText;
    private Button continueButton;
    private List<string> contentList;
    private int contentIndex = 0;

    private GameObject uiGameObject;
    private Action OnDiagueEnd;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        nameText = transform.Find("UI/NameTextBg/NameText").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("UI/ContentText").GetComponent<TextMeshProUGUI>();
        continueButton = transform.Find("UI/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(this.OnContinueButtonclick);

        uiGameObject = transform.Find("UI").gameObject;
        Hide();
    }
    public void Show()
    {
        uiGameObject.SetActive(true);
    }

    public void Show(string name, string[] content, Action OnDiagueEnd = null)
    {
        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentIndex = 0;
        contentText.text = contentList[0];
        uiGameObject.SetActive(true);
        this.OnDiagueEnd = OnDiagueEnd;

    }

    public void Hide()
    {
        uiGameObject.SetActive(false);
    }

    void OnContinueButtonclick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            OnDiagueEnd?.Invoke();
            Hide(); return;
        }
        contentText.text = contentList[contentIndex];

    }
}
