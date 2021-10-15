using System;
using System.Collections.Generic;

namespace EngineSimulator
{
    interface IEngineBody
    {
        string engineName { get; set; }
        double heatTemp();
        double condTemp(double _environmentTemp, double _engineTemp);
        void ReadValue(double _I, double _C, List<int> startM, List<int> startV, double _overheatTemp,
            double _hRateTorque, double _hRateRotSpeed, double _environmentTemp);
    }

    class InternalCombustionEngine : IEngineBody
    {
        public double I, C, M, V;
        public List<int> piecesM;
        public List<int> piecesV;
        public double overheatTemp, hRateTorque, hRateRotSpeed;
        private string nameValue;

        public string engineName
        {
            get => nameValue;
            set => nameValue = value;
        }
        public double heatTemp()
        {
            return M * hRateTorque + V * V * hRateRotSpeed;
        }
        public double condTemp(double _environmentTemp, double _engineTemp)
        {
            return C * (_environmentTemp - _engineTemp);
        }
        public void ReadValue(double _I, double _C, List<int> startM, List<int> startV, double _overheatTemp,
            double _hRateTorque, double _hRateRotSpeed, double _environmentTemp)
        {
            I = _I; C = _C; piecesM = startM; piecesV = startV; overheatTemp = _overheatTemp;
            hRateTorque = _hRateTorque; hRateRotSpeed = _hRateRotSpeed;
        }
    }
}
