using System;

namespace SelfService.Code
{
    class BooleanFormatter : ICustomFormatter, IFormatProvider
    {
        public object GetFormat(Type formatType) {
            if (formatType == typeof(ICustomFormatter)) {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider) {
            if (arg == null) {
                return string.Empty;
            }

            bool value = (bool)arg;
            switch (format ?? string.Empty) {
                case "YesNo": {
                        return (value) ? "Yes" : "No";
                    }
                case "OnOff": {
                        return (value) ? "On" : "Off";
                    }
                default: {
                        return "test";//true/false
                    }
            }
        }
    }
}
