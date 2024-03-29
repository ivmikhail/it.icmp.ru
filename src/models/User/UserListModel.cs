﻿using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class UserListModel : PaginatedModel {

        private string _showRole;

        public List<User> List {
            get;
            private set;
        }

        public UserListModel(string role, int? page)
            : base(page) {
            PerPage = Config.UsersPerPage;
            _showRole = role;
            List = GetUsers();
        }

        protected virtual List<User> GetUsers() {
            User.Roles role;

            if (Enum.TryParse(_showRole, true, out role)) {
                return Users.GetPagedByRole(role, Page, PerPage, ref TotalCount);
            }

            return Users.GetPaged(Page, PerPage, ref TotalCount);
        }
    }
}
