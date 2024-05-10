using System;
using System.Collections;
using System.Collections.Generic;
using Q4;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI bodyText;
    
    [SerializeField] private Button[] actionButtons;

    public PopupModel Model;
    public PopupModel parentPopup;
    
    public Action<bool> OnClose;
    
    public int ButtonCount
    {
        get
        {
            return actionButtons.Length;
        }
    }

    public void Close(bool isUserRequest = true)
    {
        OnClose?.Invoke(isUserRequest);
        Destroy(gameObject);
    }

    public void LoadPopupModel(PopupModel model)
    {
        Model = model;
        
        titleText.text = model.Title;
        bodyText.text = model.Body;

        if (model.ButtonLabels != null && actionButtons != null)
        {
            if (model.ButtonLabels.Length != actionButtons.Length)
            {
                Debug.LogError($"[PopupView] - Button Count mismatch between view({actionButtons.Length}) and model({model.ButtonLabels.Length}).");
                return;
            }
        }
        else if (!(model.ButtonLabels == null && actionButtons == null))
        {
            Debug.LogError($"[PopupView] - Button Count mismatch between view({actionButtons}) and model({model.ButtonLabels}).");
            return;
        }
        

        for (int i = 0; i < actionButtons.Length; i++)
        {
            int buttonIndex = i;
            actionButtons[i].onClick.AddListener(() => model.ButtonActions[buttonIndex].Invoke());
            actionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = model.ButtonLabels[i];
        }
    }
}

