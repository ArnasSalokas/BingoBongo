using System.Collections.Generic;

namespace Template.Services.Exporting.Contracts
{
    public interface IExcelExportService
    {
        byte[] Export<T>(string name, List<T> records);
    }
}
