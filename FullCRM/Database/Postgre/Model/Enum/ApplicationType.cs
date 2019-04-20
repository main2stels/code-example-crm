using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Database.Postgre.Model.Enum
{
    /// <summary>
    /// Тип приложения
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Vox
        /// </summary>
        Vox = 0,

        /// <summary>
        /// Hybrid
        /// </summary>
        Hybrid = 1,

        /// <summary>
        /// Alliance
        /// </summary>
        [Obsolete]
        Alliance = 2,

        /// <summary>
        /// HybridCrm
        /// </summary>
        HybridCrm = 4,

        /// <summary>
        /// VoxCrm
        /// </summary>
        VoxCrm = 5,

        /// <summary>
        /// TargetixApi
        /// </summary>
        [Obsolete]
        TargetixApi = 6,

        /// <summary>
        /// HybridClientReports
        /// </summary>
        HybridClientReports = 7,

        /// <summary>
        /// Hybrid insights
        /// </summary>
        Insights = 8,

        /// <summary>
        /// FullCrm
        /// </summary>
        FullCrm = 9
    }
}
