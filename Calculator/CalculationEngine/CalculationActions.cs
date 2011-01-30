namespace CalculationEngine
{
    public class CalculationActions
    {
        public static Function Add = new Function {Action = "+", ID = 0};
        public static Function Subtract = new Function {Action = "-", ID = 1};
        public static Function Divide = new Function {Action = "/", ID = 2};
        public static Function Multiply = new Function {Action = "*", ID = 3};

        public static Function[] Functions = {Add, Subtract, Divide, Multiply};
    }
}