using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Magicodes.IE
{
    public static class 导出DEMO
    {
        public static async Task 动态导出()
        {
            IExporter exporter = new ExcelExporter();
            // 生成测试数据
            var personList = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                personList.Add(new Person
                {
                    FirstName = i.ToString(),
                    LastName = i.ToString(),
                    Title = i.ToString()
                });
            }

            // 导出一个只包含"FirstName", "LastName"列的excel
            string fields = "FirstName,LastName"; // 可自定义导出想要的字段
            var expandoObjectList = new List<ExpandoObject>(personList.Count);
            var propertyInfoList = new List<PropertyInfo>();
            var fieldsAfterSplit = fields.Split(',');
            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();
                var propertyInfo = typeof(Person).GetProperty(propertyName);

                if (propertyInfo == null)
                {
                    throw new Exception($"Property: {propertyName} 没有找到：{typeof(Person)}");
                }

                propertyInfoList.Add(propertyInfo);
            }

            foreach (var person in personList)
            {
                var shapedObj = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(person);
                    ((IDictionary<string, object>)shapedObj).Add(propertyInfo.Name, propertyValue);
                }

                expandoObjectList.Add(shapedObj);
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "dynamicExportExcel.xlsx");
            var result = await exporter.ExportAsByteArray<ExpandoObject>(expandoObjectList);
            File.WriteAllBytes(filePath, result);


        }
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Title { get; set; }
            public int Age { get; set; }
            public int NumberOfKids { get; set; }
        }
    }
}
