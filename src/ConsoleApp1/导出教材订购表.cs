// ********************************** 
// Densen Informatica 中讯科技 
// 作者：Alex Chow
// e-mail:zhouchuanglin@gmail.com 
// **********************************

using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.ExporterAndImporter.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AME.Services.Tools
{
    /// <summary>
    /// 教材订购信息
    /// </summary>
    public class TextbookOrderInfo
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; }

        /// <summary>
        /// 制表人
        /// </summary>
        public string Watchmaker { get; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; }

        /// <summary>
        /// 教材信息列表
        /// </summary>
        public List<BookInfo> BookInfos { get; }

        public TextbookOrderInfo(string company, string address, string contact, string tel, string watchmaker, string time, List<BookInfo> bookInfo)
        {
            Company = company;
            Address = address;
            Contact = contact;
            Tel = tel;
            Watchmaker = watchmaker;
            Time = time;
            BookInfos = bookInfo;
        }
    }


    /// <summary>
    /// 教材信息
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int RowNo { get; }

        /// <summary>
        /// 书号
        /// </summary>
        public string No { get; }

        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 主编
        /// </summary>
        public string EditorInChief { get; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string PublishingHouse { get; }

        /// <summary>
        /// 定价
        /// </summary>
        public string Price { get; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public int PurchaseQuantity { get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; }

        public BookInfo(int rowNo, string no, string name, string editorInChief, string publishingHouse, string price, int purchaseQuantity, string remark)
        {
            RowNo = rowNo;
            No = no;
            Name = name;
            EditorInChief = editorInChief;
            PublishingHouse = publishingHouse;
            Price = price;
            PurchaseQuantity = purchaseQuantity;
            Remark = remark;
        }
    }

    public static class 导出教材订购明细样表
    {

        public static async Task 导出()
        {
            //模板路径
            var tplPath = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", "ExportTemplates",
                    "2020年春季教材订购明细样表.xlsx");
            //创建Excel导出对象
            IExportFileByTemplate exporter = new ExcelExporter();
            //导出路径
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), nameof(导出) + ".xlsx");
            if (File.Exists(filePath)) File.Delete(filePath);
            //根据模板导出
            await exporter.ExportByTemplate(filePath,
                new TextbookOrderInfo("湖南心莱信息科技有限公司", "湖南长沙岳麓区", "雪雁", "1367197xxxx", "雪雁", DateTime.Now.ToLongDateString(),
                    new List<BookInfo>()
                    {
                new BookInfo(1, "0000000001", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注"),
                new BookInfo(2, "0000000002", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注"),
                new BookInfo(3, "0000000003", "《XX从入门到放弃》", "张三", "机械工业出版社", "3.14", 100, "备注")
                    }),
                tplPath);
        }
    }

}
