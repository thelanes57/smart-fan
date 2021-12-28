namespace SmartFan.Data
{
    public class ChangeParameter
    {
        public double DutyCycle { get; set; }
        public int CurrentSpeed { get; set; }

        public static implicit operator ChangeParameter(double dutyCycle)
        {
            return new ChangeParameter
            {
                DutyCycle = dutyCycle,
                CurrentSpeed = 0
            };
        }
    }
}
