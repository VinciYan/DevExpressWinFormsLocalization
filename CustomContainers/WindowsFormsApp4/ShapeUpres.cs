using DevExpress.Utils.Serializing;
using DevExpress.Utils;
using DevExpress.XtraDiagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Drawing;

namespace WindowsFormsApp4
{
    /// <summary>
    /// 01上游水库 UPERS
    /// 单节点元件
    /// </summary>
    [Serializable]
    [XmlRoot("DEVICE")]
    public partial class ShapeUpres : DiagramImage, IShape
    {
        public ShapeUpres()
        {
            //显示图片内容
            //Image = Properties.ModelResouce.UPRES;
            //imageExplain = Properties.ModelResouce.二维码;

            #region 设置连接点
            List<PointFloat> points = new List<PointFloat>();
            points.Add(new PointFloat(0.5f, 1f));
            ConnectionPoints = new PointCollection(points);
            #endregion
            CanResize = false;
            //UperTHCurve = new List<UperTH>();//自定义水位，时间-水位曲线-菜单栏集中管理

        }

        public ShapeUpres(IContainer container)
        {
            container.Add(this);
        }

        public Image imageExplain { get; set; }

        public void SetStartNodeNo(int nodeNo)
        {
            int count = ConnectionPoints.Count;
            if (count == 1)
            {
                NodeNoOne = nodeNo;
                LastNodeNo = nodeNo;
            }
            else if (count == 2)
            {
                NodeNoOne = nodeNo;
                NodeNoTwo = nodeNo + 1;
                LastNodeNo = nodeNo + 1;
            }
            else if (count == 3)
            {
                NodeNoOne = nodeNo;
                NodeNoTwo = nodeNo + 1;
                NodeNoThree = nodeNo + 2;
                LastNodeNo = nodeNo + 2;
            }
        }

        //基础信息
        //[TypeConverter(typeof(NonEditablePropertyTypeConverter))] //无效
        [XtraSerializableProperty, Category("基础信息"), DisplayName("元件名称"), Browsable(false)/*, ReadOnly(true)*/]
        public string BCNAME { get; set; } = "上游水库";

        //开放给用户编辑名称的属性BCNAME0
        [XtraSerializableProperty, Category("基础信息"), DisplayName("元件名称"), Browsable(true)/*, ReadOnly(true)*/]
        public string BCNAME0 { get; set; } = "上游水库";

        [TypeConverter(typeof(NonEditablePropertyTypeConverter))]
        [XtraSerializableProperty, Category("基础信息"), DisplayName("元件编号"), Browsable(true)/*, ReadOnly(true)*/]
        //public int SerDEVICE_I_N { get { return DEVICE_I_N; } set { DEVICE_I_N = value; } }
        public int DEVICE_I_N { get; set; }

        [XtraSerializableProperty]
        public string BCTYPE { get; set; } = "UPRES";


        //public event PropertyChangedEventHandler PropertyChanged;
        #region 节点信息
        private int _NodeNoOne;
        [TypeConverter(typeof(NonEditablePropertyTypeConverter))]
        [XtraSerializableProperty, Category("节点信息"), DisplayName("元件节点编号"), Browsable(true)/*, ReadOnly(true)*/]
        public int NodeNoOne { get { return _NodeNoOne; } set { _NodeNoOne = value; } }

        public int NodeNoTwo { get { return _NodeNoOne; } set { _NodeNoOne = value; } }
        public int NodeNoThree { get; set; }
        public int LastNodeNo { get; set; }

        [XtraSerializableProperty, Category("节点信息"), DisplayName("元件节点高程(m)"), Browsable(true)]
        public double NODE_R_ELE1 { get; set; }
        public double HeightOne
        {
            get { return NODE_R_ELE1; }
        }

        public double HeightTwo
        {
            get { return NODE_R_ELE1; }
        }
        public int UseHeightNum
        {
            get { return 1; }
        }
        //public EnumNodeNoNum UseNodeNoNum => EnumNodeNoNum.NodeNoNumOne;

        [XtraSerializableProperty, Category("节点信息"), DisplayName("元件节点测压管水头(m)"), Browsable(true)]
        public double H0_1 { get; set; }
        #endregion


        #region 属性参数
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("输出控制"), Browsable(true)]
        //public OutPut OutYN { get; set; }

        //有压段属性参数9个
        //类型
        private UperType _STO_UP_TYPE;
        [XtraSerializableProperty, Category("属性参数"), DisplayName("有压段上游水库类型"), Browsable(true), Description("test")]
        //[TypeConverter(typeof(Frame.EnumConverter))]
        public UperType STO_UP_TYPE
        {
            get { return _STO_UP_TYPE; }
            set
            {
                _STO_UP_TYPE = value;
                SetPropertyVisibility();
            }
        }

