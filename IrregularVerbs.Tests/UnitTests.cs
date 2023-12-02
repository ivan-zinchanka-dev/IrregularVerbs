using System;
using IrregularVerbs.Models.Verbs.Components;
using NUnit.Framework;

namespace IrregularVerbs.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    
    [Test]
    public void VolatileForms()
    {
        VolatileForm learnVerb0 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        VolatileForm learnVerb1 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        
        Assert.AreEqual(learnVerb0, learnVerb1);
    }
    
    [Test]
    public void SwappedVolatileForms()
    {
        VolatileForm learnVerb0 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        VolatileForm learnVerb1 = new VolatileForm(new Tuple<string, string>("learned", "learnt"), CombineOperation.Or);
        
        Assert.AreEqual(learnVerb0, learnVerb1);
    }
    
    [Test]
    public void WrongVolatileForms()
    {
        VolatileForm learnVerb0 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        VolatileForm learnVerb1 = new VolatileForm(new Tuple<string, string>("er", "ert"), CombineOperation.Or);
        
        Assert.AreNotEqual(learnVerb0, learnVerb1);
    }
    
    
    [Test]
    public void OneVolatileForm()
    {
        VolatileForm learnVerb0 = new VolatileForm(new Tuple<string, string>("learned", string.Empty), CombineOperation.Or);
        VolatileForm learnVerb1 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        
        Assert.AreEqual(learnVerb0, learnVerb1);
    }
    
    [Test]
    public void ReverseOneVolatileForm() // problem of original, should be check instead of equal
    {
        // IPasser: bool IsPass(original)
        
        VolatileForm learnVerb0 = new VolatileForm(new Tuple<string, string>("learned", string.Empty), CombineOperation.Or);
        VolatileForm learnVerb1 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        
        Assert.AreEqual(learnVerb1, learnVerb0);
    }
}