﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yumiki.Business.Base;
using Yumiki.Business.Master.Interfaces;
using Yumiki.Commons.Dictionaries;
using Yumiki.Commons.Exceptions;
using Yumiki.Data.Master.Interfaces;
using Yumiki.Entity.Master;

namespace Yumiki.Business.Master.Services
{
    public class GUIService : BaseService<IGUIRepository>, IGUIService
    {
        /// <summary>
        /// Get all privileges (menus) from database and show to UI.
        /// </summary>
        /// <param name="userId">Menu list from this specific user.</param>
        /// <returns>List of privilege in HTML Format</returns>
        public string GetPrivilege(string userID)
        {
            Guid convertedUserID = CheckandConvertUserID(userID);
            List<VW_Privilege> privileges = Repository.GetPrivilege(convertedUserID);

            IEnumerable<VW_Privilege> roots = privileges.Where(c => !c.ParentPrivilegeID.HasValue);

            StringBuilder menu = new StringBuilder();
            foreach (VW_Privilege root in roots)
            {
                menu.Append(ScanChildrenNodes(root, privileges));
            }

            return menu.ToString();
        }

        /// <summary>
        /// Scan all children of Menu Parent node
        /// </summary>
        /// <param name="node">Parent need to be scanned to get all child.</param>
        /// <param name="privileges">The list must contain all child of Parent node.</param>
        /// <returns>HTML Format for all child.</returns>
        private string ScanChildrenNodes(VW_Privilege node, List<VW_Privilege> privileges)
        {
            StringBuilder menu = new StringBuilder();

            string path = CommonValues.HashTag.ToString();
            //If the page is Displayable, add a URL to path. Otherwise, user cannot navigate to actual page.
            if (node.IsDisplayable)
            {
                path = node.PagePath;

                //URL Path must be "/ApplicationName/PageName/[Action]", has 'slash' char at the first of path.
                if (!path.Equals(CommonValues.HashTag) && !node.PagePath.First().Equals('/'))
                {
                    path = string.Format("/{0}", path);
                }
                //Append URL Prefix such as "/Apps/ApplicationName/PageName/[Action]", prefix is mandatory for URL Routing.
                string urlPrefix = string.Format("/{0}", HttpConstants.RouteNames.WebFormAreaPrefix);
                if (!path.ToLowerInvariant().StartsWith(urlPrefix.ToLowerInvariant()))
                {
                    path = string.Format("{0}{1}", urlPrefix, path);
                }
            }

            IEnumerable<VW_Privilege> children = privileges.Where(c => c.ParentPrivilegeID == node.ID);

            //Bootstrap 4 have the difference of class name for first menu and sub-menu.
            //Render menu level 2...n
            if (node.ParentPrivilegeID.HasValue)
            {
                if (children.Count() == 0)
                {
                    menu.AppendFormat("<li><a class='dropdown-item' href='{0}'>{1}</a></li>", path, node.PrivilegeName);
                }
                else
                {
                    menu.Append("<li>");
                    menu.AppendFormat("<a class='dropdown-item dropdown-toggle' href='{0}'>{1}</a>", path, node.PrivilegeName);
                    menu.Append("<ul class='dropdown-menu'>");

                    foreach (VW_Privilege child in children)
                    {
                        menu.Append(ScanChildrenNodes(child, privileges));
                    }

                    menu.Append("</ul></li>");
                }
            }
            //Render root items
            else
            {
                if (children.Count() == 0)
                {
                    menu.AppendFormat("<li class='nav-item'><a class='nav-link' href='{0}'>{1}</a></li>", path, node.PrivilegeName);
                }
                else
                {
                    menu.Append("<li class='nav-item dropdown'>");
                    menu.AppendFormat("<a class='nav-link dropdown-toggle' href='{0}' id='dropdown-menu-{1}' data-toggle='dropdown'>{2}</a>", path, node.ID, node.PrivilegeName);
                    menu.AppendFormat("<ul class='dropdown-menu' aria-labelledby='dropdown-menu-{0}'>", node.ID);

                    foreach (VW_Privilege child in children)
                    {
                        menu.Append(ScanChildrenNodes(child, privileges));
                    }

                    menu.Append("</ul></li>");
                }
            }

            return menu.ToString();
        }

        /// <summary>
        /// Check validation for the userID before convert it to GUID and then convert it.
        /// </summary>
        /// <param name="userID">User ID need to check and convert.</param>
        /// <returns>A converted GUID User ID.</returns>
        private Guid CheckandConvertUserID(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                throw new YumikiException(ExceptionCode.E_EMPTY_VALUE, "User ID cannot be empty.", Logger);
            }

            Guid convertedID = Guid.Empty;
            Guid.TryParse(userID, out convertedID);
            if (convertedID == Guid.Empty)
            {
                throw new YumikiException(ExceptionCode.E_WRONG_TYPE, "User ID must be GUID type.", Logger);
            }

            return convertedID;
        }
    }
}
