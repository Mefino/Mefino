using System;
using System.Collections.Generic;

namespace Mefino.CLI
{
    internal class ConsoleCommand
    {
        private readonly string longName;
        private readonly string shortName;

        private readonly string description;
        private readonly Action<string[]> action;

        public ConsoleCommand(string longName, string shortName, string description, Action<string[]> action)
        {
            this.longName = longName;
            this.shortName = shortName;
            this.description = description;
            this.action = action;
        }

        public bool IsMatch(string command)
        {
            if (command.StartsWith("-"))
                command = command.Substring(1, command.Length - 1);

            return (!string.IsNullOrEmpty(shortName) && string.Equals(command, shortName))
                || string.Equals(command, longName);
        }

        public void Invoke(string[] args) => action.Invoke(args);

        public override string ToString()
        {
            string ret = $"'{longName}' ";

            if (!string.IsNullOrEmpty(this.shortName))
                ret += $"/ '{shortName}' ";

            ret += $": {description}";

            return ret;
        }
    }
}
