using System;
using Q2.Interface_Implementation.Common;

namespace Q4
{
    public class PopupModel : ITask
    {
        public string ResourceAssetName;
        public string Title;
        public string Body;
        public bool IsInner;
        public string[] ButtonLabels;
        public Action[] ButtonActions;

        public PopupModel(string resourceAssetName,string title, string body, bool isInner, string[] buttonLabels, Action[] buttonActions)
        {
            ResourceAssetName = resourceAssetName;
            Title = title;
            Body = body;
            IsInner = isInner;
            ButtonLabels = buttonLabels;
            ButtonActions = buttonActions;
        }

        public void Execute()
        {
            // we won't need this in this implementation but could be used for
            // popup's before-open state. Analytics, breadcrumb etc.
            throw new NotImplementedException();
        }
    }
}
