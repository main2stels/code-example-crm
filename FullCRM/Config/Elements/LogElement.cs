using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Config.Elements
{
    public class LogElement
    {
        /// <summary>
        /// Строка подключения к логированию в монге
        /// </summary>
        public string Mongo { get; set; }
    }
}
