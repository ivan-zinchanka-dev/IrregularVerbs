using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExcelDataReader;
using IrregularVerbs.Models;

namespace IrregularVerbs.Services;

public class IrregularVerbsService
{
    private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.xlsx";
    public List<IrregularVerb> IrregularVerbs { get; private set; }
    
    public IrregularVerbsService()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        if (File.Exists(IrregularVerbsSourcePath))
        {
            using (Stream stream = File.Open(IrregularVerbsSourcePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataTable dataTable = reader.AsDataSet().Tables[0];

                    IrregularVerbs = new List<IrregularVerb>(dataTable.Rows.Count);
                    
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        IrregularVerb irregularVerb = new IrregularVerb()
                        {
                            Term = dataTable.Rows[i]["Column0"].ToString(),
                            Infinitive = dataTable.Rows[i]["Column1"].ToString(),
                            PastSimple = dataTable.Rows[i]["Column2"].ToString(),
                            PastParticiple = dataTable.Rows[i]["Column3"].ToString(),
                        };
                        
                        /*Console.WriteLine("Term: " + irregularVerb.Term);
                        Console.WriteLine("PastSimple: " + irregularVerb.PastSimple);*/
                        
                        IrregularVerbs.Add(irregularVerb);
                    }
                    
                }
            }
        }
        else
        {
            throw new FileNotFoundException("File not found", IrregularVerbsSourcePath);
        }
    }
    
}