using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using IrregularVerbs.Factories;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Services;

public class LocalizationService
{
    private const string LocalizationSourcePath = "Resources/localization.xlsx";
    
    private readonly Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();

    private Language _currentLanguage;
    
    public Language CurrentLanguage
    {
        get => _currentLanguage;

        set
        {
            _currentLanguage = value;
            OnLanguageChanged?.Invoke(_currentLanguage);
        }
    }

    public event Action<Language> OnLanguageChanged;

    public LocalizationService()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        if (File.Exists(LocalizationSourcePath))
        {
            using (Stream stream = File.Open(LocalizationSourcePath, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    DataTable dataTable = reader.AsDataSet().Tables[0];
                    
                    Console.WriteLine("Count: " + dataTable.Columns.Count);
                    
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        DataRow dataRow = dataTable.Rows[i];
                        string term = dataRow[0].ToString();
                        
                        if (!string.IsNullOrEmpty(term))
                        {
                            _dictionary.Add(term, new List<string>()
                            {
                                dataRow[1].ToString(),
                                dataRow[2].ToString(),
                                dataRow[3].ToString(),
                            });
                        }
                        
                    }
                    
                }
            }
        }
        else
        {
            throw new FileNotFoundException("File not found", LocalizationSourcePath);
        }
    }

    public string Localize(string term)
    {
        if (_dictionary.TryGetValue(term, out List<string> results))
        {
            string match = results[(int)CurrentLanguage];

            if (!string.IsNullOrEmpty(match))
            {
                return match;
            }
        }
        
        return $"[{term}]";
    }

}