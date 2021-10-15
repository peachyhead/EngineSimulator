using System;
namespace EngineSimulator
{
    interface IConsoleDebug
    {
        double ReadTemp();
        void WriteTemp(int time, int maxTime, string engineName);
    }
    class RunTimeTestConsoleDebug : IConsoleDebug
    {
        public static string experimentalEngine;
        public double ReadTemp()
        {
            Console.WriteLine("Please, enter environment temperature in celsium");
            return Convert.ToDouble(Console.ReadLine());
        }

        public void WriteTemp(int time, int maxTime, string engineName)
        {
            if (time == maxTime) Console.WriteLine("engine '" + engineName + "'will not overheat, running with this temperature");
            else Console.WriteLine("engine '" + engineName + "' will overheat in " + time + " sec");
        }

        static void Main()
        {
            RunTimeTestConsoleDebug consoleDebug = new RunTimeTestConsoleDebug();
            InternalBurnTestingProps props = new InternalBurnTestingProps();
            InternalCombustionEngine engine = new InternalCombustionEngine();
            InternalBurnTestingProps.usedValues usedValues = new InternalBurnTestingProps.usedValues();
            
            engine.engineName = experimentalEngine = "newTestEngine";
            double environmentTemp = consoleDebug.ReadTemp();
            consoleDebug.WriteTemp(props.RunEngine(engine, environmentTemp), props.maxTime, experimentalEngine);
        }
    }

}
