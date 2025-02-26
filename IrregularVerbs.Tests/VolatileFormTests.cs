using System;
using IrregularVerbs.Domain.Models.Components;
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
        Assert.That(_learnedVerbOriginal.Inspect(_learnedVerbEqual0));
    }
    
    [Test]
    public void InspectEqual1()
    {
        Assert.That(_learnedVerbOriginal.Inspect(_learnedVerbEqual1));
    }
    
    [Test]
    public void InspectOneVariant0()
    {
        Assert.That(_learnedVerbOriginal.Inspect(_learnedVerbWithOneVariant0));
    }
    
    [Test]
    public void InspectOneVariant1()
    {
        Assert.That(_learnedVerbOriginal.Inspect(_learnedVerbWithOneVariant1));
    }

    [Test]
    public void InspectWrongInput()
    {
        Assert.IsFalse(_learnedVerbOriginal.Inspect(_wrongInput));
    }
    
    [Test]
    public void InspectOneWrongVariant()
    {
        Assert.IsFalse(_learnedVerbOriginal.Inspect(_wrongInputWithOneVariant));
    }
}