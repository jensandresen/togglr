using System;
using System.Text.RegularExpressions;

namespace Togglr.Files
{
    public class ToggleValue
    {
        private const string Pattern = @"^\s*(\w+)\s*\=\s*(on|off)\s*(\#.*)?$";

        public ToggleValue(string name, bool isEnabled)
        {
            Name = name;
            IsEnabled = isEnabled;
        }

        public string Name;
        public bool IsEnabled;

        protected bool Equals(ToggleValue other)
        {
            return string.Equals(Name, other.Name) && IsEnabled.Equals(other.IsEnabled);
        }

        public override bool Equals(object obj)
        {
            var other = obj as ToggleValue;

            if (other == null)
            {
                return false;
            }

            return Equals((ToggleValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null
                    ? Name.GetHashCode()
                    : 0)*397) ^ IsEnabled.GetHashCode();
            }
        }

        public static bool operator ==(ToggleValue left, ToggleValue right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ToggleValue left, ToggleValue right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return Name + "=" + (IsEnabled ? "on" : "off");
        }

        public static bool IsValid(string text)
        {
            return Regex.IsMatch(text, Pattern, RegexOptions.IgnoreCase);
        }

        public static bool TryParse(string text, out ToggleValue result)
        {
            result = null;
            var isValid = IsValid(text);

            if (isValid)
            {
                var items = Regex.Split(text, Pattern, RegexOptions.IgnoreCase);

                var name = items[1];
                var state = "on".Equals(items[2], StringComparison.InvariantCultureIgnoreCase);

                result = new ToggleValue(name, state);
            }

            return isValid;
        }
    }
}