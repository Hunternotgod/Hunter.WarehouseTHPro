using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hunter.WarehouseTHModels;

namespace Hunter.WarehouseTHPro
{
    /// <summary>
    /// 主窗体 - 仓储温湿度监控系统主界面
    /// </summary>
    public partial class MainView : Form
    {
        /// <summary>
        /// 构造函数 - 初始化主窗体组件
        /// </summary>
        public MainView()
        {
            InitializeComponent();
        }

        #region 字段定义

        /// <summary>
        /// 导航按钮选中状态的背景颜色
        /// </summary>
        private Color selectedColor = Color.FromArgb(1, 106, 189);

        /// <summary>
        /// 导航按钮未选中状态的背景颜色
        /// </summary>
        private Color unSelectedColor = Color.FromArgb(43, 50, 120);

        #endregion

        #region 窗体管理方法

        /// <summary>
        /// 打开或切换子窗体
        /// </summary>
        /// <param name="formNames">要打开的窗体名称枚举</param>
        /// <returns>操作是否成功</returns>
        private bool OpenForm(FormNames formNames)
        {
            // todo 预留权限判断的接口 - 后续可以在此添加用户权限验证逻辑

            // 获取主面板中控件总数
            int total = this.MainPanel.Controls.Count;
            // 已关闭的窗体数量（用于修正索引）
            int closeCount = 0;
            // 标记是否找到目标窗体
            bool isFind = false;

            // 遍历主面板中的所有控件
            for (int i = 0; i < total; i++)
            {
                // 获取当前控件（索引需要减去已关闭的窗体数量）
                Control ct = this.MainPanel.Controls[i - closeCount];

                // 判断是否是窗体类型
                if (ct is Form frm)
                {
                    // 判断是否是目标窗体
                    // 如果是目标窗体，则置顶显示
                    if (frm.Text == formNames.ToString())
                    {
                        // 将窗体置顶显示
                        frm.BringToFront();
                        // 标记已找到目标窗体
                        isFind = true;
                        // 退出循环
                        break;
                    }
                    // 如果不是目标窗体，则判断是否为可关闭的窗体
                    // 临界窗体及以上的窗体可以被自动关闭
                    else if ((FormNames)Enum.Parse(typeof(FormNames), frm.Text) >= FormNames.临界窗体)
                    {
                        // 关闭该窗体
                        frm.Close();
                        // 增加已关闭窗体计数
                        closeCount++;
                    }
                }
            }

            // 如果未找到目标窗体，则创建新窗体
            if (isFind == false)
            {
                Form form = null;

                // 根据窗体名称枚举创建对应的窗体实例
                switch (formNames)
                {
                    case FormNames.集中监控:
                        form = new MonitorView();
                        break;
                    case FormNames.实时趋势:
                        form = new TrendView();
                        break;
                    case FormNames.参数配置:
                        form = new ConfigView();
                        break;
                    case FormNames.历史趋势:
                        form = new HistoryView();
                        break;
                    case FormNames.报警记录:
                        form = new AlarmView();
                        break;
                    case FormNames.数据报表:
                        form = new RecordView();
                        break;
                    case FormNames.用户管理:
                        form = new UserView();
                        break;
                    default:
                        break;
                }

                // 如果窗体创建成功，则配置并显示
                if (form != null)
                {
                    // 设置为非顶层窗体（作为子控件嵌入）
                    form.TopLevel = false;
                    // 设置为无边框样式
                    form.FormBorderStyle = FormBorderStyle.None;
                    // 设置Dock属性为Fill，填充整个父容器
                    form.Dock = DockStyle.Fill;
                    // 设置父容器为主面板
                    form.Parent = this.MainPanel;
                    // 将窗体置顶
                    form.BringToFront();
                    // 显示窗体
                    form.Show();
                }
            }

            // 返回操作成功
            return true;
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 导航按钮点击事件 - 处理窗体切换
        /// </summary>
        /// <param name="sender">事件触发源（按钮）</param>
        /// <param name="e">事件参数</param>
        private void CommnNavi_Click(object sender, EventArgs e)
        {
            // 判断触发源是否为按钮
            if (sender is Button btn)
            {
                // 验证按钮文本是否为有效的窗体名称枚举
                if (Enum.IsDefined(typeof(FormNames), btn.Text))
                {
                    // 将按钮的Text转换为窗体名称枚举类型
                    FormNames formNames = (FormNames)Enum.Parse(typeof(FormNames), btn.Text);

                    // 打开对应窗体，如果成功则更新导航按钮样式
                    if (OpenForm(formNames))
                    {
                        // 设置导航按钮的选中状态
                        SetNaviButton(formNames);
                    }
                }
            }
        }

        #endregion

        #region UI样式方法

        /// <summary>
        /// 设置导航按钮的选中状态样式
        /// </summary>
        /// <param name="formNames">当前选中的窗体名称枚举</param>
        private void SetNaviButton(FormNames formNames)
        {
            // 遍历导航面板中的所有按钮控件
            foreach (var item in this.NavPanel.Controls.OfType<Button>())
            {
                // 验证按钮文本是否为有效的窗体名称枚举
                if (Enum.IsDefined(typeof(FormNames), item.Text))
                {
                    // 将按钮的Text转换为窗体名称枚举类型
                    FormNames buttonName = (FormNames)Enum.Parse(typeof(FormNames), item.Text);

                    // 判断是否为当前选中的窗体对应的按钮
                    if (buttonName == formNames)
                    {
                        // 设置按钮的背景色为选中颜色
                        item.BackColor = this.selectedColor;
                    }
                    else
                    {
                        // 设置按钮的背景色为未选中颜色
                        item.BackColor = this.unSelectedColor;
                    }
                }
            }
        }

        #endregion
    }
}

/*** 
 * 用户点击导航按钮
    ↓
CommnNavi_Click 触发
    ↓
OpenForm 打开/切换窗体
    ↓
SetNaviButton 更新按钮样式
    ↓
窗体显示在 MainPanel 中

 */

