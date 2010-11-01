using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Modules;


namespace ITCommunity.Models {

    public class WinUpdatesListModel {

        [DisplayName("Начало имени файла")]
        [Required(ErrorMessage = "Задайте начало имени файла обновления")]
        public string Start {
            get; set;
        }
        [DisplayName("1-й аргумент")]
        [Required(ErrorMessage = "Введите 1-й аргумент")]
        public string Q1 {
            get;
            private set;
        }
        [DisplayName("2-й аргумент")]
        public string Q2 {
            get;
            private set;
        }

        public List<WsusFile> List {
            get;
            private set;
        }

        public WinUpdatesListModel() {
            Start = "";
            Q1 = "";
            Q2 = "";
            List = null;
        }

        public WinUpdatesListModel(string start, string q1, string q2) {
            if (start != string.Empty) {
                List = Wsus.search(start, q1, q2);  
            }
            Start = start;
            Q1 = q1;
            Q2 = q2;
        }
    }
}
