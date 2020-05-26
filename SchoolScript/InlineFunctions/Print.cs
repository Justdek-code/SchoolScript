using System;
using SchoolScript.AST;
using System.Collections.Generic;
using SchoolScript.EvaluatorClasses;


namespace SchoolScript.InlineFunctions
{
    public class Print
    {
        private VariablesHeap _variables;


        public Print(List<ICompound> arguments, VariablesHeap variables)
        {
            _variables = variables;
            PrintConsole(arguments);
        }

        private void PrintConsole(List<ICompound> arguments)
        {
            foreach (ICompound argument in arguments)
            {
                if (argument.Type == ASTType.VARIABLE_CALL)
                {
                    IVariableCall variableCall = (IVariableCall) argument;
                    string varName = variableCall.VariableName;
                    Variable varValue = _variables.GetVariable(varName);
                    PrintArgument(varValue.GetContent());
                }
                else
                {
                    PrintArgument(argument);
                }
            }
        }

        private void PrintArgument(ICompound argument)
        {
            if (argument.Type == ASTType.STRING)
            {
                string value =((IString) argument).StringValue;
                Console.WriteLine(value);
            }
            else if (argument.Type == ASTType.INTEGER)
            {
                int value = ((IInteger) argument).IntegerValue;
                Console.WriteLine(value);
            }
            else if (argument.Type == ASTType.MATH_OPERATION)
            {
                EvaluatorClasses.Math math = new EvaluatorClasses.Math(argument);
                int value = math.GetContent();
                Console.WriteLine(value);
            }
        }
    }
}