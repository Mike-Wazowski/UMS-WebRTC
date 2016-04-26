using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web.ViewModels
{
    public class BaseViewModel
    {
        public ToastrType ToastrType { get; set; }
        public string MessageTitle { get; set; }
        public string Message { get; set; }
        public BaseViewModel() { }
        public BaseViewModel(ToastrType toastrType, string messageTitle, string message)
        {
            ShowMessage(toastrType, messageTitle, message);
        }

        public void ShowMessage(ToastrType toastrType, string messageTitle, string message)
        {
            ToastrType = toastrType;
            MessageTitle = messageTitle;
            Message = message;
        }
    }
}