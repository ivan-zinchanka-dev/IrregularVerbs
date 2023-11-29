using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using IrregularVerbs.Models;

namespace IrregularVerbs.Services;

public class IrregularVerbsService
{
    private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.json";
    //private const string IrregularVerbsSourcePath = "Resources/irregular_verbs_source.json";
    private List<IrregularVerb> _irregularVerbs;

    public IrregularVerbsService()
    {
        if (File.Exists(IrregularVerbsSourcePath))
        {
            string jsonNotation = File.ReadAllText(IrregularVerbsSourcePath);
            _irregularVerbs = JsonSerializer.Deserialize<List<IrregularVerb>>(jsonNotation);
        }
        else
        {
            throw new FileNotFoundException("File not found", IrregularVerbsSourcePath);
        }
    }

    public void Write()
    {
        _irregularVerbs = new List<IrregularVerb>()
        {
            new IrregularVerb()
            {
                Term = "term",
                Infinitive = "beat",
                PastSimple = "beat",
                PastParticiple = "beaten",
            },
            
            new IrregularVerb()
            {
                Term = "term",
                Infinitive = "become",
                PastSimple = "became",
                PastParticiple = "become",
            },
            
            new IrregularVerb()
            {
                Term = "term",
                Infinitive = "begin",
                PastSimple = "began",
                PastParticiple = "begun",
            },
            
        };
        
        string jsonNotation = JsonSerializer.Serialize(_irregularVerbs, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(jsonNotation);
        File.WriteAllText(IrregularVerbsSourcePath, jsonNotation);
    }
}