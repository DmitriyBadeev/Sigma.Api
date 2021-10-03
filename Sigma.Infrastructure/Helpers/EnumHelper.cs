using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Sigma.Infrastructure.Helpers
{
    public class EnumHelper
    {
        public static string GetEnumName<TEnum>(TEnum enumerator)
            where TEnum : Enum
        {
            var enumType = enumerator.GetType();

            var enumName = ((DescriptionAttribute)enumType
                .GetMembers()
                .FirstOrDefault(x => x.Name == enumerator.ToString())?
                .GetCustomAttributes()
                .FirstOrDefault(x => x is DescriptionAttribute))?.Description;

            return enumName;
        }
    }
}
