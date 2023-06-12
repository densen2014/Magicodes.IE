// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Html;
using Magicodes.ExporterAndImporter.Pdf;
using Magicodes.ExporterAndImporter.Word;
using System;
using System.Collections.Generic;
using System.IO;
using Task = System.Threading.Tasks.Task;


public partial class Program
{

    public partial class Foo
    {
        public decimal UnitPrice { get; set; } = 1;
        public decimal? UnitPrice2 { get; set; } = null;

    }

    public static async Task 测试IE()
    {

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "xxxx");
        var items = new List<Foo>() { new Foo() };

        var exporter = new ExcelExporter();
        var result = await exporter.Export(filePath + ".xlsx", items);
        Console.WriteLine(result.FileName);

        var exporterWord = new WordExporter();
        var resultWord = await exporterWord.ExportListByTemplate(filePath + ".docx", items);
        Console.WriteLine(resultWord.FileName);

        var exporterPdf = new PdfExporter();
        var resultPdf = await exporterPdf.ExportListByTemplate(filePath + ".pdf", items);
        Console.WriteLine(resultPdf.FileName);

        var exporterHtml = new HtmlExporter();
        var resultHtml = await exporterHtml.ExportListByTemplate(filePath + ".html", items);
        Console.WriteLine(resultHtml.FileName);
    }

} 
