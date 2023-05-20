using System.Data;

namespace Calculator.MVVM.Logic
{
    public static class CalculatorLogic
    {
        public static double Calculate(string expression)
        {
            var dtCalculate = new DataTable();
            var resultExpression = dtCalculate.Compute(expression, "");
            _ = double.TryParse(resultExpression?.ToString(), out double result);
            return result;
        }
        public static bool IsOperator(char item)
        {
            char[] operators = { '(', ')', '^', '/', '*', '+', '-' };
            return operators.Contains(item);
        }
        public static bool IsOperatorValidInStart(char item)
        {
            char[] operators = { '(', '-' };
            return operators.Contains(item);
        }
        public static bool IsOperatorValidInEnd(char item)
        {
            char[] operators = { '(', ')', '^', '/', '*', '+', '-' };
            return operators.Contains(item);
        }
    }
}