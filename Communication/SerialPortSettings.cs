using System.IO.Ports;

namespace HendersonvilleTrafficTest.Communication
{
    public class SerialPortSettings
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public Parity Parity { get; set; } = Parity.None;
        public int DataBits { get; set; } = 8;
        public StopBits StopBits { get; set; } = StopBits.One;
        public Handshake Handshake { get; set; } = Handshake.None;
        public int ReadTimeoutMs { get; set; } = 1000;
        public int WriteTimeoutMs { get; set; } = 1000;
        public string NewLine { get; set; } = "\r\n";
        public bool UseNewLineForBinary { get; set; } = true;
        public bool DtrEnable { get; set; } = false;
        public bool RtsEnable { get; set; } = false;

        public SerialPortSettings Clone()
        {
            return new SerialPortSettings
            {
                PortName = PortName,
                BaudRate = BaudRate,
                Parity = Parity,
                DataBits = DataBits,
                StopBits = StopBits,
                Handshake = Handshake,
                ReadTimeoutMs = ReadTimeoutMs,
                WriteTimeoutMs = WriteTimeoutMs,
                NewLine = NewLine,
                UseNewLineForBinary = UseNewLineForBinary,
                DtrEnable = DtrEnable,
                RtsEnable = RtsEnable
            };
        }

        public override string ToString()
        {
            return $"{PortName}: {BaudRate},{DataBits},{Parity},{StopBits}";
        }
    }
}