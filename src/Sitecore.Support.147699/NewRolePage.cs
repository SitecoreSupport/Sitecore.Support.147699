using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Sitecore.Controls;
using Sitecore.Diagnostics;
using Sitecore.localhost;
using Sitecore.StringExtensions;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;

namespace Sitecore.Support.Shell.Applications.Security.RoleManager

{
    public class NewRolePage : DialogPage
    {

        protected DropDownList Domain;
        protected TextBox Name;

        protected override void OK_Click()
        {
            string text = this.Name.Text;
            if (!Regex.IsMatch(text, @"^\w[\w\s]*$"))
            {
                SheerResponse.Alert("The role name \"{0}\" contains illegal characters.\n\nThe role name can only contain the following characters: A-Z, a-z, 0-9, space and underscore.", new string[] { text });
            }
            else
            {
                SheerResponse.SetDialogValue(@"{0}\{1}".FormatWith(new object[] { this.Domain.SelectedValue, text }));
                base.OK_Click();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            Assert.ArgumentNotNull(e, "e");
            Assert.CanRunApplication("/sitecore/content/Applications/Security/Role Manager");
            base.OnLoad(e);
            if (!XamlControl.AjaxScriptManager.IsEvent)
            {
                foreach (Sitecore.Security.Domains.Domain domain in Sitecore.Context.User.Delegation.GetManagedDomains())
                {
                    ListItem item = new ListItem(domain.Name, domain.Name);
                    this.Domain.Items.Add(item);
                    if (domain.Name.Equals("sitecore", StringComparison.InvariantCultureIgnoreCase))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
    }
}