using System;
using IrregularVerbs.Models.Verbs.Components;
using NUnit.Framework;

namespace IrregularVerbs.Tests;

[TestFixture]
public class VolatileFormTests
{
    private VolatileForm _learnedVerbOriginal;
    
    private VolatileForm _learnedVerbEqual0;
    private VolatileForm _learnedVerbEqual1;
    
    private VolatileForm _learnedVerbWithOneVariant0;
    private VolatileForm _learnedVerbWithOneVariant1;

    private VolatileForm _wrongInput;
    private VolatileForm _wrongInputWithOneVariant;

    [SetUp]
    public void Setup()
    {
        _learnedVerbOriginal = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        
        _learnedVerbEqual0 = new VolatileForm(new Tuple<string, string>("learnt", "learned"), CombineOperation.Or);
        _learnedVerbEqual1 = new VolatileForm(new Tuple<string, string>("learned", "learnt"), CombineOperation.Or);
        
        _learnedVerbWithOneVariant0 = new VolatileForm(new Tuple<string, string>("learnt", string.Empty), CombineOperation.Or);
        _learnedVerbWithOneVariant1 = new VolatileForm(new Tuple<string, string>("learned", string.Empty), CombineOperation.Or);
        
        _wrongInput = new VolatileForm(new Tuple<string, string>("wrong", "wronger"), CombineOperation.Or);
        _wrongInputWithOneVariant = new VolatileForm(new Tuple<string, string>("wrong", string.Empty), CombineOperation.Or);
    }
    
    [Test]
    public void InspectEqual0()
    {
        Assert.That(VolatileForm.Inspect(_learnedVerbEqual0, _learnedVerbOriginal));
    }
    
    [Test]
    public void InspectEqual1()
    {
        Assert.That(VolatileForm.Inspect(_learnedVerbEqual1, _learnedVerbOriginal));
    }
    
    [Test]
    public void InspectOneVariant0()
    {
        Assert.That(VolatileForm.Inspect(_learnedVerbWithOneVariant0, _learnedVerbOriginal));
    }
    
    [Test]
    public void InspectOneVariant1()
    {
        Assert.That(VolatileForm.Inspect(_learnedVerbWithOneVariant1, _learnedVerbOriginal));
    }

    [Test]
    public void InspectWrongInput()
    {
        Assert.IsFalse(VolatileForm.Inspect(_wrongInput, _learnedVerbOriginal));
    }
    
    [Test]
    public void InspectOneWrongVariant()
    {
        Assert.IsFalse(VolatileForm.Inspect(_wrongInputWithOneVariant, _learnedVerbOriginal));
    }
}