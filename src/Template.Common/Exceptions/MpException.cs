using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Template.Common.Extensions;

namespace Template.Common.Exceptions
{
    public class MpException : Exception, IMpException
    {
        public Enum Code { get; set; }
        public LogLevel Level { get; set; } = LogLevel.None;
        public List<MpFieldException> FieldExceptions { get; set; } = new List<MpFieldException>();

        public MpException(Enum code)
            : base(code.GetDescription()) => Code = code;

        public MpException(Enum code, string message)
            : base($"{code.GetDescription()} {Environment.NewLine}{message}") => Code = code;

        public MpException(Enum code, string message, LogLevel logLevel)
            : base($"{code.GetDescription()} {Environment.NewLine}{message}")
        {
            Code = code;
            Level = logLevel;
        }

        public void AddFieldException(Enum code) => FieldExceptions.Add(new MpFieldException(code, code.GetDescription()));

        public void AddFieldException(Enum code, string fieldName) => FieldExceptions.Add(new MpFieldException(code, fieldName));

        public void AddFieldException(Enum code, string fieldName, string value) => FieldExceptions.Add(new MpFieldException(code, fieldName, value));

        public void ThrowExceptionIfExists()
        {
            if (FieldExceptions.Any())
                throw this;
        }
    }

    public class MpFieldException : Exception, IMpException
    {
        public Enum Code { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }

        public MpFieldException(Enum code, string field) : base(code.GetDescription())
        {
            Code = code;
            Field = field;
        }

        public MpFieldException(Enum code, string field, string value) : base(code.GetDescription())
        {
            Code = code;
            Field = field;
            Value = value;
        }
    }

    public interface IMpException
    {
        Enum Code { get; set; }
    }
}
