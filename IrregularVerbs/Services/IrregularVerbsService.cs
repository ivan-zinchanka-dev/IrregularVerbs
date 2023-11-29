using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
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

    public List<IrregularVerb> GetRandomVerbForms(int formsCount)
    {
        if (IrregularVerbs == null)
        {
            return null;
        }

        if (formsCount > IrregularVerbs.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(formsCount), formsCount, "should be less than " + IrregularVerbs.Count);
        }

        Random random = new Random();
        
        List<IrregularVerb> allEmptyForms = IrregularVerbs.Select(verb => verb.GetEmptyForm()).ToList();
        List<IrregularVerb> randomEmptyForms = new List<IrregularVerb>(formsCount);

        LinkedList<int> indices = new LinkedList<int>();
        
        for (int i = 0; i < formsCount; i++)
        {
            int randomFormIndex = random.Next(allEmptyForms.Count);

            while (indices.Contains(randomFormIndex))
            {
                randomFormIndex = random.Next(allEmptyForms.Count);
            }

            indices.AddLast(randomFormIndex);
            randomEmptyForms.Add(allEmptyForms[randomFormIndex]);
        }

        return randomEmptyForms;
    }

}