using System;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using NUnit.Framework;

namespace IrregularVerbs.Tests;

[TestFixture]
public class GeneralTests
{
    [Test]
    public void Check()
    {
        Language lang = Language.English;
        object obj = lang;

        int number = (int)obj;
        
        Console.WriteLine(number);

    }
}