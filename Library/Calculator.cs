namespace Library;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Calculator
{
    private readonly Dictionary<char, Func<double, double, double>> operations;

    public Calculator()
    {
        operations = new Dictionary<char, Func<double, double, double>>
        {
            ['+'] = (a, b) => a + b,
            ['-'] = (a, b) => a - b,
            ['*'] = (a, b) => a * b,
            ['/'] = (a, b) => a / b
        };
    }

    public void ProcessFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Файл '{filePath}' не найден.");
            return;
        }

        string pattern = @"(\d+)\s*([+\-*/])\s*(\d+)";
        var regex = new Regex(pattern);

        foreach (var line in File.ReadLines(filePath))
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                double operand1 = double.Parse(match.Groups[1].Value);
                char op = match.Groups[2].Value[0];
                double operand2 = double.Parse(match.Groups[3].Value);

                if (operations.TryGetValue(op, out var operation))
                {
                    double result = operation(operand1, operand2);
                    Console.WriteLine($"{operand1} {op} {operand2} = {result}");
                }
                else
                {
                    Console.WriteLine($"Неизвестная операция: {op}");
                }
            }
            else
            {
                Console.WriteLine($"Неверный формат строки: {line}");
            }
        }
    }
}
