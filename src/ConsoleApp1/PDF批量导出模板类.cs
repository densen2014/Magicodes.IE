// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

#if NET461
#else
using DinkToPdf;
#endif
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AME.Services.Tools
{
    /// <summary>
    ///     批量导出Dto
    /// </summary>
#if NET461
#else
    [PdfExporter(Orientation = Orientation.Portrait, PaperKind = PaperKind.A5)]
#endif
    public class BatchReceiptInfoDto
    {
        /// <summary>
        ///     交易时间
        /// </summary>
        public DateTime TradeTime { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     身份证
        /// </summary>
        public string IdNo { get; set; }

        /// <summary>
        ///     金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     支付方式
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        ///     交易状态
        /// </summary>
        public string TradeStatus { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     年级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        ///     专业
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        ///     大写金额
        /// </summary>
        public string UppercaseAmount { get; set; }

        /// <summary>
        ///     编号
        /// </summary>
        public string Code { get; set; }


        public async Task BathExportReceipt()
        {
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates",
                "PDF批量导出模板.cshtml");
            var tpl = File.ReadAllText(tplPath);
            var exporter = new PdfExporter();

            var input = new BatchReceiptInfoInput
            {
                Payee = "湖南心莱信息科技有限公司",
                ReceiptInfoInputs = new List<BatchReceiptInfoDto>()
            };

            for (var i = 0; i < 20; i++)
                input.ReceiptInfoInputs.Add(new BatchReceiptInfoDto
                {
                    Amount = 22939.43M,
                    Grade = "2019秋",
                    IdNo = "43062619890622xxxx",
                    Name = "张三",
                    PaymentMethod = "微信支付",
                    Profession = "运动训练",
                    Remark = "学费",
                    TradeStatus = "已完成",
                    TradeTime = DateTime.Now,
                    UppercaseAmount = "贰万贰仟玖佰叁拾玖圆肆角叁分",
                    Code = "1907180000" + i
                });

            //此处使用默认模板导出
            var result = await exporter.ExportByTemplate("test.pdf", input, tpl);
        }
    }

    internal class BatchReceiptInfoInput
    {
        public string Payee { get; set; }
        public List<BatchReceiptInfoDto> ReceiptInfoInputs { get; set; }
    }
}
