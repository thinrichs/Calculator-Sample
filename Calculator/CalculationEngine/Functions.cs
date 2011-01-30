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

    public class Function
    {
        public string Action { get; set; }
        public short ID { get; set; }

        // equals override is needed for unit tests
        public override bool Equals(object obj)
        {
            bool equals = ((obj as Function != null) &&
                           ((obj as Function).Action == Action) &&
                           ((obj as Function).ID == ID));

            return equals;
        }
    }
}