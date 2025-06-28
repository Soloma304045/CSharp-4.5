using System;
using Library;

class Program
{
    static void Main()
    {
        var calculator = new Calculator();
        calculator.ProcessFile("../input.txt");
    }
}
