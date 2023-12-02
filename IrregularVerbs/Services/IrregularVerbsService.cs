﻿using System;
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
using IrregularVerbs.Factories;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Services;

public class IrregularVerbsService
{
    private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.xlsx";
    public List<BaseIrregularVerb> IrregularVerbs { get; private set; }

    private IrregularVerbsFactory _verbsFactory = new IrregularVerbsFactory();
    
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

                    IrregularVerbs = new List<BaseIrregularVerb>(dataTable.Rows.Count);
                    
                    for (int i = 1; i < dataTable.Rows.Count; i++)
                    {
                        BaseIrregularVerb irregularVerb = _verbsFactory.FromDataRow(dataTable.Rows[i]);

                        if (irregularVerb != null)
                        {
                            IrregularVerbs.Add(irregularVerb);
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

    private static IrregularVerbAnswer CreateAnswer(BaseIrregularVerb irregularVerb)
    {
        return new IrregularVerbAnswer()
        {
            Term = irregularVerb.Term,
        };
    }

    public List<IrregularVerbAnswer> GetRandomVerbAnswers(int formsCount)
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
        
        List<IrregularVerbAnswer> allEmptyAnswers = IrregularVerbs.Select(CreateAnswer).ToList();
        List<IrregularVerbAnswer> randomEmptyAnswers = new List<IrregularVerbAnswer>(formsCount);

        LinkedList<int> indices = new LinkedList<int>();
        
        for (int i = 0; i < formsCount; i++)
        {
            int randomFormIndex = random.Next(allEmptyAnswers.Count);

            while (indices.Contains(randomFormIndex))
            {
                randomFormIndex = random.Next(allEmptyAnswers.Count);
            }

            indices.AddLast(randomFormIndex);
            randomEmptyAnswers.Add(allEmptyAnswers[randomFormIndex]);
        }

        return randomEmptyAnswers;
    }

}