using System;
using System.IO.Ports;
using Signal.Infrastructure.Services.Serial;

class Program 
{ 
    
    [STAThread] 
    static void Main(string[] args)
    {
        var dataProvider = new SerialDataProvider("COM5");
    } 
}