//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;

namespace BankBranchWorkBench.MAUI.Common
{
    public static class MAUIUtilities
    {
        #region Windows APIs

        /// <summary>
        /// Get the window with the supplied caption
        /// </summary>
        /// <returns>The window with the supplied caption</returns>
        public static Window GetWindow(App application, string caption)
        {
            return GetWindow(application, caption, true);
        }

        public static Maui.Core.Window GetWindow(App application, string caption, bool throwWhenWindowNotFound)
        {
            Window window = null;

            try
            {
                window = new Maui.Core.Window(caption, StringMatchSyntax.ExactMatch, "WindowsForms10.Window.8.*", StringMatchSyntax.WildCard, application, 5000);
            }
            catch (Window.Exceptions.WindowNotFoundException ex)
            {

                window = null;

                if (throwWhenWindowNotFound)
                {
                    throw new WindowNotFoundException(String.Format("{0} Screen not Found", caption), ex);
                }
            }

            return window;
        }

        public static T GetControl<T>(Window window, string controlName)
            where T : Control
        {
            return (T) Activator.CreateInstance(typeof(T), window, controlName);    
        }

        public static T GetControl<T>(Window window, string caption, StringMatchSyntax captionMatchSyntax, 
            string className, StringMatchSyntax classNameMatchSyntax)
            where T : Control
        {
            return (T)Activator.CreateInstance(typeof(T), window, caption, captionMatchSyntax, 
                className, classNameMatchSyntax);
        }

        /// <summary>
        /// Checks if the supplied window is a modal window
        /// </summary>
        /// <returns>True, if modal window. False otherwise</returns>
        public static bool IsModal(App application, Window window)
        {
            return IsModal(new Dialog(application, window)); 
        }

        /// <summary>
        /// Checks if the supplied window is a modal window
        /// </summary>
        /// <returns>True, if modal window. False otherwise</returns>
        public static bool IsModal(Dialog dialog)
        {
            return dialog.IsModal();
        }

        #endregion

        #region Menu APIs

        /// <summary>
        /// Clicks the menu item in the specified heirarchy
        /// </summary>
        /// <param name="menuHeirarchy">Menu heirarchy to be clicked</param>
        public static void ClickMenu(Menu menuStrip, params string[] menuHeirarchy)
        {
            StringCollection clickMenu = new StringCollection();

            for (int count = 0; count < menuHeirarchy.Length; count++)
            {
                clickMenu.Add(menuHeirarchy[count]);
            }
            menuStrip.ExecuteMenuItem(clickMenu);
        }

        /// <summary>
        /// To get the menu item heirarchy for the given window
        /// </summary>
        /// <returns>Dictionary of menu heirarchy in the supplied window</returns>
        public static Dictionary<string, StringCollection> GetMenuItems(Maui.Core.Window fromWindow)
        {
            Dictionary<string, StringCollection> menuDictionary = new Dictionary<string, StringCollection>();
            string strRootMenu = string.Empty;
            StringCollection strMenuItems;

            Menu appMenu = new Menu(fromWindow);

            for (int i = 0; i < appMenu.MenuItems.Count; i++)
            {
                strRootMenu = appMenu.MenuItems[i].Text;
                strMenuItems = new StringCollection();

                for (int j = 0; j < appMenu.MenuItems[i].MenuItems.Count; j++)
                {
                    strMenuItems.Add(appMenu.MenuItems[i].MenuItems[j].Text);
                }

                menuDictionary.Add(strRootMenu, strMenuItems);
            }
            return menuDictionary;
        }

        /// <summary>
        /// To get the ToolStripItems for the given window
        /// </summary>
        /// <returns>StringCollection of ToolStripItem names in the supplied window</returns>
        public static StringCollection GetToolStripItems(Maui.Core.Window fromWindow)
        {
            StringCollection strToostripItems = new StringCollection();
            ToolStrip toolstrip = new ToolStrip(fromWindow);

            for (int i = 0; i < toolstrip.ToolStripItems.Count; i++)
            {
                strToostripItems.Add(toolstrip.ToolStripItems[i].Text);
            }

            return strToostripItems;
        }

        #endregion

    }
}
