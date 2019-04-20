using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Models.Document
{
    public enum DocumentFormat
    {
        [Description("Pdf")]
        Pdf = 1,

        [Description("Docx")]
        Docx = 2
    }
}
