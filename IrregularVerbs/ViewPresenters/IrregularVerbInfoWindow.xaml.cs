﻿using System.Windows;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.ViewPresenters;

public partial class IrregularVerbInfoWindow : Window
{
    private const string CorrectVerbsPattern = 
        "Native Word: {0}\nInfinitive: {1}\nPast Simple: {2}\nPast Participle: {3}";
    
    public IrregularVerbInfoWindow(BaseIrregularVerb irregularVerb)
    {
        InitializeComponent();
        
        _infoText.Text = string.Format(CorrectVerbsPattern, 
            irregularVerb.NativeWord, 
            irregularVerb.Infinitive, 
            irregularVerb.PastSimple, 
            irregularVerb.PastParticiple);
    }
    
    private void OnGotItClick(object sender, RoutedEventArgs e)
    {
        DialogResult = true;
    }
}