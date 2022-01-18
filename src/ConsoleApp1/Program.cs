﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AME.Models.Entity;
using DinkToPdf;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Tests.Models.Export;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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

var exporterPdf = new PdfExporter();
var resultPdf = await exporterPdf.ExportListByTemplate(filePath + ".pdf", items);
var exporterXlsx = new ExcelExporter();
var resultXlsx = await exporterXlsx.Export(filePath + ".xlsx", items);

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

 