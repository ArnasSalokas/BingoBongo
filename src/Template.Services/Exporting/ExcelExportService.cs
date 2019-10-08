using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using OfficeOpenXml;
using Template.Common.Extensions;
using Template.Common.Helpers;
using Template.Entities.Enums;
using Template.Services.Base;
using Template.Services.Exporting.Contracts;
using Template.Services.Exporting.Models;

namespace Template.Services.Exporting
{
    public class ExcelExportService : BaseService, IExcelExportService
    {
        public ExcelExportService(IServiceProvider services) : base(services)
        {
        }

        public byte[] Export<T>(string name, List<T> records)
        {
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add(name);

                var instructions = ExportInstruction.Construct(records.Count() > 0 ? records.First() : Activator.CreateInstance<T>());

                AddHeaders(ref workSheet, instructions);

                // First row was header, Cells are numbered from 1, not 0.
                for (int i = 0; i < records.Count(); i++)
                {
                    AddRow(ref workSheet, instructions, records[i], i + 2);
                }

                return package.GetAsByteArray();
            }
        }

        private void AddHeaders(ref ExcelWorksheet workSheet, IEnumerable<ExportInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                workSheet.Cells[1, instruction.Order].Style.Numberformat.Format = ExcelCellType.General.GetDescription();
                workSheet.Cells[1, instruction.Order].Style.Font.Bold = true;
                workSheet.Cells[1, instruction.Order].Value = instruction.Header;
            }
        }

        private void AddRow<T>(ref ExcelWorksheet workSheet, IEnumerable<ExportInstruction> instructions, T entity, int rowNumber)
        {
            foreach (var instruction in instructions)
            {
                workSheet.Cells[rowNumber, instruction.Order].Style.Numberformat.Format = instruction.Format;
                workSheet.Cells[rowNumber, instruction.Order].Value = ReflectionHelper.GetPropertyValue(entity, instruction.Property);
            }
        }
    }
}
