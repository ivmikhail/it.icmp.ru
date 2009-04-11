using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for VariantCaptcha
/// </summary>
public class VariantCaptcha {
    private readonly String _question;
    public String Question {
        get {
            return _question;
        }
    }

    private readonly List<String> _variants;
    public List<String> Variants {
        get {
            return _variants;
        }
    }

    private readonly int _rightAnswer;
    public int RightAnswer {
        get {
            return _rightAnswer;
        }
    }
    public VariantCaptcha(String question, List<String> variants, int rigthAnswer) {
        this._question = question;
        this._variants = variants;
        this._rightAnswer = rigthAnswer;
    }
}
