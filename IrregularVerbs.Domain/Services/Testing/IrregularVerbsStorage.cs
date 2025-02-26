using System.Data;
using System.Text;
using ExcelDataReader;
using IrregularVerbs.Domain.Factories;
using IrregularVerbs.Domain.Models.Verbs;

namespace IrregularVerbs.Domain.Services.Testing;

public class IrregularVerbsStorage
{
    private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.xlsx";

    public IReadOnlyCollection<BaseIrregularVerb> IrregularVerbs { get; }

    public IrregularVerbsStorage(IrregularVerbsFactory verbsFactory)
    {
        if (File.Exists(IrregularVerbsSourcePath))
        {
            try
            {
                IrregularVerbs = ReadTableData(verbsFactory);
            }
            catch (Exception exception)
            {
                throw new Exception("Irregular verbs source table is corrupted", exception);
            }
        }
        else
        {
            throw new FileNotFoundException(
                "Irregular verbs source table not found", 
                IrregularVerbsSourcePath);
        }
    }
    
    private static IReadOnlyCollection<BaseIrregularVerb> ReadTableData(IrregularVerbsFactory verbsFactory)
    {
        List<BaseIrregularVerb> irregularVerbs;
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        using (Stream stream = File.Open(IrregularVerbsSourcePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataTable dataTable = reader.AsDataSet().Tables[0];

                irregularVerbs = new List<BaseIrregularVerb>(dataTable.Rows.Count);
                    
                for (int i = 1; i < dataTable.Rows.Count; i++)
                {
                    BaseIrregularVerb irregularVerb = verbsFactory.FromDataRow(dataTable.Rows[i]);

                    if (irregularVerb != null)
                    {
                        irregularVerbs.Add(irregularVerb);
                    }
                }
            }
        }

        return irregularVerbs;
    }
    
}