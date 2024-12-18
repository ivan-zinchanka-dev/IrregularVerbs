﻿using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Services;

public class IrregularVerbsStorage
{
    private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.xlsx";
    
    private readonly IrregularVerbsFactory _verbsFactory;
    private readonly List<BaseIrregularVerb> _irregularVerbs;

    public IReadOnlyCollection<BaseIrregularVerb> IrregularVerbs => _irregularVerbs;

    public IrregularVerbsStorage(IrregularVerbsFactory verbsFactory)
    {
        _verbsFactory = verbsFactory;
        
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        if (File.Exists(IrregularVerbsSourcePath))
        {
            using (Stream stream = File.Open(IrregularVerbsSourcePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataTable dataTable = reader.AsDataSet().Tables[0];

                    _irregularVerbs = new List<BaseIrregularVerb>(dataTable.Rows.Count);
                    
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        BaseIrregularVerb irregularVerb = _verbsFactory.FromDataRow(dataTable.Rows[i]);

                        if (irregularVerb != null)
                        {
                            _irregularVerbs.Add(irregularVerb);
                        }
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