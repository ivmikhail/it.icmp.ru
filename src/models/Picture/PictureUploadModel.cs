using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Validators;


namespace ITCommunity.Models {

    public class PictureUploadModel {

        [DisplayName("Выберите рисунок для загрузки")]
//        [PictureAllowedTypes]
        [PictureAllowedExtensions]
        [PictureMaxSize]
        public HttpPostedFileBase Picture { get; set; }

        public string Path { get; set; }

        public Dictionary<string, string> PictureTextareas { get; set; }

    }
}
