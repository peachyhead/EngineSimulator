using System.Collections.Generic;

namespace EngineSimulator
{
    interface ITestingProps<T1> where T1 : class
    {
        IEngineBody usedEngine { get; set; }
        IEngineBody GetEngine() { return usedEngine; }
        int RunEngine(T1 engine, double value);
    }

    class InternalBurnTestingProps : ITestingProps<InternalCombustionEngine>
    {
        public int maxTime = 1800;
        double measurementError = 0.1f;

        private InternalCombustionEngine internalBurn;
        public IEngineBody usedEngine
        {
            get => internalBurn;
            set => internalBurn = (InternalCombustionEngine)value;
        }

        public class usedValues
        {
            public double I = 10;
            public double C = 0.1f;
            public double overheatTemp = 110;
            public double hRateTorque = 0.01f;
            public double hRateRotSpeed = 0.0001f;
            public List<int> piecesM =
                new List<int> { 20, 75, 100, 105, 75, 0 };
            public List<int> piecesV =
                new List<int> { 0, 75, 150, 200, 250, 300 };
        }

        public void WriteValues(InternalCombustionEngine engine, double environmentTemp)
        {
            usedValues values = new usedValues();
            engine.ReadValue(values.I, values.C, values.piecesM, values.piecesV, values.overheatTemp,
                values.hRateTorque, values.hRateRotSpeed, environmentTemp);
        }

        public int RunEngine(InternalCombustionEngine engine, double environmentTemp)
        {
            WriteValues(engine, environmentTemp);
            double engineTemp = environmentTemp;
            double a = engine.M / engine.I;
            double eps = engine.overheatTemp - engineTemp;
            int numberOfPiece = 0;
            int time = 0;

            while (eps > measurementError && time < maxTime)
            {
                time++;
                engine.V += a;
                if (numberOfPiece < engine.piecesM.Count - 2)
                    numberOfPiece += engine.V < engine.piecesV[numberOfPiece + 1] ? 0 : 1;
                double up = engine.V - engine.piecesV[numberOfPiece];
                double down = engine.piecesV[numberOfPiece + 1] - engine.piecesV[numberOfPiece];
                double factor = engine.piecesM[numberOfPiece + 1] - engine.piecesM[numberOfPiece];
                engine.M = up / down * factor + engine.piecesM[numberOfPiece];
                engineTemp += engine.condTemp(environmentTemp, engineTemp) + engine.heatTemp();
                a = engine.M / engine.I;
                eps = engine.overheatTemp - engineTemp;
            }
            return time;
        }
    }
}
