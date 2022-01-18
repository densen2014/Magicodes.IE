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

         [ExporterHeader(IsIgnore = true)]
        [DisplayName("IDD")]
        public int MiniOrderDetailId { get; set; }

 
        [ExporterHeader(DisplayName = "订单ID", IsIgnore = true)]
        public int MiniOrderId { get; set; }
 
        /// <summary>
        /// 结算金额
        /// </summary>
        [ExporterHeader(Format = "#,##0")]
        [DisplayName("小计")]
        public decimal Total { get; set; }


    }




}
