using System;
using IrregularVerbs.Services;

namespace IrregularVerbs.Models;

public class ApplicationSettings
{
    public Language NativeLanguage { get; set; }
    public int VerbsCount { get; set; }
    public bool DisorderVerbs { get; set; }
    
}