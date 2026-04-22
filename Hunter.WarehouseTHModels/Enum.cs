using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.WarehouseTHModels
{
    /// <summary>
    /// 窗体枚举，临界窗体上为固定窗体
    /// </summary>
    public enum FormNames
    {
        集中监控,
        实时趋势,
        // ----上为固定窗体----
        临界窗体,
        参数配置,
        历史趋势,
        报警记录,
        数据报表,
        用户管理

    }
}
