﻿using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using System;

public class ModelInput
{
    [LoadColumn(0)]
    public float Temperature { get; set; }

    [LoadColumn(1)]
    public float Luminosity { get; set; }

    [LoadColumn(2)]
    public float Infrared { get; set; }

    [LoadColumn(3)]
    public float Distance { get; set; }

    [LoadColumn(4)]
    public string CreatedAt { get; set; }

    [ColumnName("Label"), LoadColumn(5)]
    public string Source { get; set; }
}

public class ModelOutput
{
    [ColumnName("PredictedLabel")]
    public string PredictedLabel;

    [ColumnName("Score")]
    public float[] Score;
}

public class CustomInputRow
{
    public string CreatedAt;
}

public class CustomOutputRow
{
    public float Hour;
    public float Day;
}

[CustomMappingFactoryAttribute(nameof(CustomMappings.IncomeMapping))]
public class CustomMappings : CustomMappingFactory<CustomInputRow, CustomOutputRow>
{
    public static void IncomeMapping(CustomInputRow input, CustomOutputRow output)
    {
        output.Hour = DateTime.Parse(input.CreatedAt).Hour;
        output.Day = DateTime.Parse(input.CreatedAt).DayOfYear;
    }

    // This factory method will be called when loading the model to get the mapping operation.
    public override Action<CustomInputRow, CustomOutputRow> GetMapping()
    {
        return IncomeMapping;
    }
}
