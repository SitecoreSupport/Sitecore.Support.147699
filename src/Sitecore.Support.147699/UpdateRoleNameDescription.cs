using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Events.Hooks;
using Sitecore.SecurityModel;

namespace Sitecore.Support.Hooks
{
    public class UpdateRoleNameDescription : IHook
    {
        public void Initialize()
        {
            using (new SecurityDisabler())
            {
                var databaseName = "core";
                var itemPath = "/sitecore/system/Dictionary/T/The role name can only contain the following characters  A Z  a z  0 9 and space";
                var fieldName = "Phrase";
                var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                var fieldValue = $"The role name can only contain the following characters: A-Z, a-z, 0-9, space and underscore.";

                var database = Factory.GetDatabase(databaseName);
                var item = database.GetItem(itemPath);

                if (string.Equals(item[fieldName], fieldValue, StringComparison.Ordinal))
                {
                    // already installed
                    return;
                }

                Log.Info($"Installing {assemblyName}", this);
                item.Editing.BeginEdit();
                item[fieldName] = fieldValue;
                item.Editing.EndEdit();
            }
        }
    }
}