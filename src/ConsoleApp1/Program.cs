using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using AME.Models.Entity;
using DinkToPdf;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Tests.Models.Export;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Extension;
using System.Dynamic;
using System.Reflection;
using Test.Magicodes.IE;
using AME.Services.Tools;
using ReceiptInfo = Magicodes.ExporterAndImporter.Tests.Models.Export.ReceiptInfo;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

await 导出DEMO.动态导出();
await 导出教材订购明细样表.导出();

var filePath = Path.Combine(Directory.GetCurrentDirectory(),   "ExportReceipt_Test"); 

List<MiniOrderDetailsLite> items = new List<MiniOrderDetailsLite>();

for (int i = 0; i < 10; i++)
{
    items.Add(new MiniOrderDetailsLite
    {
        MiniOrderDetailId = i,
        MiniOrderId = i,
        Total = i/100,
    });
}
List<MiniOrderDetailsLite2> items2 = new List<MiniOrderDetailsLite2>();

for (int i = 0; i < 10; i++)
{
    items2.Add(new MiniOrderDetailsLite2
    {
        MiniOrderDetailId = i,
        MiniOrderId = i,
        Total = i,
        Total2 = i*5,
    });
}
 DataTable dt = items.ToDataTable();

//var exporterPdf = new PdfExporter();
//var resultPdf = await exporterPdf.ExportListByTemplate(filePath + ".pdf", items);
var exporterXlsx = new ExcelExporter();
var resultXlsx = await exporterXlsx.Append(items)
                                    //.SeparateByColumn() //能显示列名
                                    .SeparateByRow() //不能显示列名
                                    //.SeparateBySheet() //能显示列名
                                    .Append(items2)
                                    .ExportAppendData(filePath + ".xlsx");

Console.ReadKey();

var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates",
    "receipt.cshtml");
var tpl = File.ReadAllText(tplPath);
var exporter = new PdfExporter();
if (File.Exists(filePath)) File.Delete(filePath);
//此处使用默认模板导出
var result = await exporter.ExportBytesByTemplate(
    new ReceiptInfo
    {
        Amount = 22939.43M,
        Grade = "2019秋",
        IdNo = "43062619890622xxxx",
        Name = "张三",
        Payee = "湖南心莱信息科技有限公司",
        PaymentMethod = "微信支付",
        Profession = "运动训练",
        Remark = "学费",
        TradeStatus = "已完成",
        TradeTime = DateTime.Now,
        UppercaseAmount = "贰万贰仟玖佰叁拾玖圆肆角叁分",
        Code = "19071800001"
    }, new PdfExporterAttribute(), tpl);

using (var file = File.OpenWrite(filePath))
{
    file.Write(result, 0, result.Length);
}


