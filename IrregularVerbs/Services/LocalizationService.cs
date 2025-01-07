using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;

namespace IrregularVerbs.Services;

public class LocalizationService
{
    private const string LocalizationSourcePath = "Resources/localization.xlsx";

    private readonly List<string> _languages = new List<string>();
    private readonly Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();

    private string _currentLanguage;
    
    public string CurrentLanguage
    {
        get => _currentLanguage;

        set
        {
            _currentLanguage = value;
            OnLanguageChanged?.Invoke(_currentLanguage);
        }
    }

    public event Action<string> OnLanguageChanged;

    private int GetLanguageId(string language)
    {
        return _languages.IndexOf(language);
    }

    public LocalizationService()
    {
        if (File.Exists(LocalizationSourcePath))
        {
            try
            {
                ReadTableData();
            }
            catch (Exception exception)
            {
                throw new Exception("Localization source table is corrupted", exception);
            }
        }
        else
        {
            throw new FileNotFoundException("Localization source table not found", LocalizationSourcePath);
        }
    }

    public string Localize(string term)
    {
        try
        {
            if (_dictionary.TryGetValue(term, out List<string> results))
            {
                string match = results[GetLanguageId(CurrentLanguage)];

                if (!string.IsNullOrEmpty(match))
                {
                    return match;
                }
            }
            
            return GetFailResult(term);
        }
        catch (Exception ex)
        {
            return GetFailResult(term);
        }
    }

    private static string GetFailResult(string term)
    {
        return $"[{term}]";
    }

    private void ReadTableData()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        
        using (Stream stream = File.Open(LocalizationSourcePath, FileMode.Open, FileAccess.Read))
        {
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
            {
                ReadTableData(reader.AsDataSet().Tables[0]);
            }
        }
    }

    private void ReadTableData(DataTable sourceTable)
    {
        int columnsCount = sourceTable.Columns.Count;
        DataRow headers = sourceTable.Rows[0];
                
        for (int headerIndex = 1; headerIndex < columnsCount; headerIndex++)
        {
            _languages.Add(headers[headerIndex].ToString());
        }
                
        for (int rowIndex = 1; rowIndex < sourceTable.Rows.Count; rowIndex++)
        {
            DataRow dataRow = sourceTable.Rows[rowIndex];
            string term = dataRow[0].ToString();
                        
            List<string> words = new List<string>(columnsCount);
                        
            for (int wordIndex = 1; wordIndex < columnsCount; wordIndex++)
            {
                words.Add(dataRow[wordIndex].ToString());
            }
                        
            _dictionary.Add(term, words);
        }
    }

}