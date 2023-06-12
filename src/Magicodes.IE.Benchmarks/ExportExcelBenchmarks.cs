﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Magicodes.Benchmarks.Models;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Diagnostics.Windows.Configs;

namespace Magicodes.Benchmarks
{

    //[SimpleJob(launchCount: 1, warmupCount: 1, targetCount: 5, runtimeMoniker: RuntimeMoniker.net48)]
    //[SimpleJob(launchCount: 1, warmupCount: 1, targetCount: 5, runtimeMoniker: RuntimeMoniker.NetCoreApp22)]
    [ThreadingDiagnoser]
    [TailCallDiagnoser]
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 1, warmupCount: 1, targetCount: 5, runtimeMoniker: RuntimeMoniker.NetCoreApp31)]
    public class ExportExcelBenchmarks
    {
        [Params(10000, 120000, 240000, 500000, 1000000)]
        public int RowsCount;
        private readonly List<ExportTestDataWithAttrs> _exportTestData = new List<ExportTestDataWithAttrs>();
        private readonly IExporter _exporter;

        public ExportExcelBenchmarks()
        {
            _exporter = new ExcelExporter();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (var i = 1; i <= RowsCount; i++)
            {
                _exportTestData.Add(new ExportTestDataWithAttrs
                {
                    Age = i,
                    Name = "Mr.A",
                    Text3 = "Text3"
                });
            }
        }


        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _exportTestData.Clear();
        }

        [Benchmark]
        public async Task ExportExcelAsByteArrayTest()
        {
            await _exporter.ExportAsByteArray(_exportTestData);
        }
    }
}
