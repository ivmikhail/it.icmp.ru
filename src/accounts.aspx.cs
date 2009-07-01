using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using ITCommunity;


namespace ITCommunity
{

    public partial class Accounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDropDown();
            }
            FillUsersList();
        }

        private void FillUsersList()
        {
            admins.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.Admin);
            posters.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.Poster);
            banned.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.Banned);
        }

        private void FillDropDown()
        {
            DropDownListActions.Items.Add(new ListItem("сделать постером", "2"));
            DropDownListActions.Items.Add(new ListItem("сделать простым смертным", "3"));
            DropDownListActions.Items.Add(new ListItem("забанить", "4"));
        }
        protected void ModifyUser_Click(object sender, EventArgs e)
        {
            int int_role = Convert.ToInt32(DropDownListActions.SelectedValue);
            string message = "";
            ITCommunity.User.Roles newrole = (ITCommunity.User.Roles)Enum.ToObject(typeof(ITCommunity.User.Roles), int_role);
            ITCommunity.User mod_user = ITCommunity.User.GetByLogin(UserLogin.Text.Trim());

            if (mod_user.Id < 1)
            {
                message = "“акой пользователь не зарегистрирован.";
            } else if (mod_user.Role == ITCommunity.User.Roles.Admin)
            {
                message = "ƒанный пользователь админ. Ќад ним нельз€ производить никаких действий.";
            } else
            {
                mod_user.Role = newrole;
                mod_user.Update();
                FillUsersList();
                message = "»зменени€ вступили в силу.";
            }
            ModifyUserMessage.Text = message;
            
        }
    }
}
