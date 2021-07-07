using System;

namespace RaspberryWebTerminal.Models
{
    public class Unknown
    {
        public readonly string Info = "I am unknown class.";
        private readonly Guid _classGuid;
        public string DefaultProperty { get; set; }

        public Unknown()
        {
            _classGuid = new Guid();
            DefaultProperty = "Initialized successfully";
        }

        public string GenerateReport(int? optionalParameter = 0)
        {
            string head = $"Class GUID: {_classGuid};\n";
            string body = $"Type: Unknown. Info: {Info};\n";
            string foot = $"Last property value: {DefaultProperty}";
            string resultString = head + body + foot;

            return resultString;
        }
    }
}