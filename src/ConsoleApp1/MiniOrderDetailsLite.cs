using System.ComponentModel;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Core;
using OfficeOpenXml.Table;

namespace AME.Models.Entity
{

    /// <summary>
    /// 小程序订单项Lite
    /// </summary>
    [ExcelExporter(Name = "小程序订单", TableStyle = TableStyles.Light10, AutoFitAllColumn = true)]
    public partial class MiniOrderDetailsLite 
    {

        [ExporterHeader(IsIgnore = false)]
        [DisplayName("IDD")]
        public int MiniOrderDetailId { get; set; }

 
        [ExporterHeader(DisplayName = "订单ID", IsIgnore = false)]
        public int MiniOrderId { get; set; }
 
        /// <summary>
        /// 结算金额
        /// </summary>
        [ExporterHeader(Format = "#,##0")]
        [DisplayName("小计")]
        public decimal Total { get; set; }


    }

    /// <summary>
    /// 小程序订单项Lite2
    /// </summary>
    [ExcelExporter(Name = "小程序订单2", TableStyle = TableStyles.Light10, AutoFitAllColumn = true)]
    public partial class MiniOrderDetailsLite2: MiniOrderDetailsLite
    {
        /// <summary>
        /// 结算金额2
        /// </summary>
        [ExporterHeader(Format = "#,##0")]
        [DisplayName("小计2")]
        public decimal Total2 { get; set; }
    }


}
