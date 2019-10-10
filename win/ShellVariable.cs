/*
    Strayex Shell for Windows
    Copyright © 2019 Daniel Strayker Nowak
    All rights reserved
 */


namespace strayex_shell_win
{ // Shell variable class, for storing data in shell:
    public class ShellVariable
    {
        private string Name; // Variable name,
        private string ValueS; // Variable value, string,
        private int ValueI; // Variable value, integer,

        // Constructors, for two types of values:
        public ShellVariable(string Name, string Value)
        { // Constructor for string value:
            this.Name = Name;
            ValueS = Value;
            ValueI = 0;
        }

        public ShellVariable(string Name, int Value)
        { // Constructor for integer value:
            this.Name = Name;
            ValueI = Value;
            ValueS = "";
        }

        // If given value is empty, then this variable is of another type:
        public string CheckType()
        {
            if (ValueS == "") return "int";
            else return "str";
        }

        // Returns name of var:
        public string GetName() { return Name; }

        // Returns value of var, string overload:
        public string GetVarString() { return ValueS; }

        // Returns value of var, integer overload:
        public int GetVarInt() { return ValueI; }
    }
}
