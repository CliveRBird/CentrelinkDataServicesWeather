﻿namespace CDS.Weather.Wrappers;

public interface IRandomWrapper
{
    int Next(int minValue, int maxValue);
}