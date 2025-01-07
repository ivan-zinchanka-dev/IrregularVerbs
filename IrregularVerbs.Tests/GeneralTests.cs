using System;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Services;
using IrregularVerbs.Services.Localization;
using NUnit.Framework;

namespace IrregularVerbs.Tests;

[TestFixture]
public class GeneralTests
{
    [Test]
    public void Check1()
    {
        try
        {
            throw new LocalizationException("Language not found");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}