        //[Range(0, double.MaxValue, ErrorMessage = "该参数范围应大于等于0")]
        //private double _STO_KF;
        [XtraSerializableProperty, Category("属性参数"), DisplayName("水库出口水头损失系数(/)"), Browsable(true)]
        public double STO_KF { get; set; }
        //{
        //    get { return _STO_KF; }
        //    set
        //    {
        //        if (value < 0)
        //        { XtraMessageBox.Show("正向流动水库出口水头损失系数大于等于0."); }
        //        else { _STO_KF = value; }
        //    }
        //}

        //[Range(0, double.MaxValue, ErrorMessage = "该参数范围应大于等于0")]
        [XtraSerializableProperty, Category("属性参数"), DisplayName("水库入口水头损失系数(/)"), Browsable(true)]
        public double STO_KFN { get; set; }

        #region 菜单栏中集中管理
        ////随STO_UP_TYPE变化的参数
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("上游水库恒定水位(m)"), Browsable(true)]
        //public double STO_UP_HR { get; set; }
        ////时间-上游水位关系曲线
        //private int _STO_UP_N;
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("时间-上游水库水位关系曲线离散点个数"), Browsable(false)]
        //public int STO_UP_N
        //{
        //    get { return _STO_UP_N; }
        //    set
        //    {
        //        _STO_UP_N = value;
        //        if (STO_UP_N != 0)
        //        {
        //            Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "UperTHCurve", true);
        //        }
        //    }
        //}

        //[XtraSerializableProperty, Category("属性参数"), DisplayName("时间-上游水库水位关系曲线"), Browsable(false)]
        //public List<UperTH> UperTHCurve { get; set; }
        ////时间-上游水位关系曲线

        //[XtraSerializableProperty, Category("属性参数"), DisplayName("变水头工况的水头波幅"), Browsable(false)]
        //public double STO_DH { get; set; }
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("变水头工况的圆频率"), Browsable(false)]
        //public double STO_W { get; set; }
        ////end 随STO_UP_TYPE变化的参数
        #endregion

        //无压段属性参数5个
        [XtraSerializableProperty, Category("属性参数"), DisplayName("正向流动水库出口水头损失系数(/)"), Browsable(false)]
        public double OPC_STO_KF { get; set; }
        [XtraSerializableProperty, Category("属性参数"), DisplayName("反向流动水库进口水头损失系数(/)"), Browsable(false)]
        public double OPC_STO_KFN { get; set; }

        #region 菜单栏中集中管理
        ////随OPC_RES_TYPE变化的参数
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("上游水库恒定流量"), Browsable(false)]
        //public double OPC_STO_QR { get; set; }
        //[XtraSerializableProperty, Category("属性参数"), DisplayName("上游水库恒定水位"), Browsable(false)]
        //public int OPC_STO_HR { get; set; }
        #endregion
        //类型
        private FlowType _STO_UP_TYPE1;
        [XtraSerializableProperty, Category("属性参数"), DisplayName("管段水流状态"), Browsable(true)]
        //[TypeConverter(typeof(Frame.EnumConverter))]
        public FlowType STO_UP_TYPE1
        {
            get { return _STO_UP_TYPE1; }
            set
            {
                _STO_UP_TYPE1 = value;
                SetPropertyVisibility();
            }
        }
        //类型
        private UperType0 _OPC_RES_TYPE;
        [XtraSerializableProperty, Category("属性参数"), DisplayName("无压段上游水库边界类型"), Browsable(false)]
        //[TypeConverter(typeof(Frame.EnumConverter))]
        public UperType0 OPC_RES_TYPE { get { return _OPC_RES_TYPE; } set { _OPC_RES_TYPE = value; SetPropertyVisibility(); } }
        #endregion

        //属性组合可视化
        void SetPropertyVisibility()
        {
            if (STO_UP_TYPE1 == FlowType.Press) //有压段
            {
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_TYPE", true);  //有压段上游水库类型
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_RES_TYPE", false);    //无压段上游水库边界类型
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_KF", true);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_KFN", true);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_KF", false);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_KFN", false);
                #region 菜单栏集中管理
                if (STO_UP_TYPE == UperType.Const)  //上游水库类型（有压段）恒定水位
                {
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_HR", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_N", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_X0", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_Y0", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "UperTHCurve", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_DH", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_W", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_HR", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_QR", false);
                }
                else if (STO_UP_TYPE == UperType.Custom)//自定义水位
                {
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_HR", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_N", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_X0", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_Y0", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_DH", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_W", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_HR", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_QR", false);
                }
                else if (STO_UP_TYPE == UperType.Sin)//正弦波动水位
                {
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_HR", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_N", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_X0", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_Y0", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "UperTHCurve", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_DH", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_W", true);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_HR", false);
                    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_QR", false);
                }
                #endregion
            }
            else if (STO_UP_TYPE1 == FlowType.Nopress) //无压段
            {
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_TYPE", false);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_RES_TYPE", true);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_KF", false);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_KFN", false);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_KF", true);
                //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_KFN", true);
                #region 菜单栏集中管理
                //if (OPC_RES_TYPE == UperType0.Hbound)//水头边界
                //{
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_HR", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_N", false);
                //    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_X0", false);
                //    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_Y0", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "UperTHCurve", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_DH", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_W", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_HR", true);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_QR", false);
                //}
                //else if (OPC_RES_TYPE == UperType0.Tbound)//流量边界
                //{
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_HR", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_UP_N", false);
                //    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_X0", false);
                //    //Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_HR_Y0", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "UperTHCurve", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_DH", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "STO_W", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_HR", false);
                //    Jr.Frame.PropertyGridSet.SetPropertyVisibility(this, "OPC_STO_QR", true);
                //}
                #endregion
            }
        }

        #region 汉化属性
        [XtraSerializableProperty, Category("尺寸和位置"), DisplayName("X轴"), Browsable(true)/*, ReadOnly(true)*/]
        public float X { get; set; }
        [XtraSerializableProperty, Category("尺寸和位置"), DisplayName("Y轴"), Browsable(true)/*, ReadOnly(true)*/]
        public float Y { get; set; }
        [XtraSerializableProperty, Category("尺寸和位置"), DisplayName("宽度"), Browsable(true)/*, ReadOnly(true)*/]
        public float Width { get; set; }
        [XtraSerializableProperty, Category("尺寸和位置"), DisplayName("高度"), Browsable(true)/*, ReadOnly(true)*/]
        public float Height { get; set; }
        [XtraSerializableProperty, Category("尺寸和位置"), DisplayName("角度"), Browsable(true)/*, ReadOnly(true)*/]
        public float NewAngle { get; set; }
        #endregion

        public static void EditProperty(DiagramCustomGetEditableItemPropertiesEventArgs arg)
        {
            //SetNewPro(arg);//汉化
            FilterPro(arg);//隐藏

            //基础信息
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["BCNAME0"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["DEVICE_I_N"]);
            //节点
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["NodeNoOne"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["NODE_R_ELE1"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["H0_1"]);
            //有压
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_UP_TYPE1"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_UP_TYPE"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_KF"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_KFN"]);
            #region 菜单栏集中管理
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_UP_HR"]);
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_UP_N"]);
            ////arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpers))["STO_HR_X0"]);
            ////arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpers))["STO_HR_Y0"]);
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["UperTHCurve"]);
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_DH"]);
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["STO_W"]);
            #endregion
            //无压
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["OPC_RES_TYPE"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["OPC_STO_KF"]);
            arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["OPC_STO_KFN"]);
            #region 菜单栏集中管理
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["OPC_STO_QR"]);
            //arg.Properties.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["OPC_STO_HR"]);
            #endregion

        }

        public static (List<object>, List<object>) GetNewPro(DiagramCustomGetEditableItemPropertiesEventArgs arg)
        {
            var item = arg.Item as ShapeUpres;

            List<object> addObjs = new List<object>();//自定义汉化属性
            List<object> reObjs = new List<object>();//旧画布元件自带属性
            for (int i = 0; i < arg.Properties.Count; i++)
            {
                var eaArg = arg.Properties[i];
                if (eaArg.Category.StartsWith("Size") && eaArg.DisplayName == "X")
                {
                    item.X = item.Position.X;
                    reObjs.Add(eaArg);
                    addObjs.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["X"]);
                }
                if (eaArg.Category.StartsWith("Size") && eaArg.DisplayName == "Y")
                {
                    item.Y = item.Position.Y;
                    reObjs.Add(eaArg);
                    addObjs.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["Y"]);
                }
                if (eaArg.Category.StartsWith("Size") && eaArg.DisplayName == "Angle")
                {
                    item.NewAngle = item.Angle;
                    reObjs.Add(eaArg);
                    addObjs.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["NewAngle"]);
                }
                if (eaArg.Category.StartsWith("Size") && eaArg.DisplayName == "Width")
                {
                    item.Width = item.Size.Width;
                    reObjs.Add(eaArg);
                    addObjs.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["Width"]);

                }
                if (eaArg.Category.StartsWith("Size") && eaArg.DisplayName == "Height")
                {
                    item.Height = item.Size.Height;
                    reObjs.Add(eaArg);
                    addObjs.Add(TypeDescriptor.GetProperties(typeof(ShapeUpres))["Height"]);
                }
            }
            return (addObjs, reObjs);
        }

        public static void SetNewPro(DiagramCustomGetEditableItemPropertiesEventArgs arg)
        {
            var item = arg.Item as ShapeUpres;

            var res = GetNewPro(arg);

            foreach (var ea in res.Item1)
            {
                arg.Properties.Add(ea as PropertyDescriptor);//添加汉化属性
            }
            foreach (var ea in res.Item2)
            {
                arg.Properties.Remove(ea as PropertyDescriptor);//移除旧属性
            }
        }

        public static List<object> GetFilterRem(DiagramCustomGetEditableItemPropertiesEventArgs arg)
        {
            List<object> reObjs = new List<object>();//需被移除的属性
            for (int i = 0; i < arg.Properties.Count; i++)
            {
                var eaArg = arg.Properties[i];
                if (eaArg.Category.StartsWith("行为") && eaArg.DisplayName == "Flip")
                {
                    reObjs.Add(eaArg);
                }
                else if (eaArg.Category.StartsWith("行为") && eaArg.DisplayName == "Stretch")
                {
                    reObjs.Add(eaArg);
                }
                else if (eaArg.Category.StartsWith("外观") && eaArg.DisplayName == "Stroke Style")
                {
                    reObjs.Add(eaArg);
                }
                else if (eaArg.Category.StartsWith("外观") && eaArg.DisplayName == "Stroke Thickness")
                {
                    reObjs.Add(eaArg);
                }
            }
            return reObjs;
        }

        public static void FilterPro(DiagramCustomGetEditableItemPropertiesEventArgs arg)
        {
            var res = GetFilterRem(arg);

            foreach (var ea in res)
            {
                arg.Properties.Remove(ea as PropertyDescriptor);
            }
        }

    }

    #region 菜单栏集中管理
    /// <summary>
    /// 时间-上游水库水位关系曲线
    /// </summary>
    [Serializable]
    public class UperTH
    {
        [XtraSerializableProperty, Category("时间-上游水库水位关系曲线"), DisplayName("横坐标-时间(s)")]
        public double STO_HR_X0 { get; set; }
        [XtraSerializableProperty, Category("时间-上游水库水位关系曲线"), DisplayName("纵坐标-水库水位(m)")]
        public double STO_HR_Y0 { get; set; }
        public override string ToString()
        {
            return "曲线离散点";
        }
    }
    #endregion

    /// <summary>
    /// 上游水库类型（有压段）
    /// </summary>
    public enum UperType
    {
        //[XmlEnum(Name = "0")]
        //[Description("")]
        //None = 0,
        //[XmlEnum(Name = "1")]
        //[Description("恒定水位")]
        //Const = 1,
        //[XmlEnum(Name = "2")]
        //[Description("自定义水位")]
        //Custom = 2,
        //[XmlEnum(Name = "3")]
        //[Description("正弦波动水位")]
        //Sin = 3

        [XmlEnum(Name = "1")]
        [Description("恒定水位")]
        Const = 0,
        [XmlEnum(Name = "2")]
        [Description("自定义水位")]
        Custom = 1,
        [XmlEnum(Name = "3")]
        [Description("正弦波动水位")]
        Sin = 2
    }

    /// <summary>
    /// 上游水库类型（无压段）
    /// </summary>
    public enum UperType0
    {
        //[XmlEnum(Name = "-1")]
        //[Description("")]
        //None = 0,
        //[XmlEnum(Name = "0")]
        //[Description("水头边界")]
        //Hbound = 1,
        //[XmlEnum(Name = "1")]
        //[Description("流量边界")]
        //Tbound = 2,

        [XmlEnum(Name = "0")]
        [Description("水头边界")]
        Hbound = 0,
        [XmlEnum(Name = "1")]
        [Description("流量边界")]
        Tbound = 1
    }

    /// <summary>
    /// 管段水流状态
    /// </summary>
    public enum FlowType
    {
        //[XmlEnum(Name = "0")]
        //[Description("")]
        //None = 0,
        //[XmlEnum(Name = "0")]
        //[Description("有压段")]
        //Press = 1,
        //[XmlEnum(Name = "1")]
        //[Description("无压段")]
        //Nopress = 2,

        [XmlEnum(Name = "0")]
        [Description("有压段")]
        Press = 0,
        [XmlEnum(Name = "1")]
        [Description("无压段")]
        Nopress = 1
    }


    /// <summary>
    /// 自定义TypeConverter,用户尝试编辑属性，返回原始值
    /// </summary>
    public class NonEditablePropertyTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return GetOriginalValue(context);
        }

        private object GetOriginalValue(ITypeDescriptorContext context)
        {
            return context.PropertyDescriptor.GetValue(context.Instance);
        }
    }
}
