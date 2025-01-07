using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ExcelDataReader;
using IrregularVerbs.CodeBase.Localization;

namespace IrregularVerbs.Services.Localization;

public class LocalizationService : ILocalizationService
{
    private const string LocalizationSourcePath = "Resources/localization.xlsx";

    private readonly List<string> _languages = new List<string>();
    private readonly Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();
    private string _currentLanguage;
    
    public IReadOnlyCollection<string> Languages => _languages; 
    
    public string CurrentLanguage
    {
        get => _currentLanguage;

        set
        {
            if (_languages.Contains(value))
            {
                _currentLanguage = value;
                OnLanguageChanged?.Invoke(_currentLanguage);
            }
            else
            {
                throw new LocalizationException($"Language \"{value}\" not defined.");
            }
        }
    }
    
    public event Action<string> OnLanguageChanged;
    
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
                throw new LocalizationException("Localization source table is corrupted", exception);
            }
        }
        else
        {
            throw new LocalizationException(
                $"Localization source table not found. Expected source path: {LocalizationSourcePath}");
        }
    }

    public string Localize(string term)
    {
        try
        {
            if (TryLocalizeInternal(term, out string result))
            {
                return result;
            }

            throw CreateTranslationException(term);
        }
        catch (Exception exception)
        {
            throw CreateTranslationException(term, exception);
        }
    }

    public bool TryLocalize(string term, out string result)
    {
        try
        {
            return TryLocalizeInternal(term, out result);
        }
        catch (Exception exception)
        {
            result = null;
            return false;
        }
    }

    private bool TryLocalizeInternal(string term, out string result)
    {
        if (_dictionary.TryGetValue(term, out List<string> results))
        {
            string match = results[GetLanguageIndex(CurrentLanguage)];

            if (match != null)
            {
                result = match;
                return true;
            }
        }

        result = null;
        return false;
    }
    
    private int GetLanguageIndex(string language)
    {
        return _languages.IndexOf(language);
    }
    
    private LocalizationException CreateTranslationException(string term, Exception innerException = null)
    {
        return new LocalizationException(
            $"Failed to translate term \"{term}\" into \"{CurrentLanguage}\" language.", innerException);
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