using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    /// <summary>
    /// 所有水力安全图形组件的接口
    /// </summary>
    public interface IShape
    {
        /// <summary>
        /// 元件功能注释二维码
        /// </summary>
        Image imageExplain { get; set; }

        /// <summary>
        /// 元件编号
        /// </summary>
        int DEVICE_I_N { get; set; }


        /// <summary>
        /// 元件名称(类型_编号)
        /// </summary>
        string BCNAME { get; set; }

        string BCNAME0 { get; set; }

        int NodeNoOne { get; set; }
        int NodeNoTwo { get; set; }
        int NodeNoThree { get; set; }
        int LastNodeNo { get; set; }

        double HeightOne { get; }
        double HeightTwo { get; }

        int UseHeightNum { get; }
        //EnumNodeNoNum UseNodeNoNum { get; }

        /// <summary>
        /// 设置起始节点编号
        /// </summary>
        /// <param name="nodeNo">开始节点编号</param>
        void SetStartNodeNo(int nodeNo);

        /// <summary>
        /// 元件编号
        /// </summary>
        //int ID { get; set; }
    }
}
