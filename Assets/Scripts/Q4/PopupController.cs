using System;
using System.Collections;
using System.Collections.Generic;
using Q2.Interface_Implementation.Common;
using Q4;
using UnityEngine;

public class PopupController : MonoBehaviour, ITaskQueue
{
    [SerializeField] private Transform canvasTransform;
    
    private List<PopupModel> _popupQueue = new List<PopupModel>();

    private PopupView shownPopup;
    private ITaskQueue _taskQueueImplementation;

    public bool ShowingPopup
    {
        get { return shownPopup != null; }
    }

    private void Awake()
    {
        // Example Popup open-close worktree.
        // This is just an example. Different game modules will be responsible
        // of assigning actions to buttons.
        
        // Because this is ugly and unmanageable :D
        
        ShowPopup(new PopupModel("Example-Popup", "Welcome", "Welcome to fake in-game shop!", false, new string[2]{"Next", "Close"}, new Action[2]
        {
            () =>
            {
                ShowPopup(new PopupModel("Example-Popup", "In-Game Shop!", "Buy X of X?", true, new string[2]{"Yes", "No"}, new Action[2]
                {
                    () =>
                    {
                        ShowPopup(new PopupModel("Example-Popup", "Confirmation", "Are You Sure?", true, new string[2]{"Yes", "No"}, new Action[2]
                        {
                            () => { shownPopup.Close();}, () => { shownPopup.Close();}
                        }));
                    }, 
                    () => 
                    {
                        shownPopup.Close();
                    }
                }));
            },
            () =>
            {
                shownPopup.Close();
            }
        }));
        
        
    }

    private void ShowPopup(PopupModel model)
    {
        if (!ShowingPopup)
        {
            Enqueue(model);
            ExecuteNext();
        }
        else
        {
            if (model.IsInner)
            {
                shownPopup.Close(false);
                SpawnPopup(model);
            }
            else
            {
                Enqueue(model);
            }
        }
    }

    private void SpawnPopup(PopupModel model)
    {
        Debug.LogError(model.Title);
        
        var loadedPopup = Resources.Load<PopupView>(model.ResourceAssetName);
        var newPopup = Instantiate(loadedPopup, canvasTransform);
        //newPopup.transform.position = Vector3.zero;
        newPopup.LoadPopupModel(model);
        newPopup.OnClose += OnPopupClose;

        if (model.IsInner)
        {
            if (ShowingPopup)
            {
                newPopup.parentPopup = shownPopup.Model;
            }
        }

        shownPopup = newPopup;
    }

    private void OnPopupClose(bool userRequest)
    {
        if (shownPopup.Model.IsInner)
        {
            if (shownPopup.parentPopup != null)
            {
                SpawnPopup(shownPopup.parentPopup);
                return;
            }
        }
        else
        {
            
            shownPopup = null;
            if (_popupQueue.Count > 0)
            {
                ExecuteNext();
            }
        }
    }

    public void Enqueue(ITask task)
    {
        if (task is PopupModel model)
        {
            Debug.Log(model.Title);
            _popupQueue.Insert(_popupQueue.Count ,model);
        }
    }

    public void ExecuteNext()
    {
        if (_popupQueue.Count > 0)
        {
            var nextPopup = _popupQueue[^1];
            _popupQueue.Remove(nextPopup);
            SpawnPopup(nextPopup);
        }
    }
}