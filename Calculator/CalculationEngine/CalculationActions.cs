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
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == typeof (Function) && Equals((Function) obj));
        }

        public bool Equals(Function other)
        {
            return !ReferenceEquals(null, other) &&
                   (ReferenceEquals(this, other) || Equals(other.Action, Action) && other.ID == ID);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Action != null ? Action.GetHashCode() : 0)*397) ^ ID.GetHashCode();
            }
        }
    }
}