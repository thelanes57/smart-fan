namespace SmartFan.Data
{
    public record ParameterValues
    {
        public double TermValueC { get; set; }
        public double TermValueF { get; set; }
        public int BarValueMGH { get; set; }
        public int BarValuePascal { get; set; }
        public int HygrValue { get; set; }
    }
}
