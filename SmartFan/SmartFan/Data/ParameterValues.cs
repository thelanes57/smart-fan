namespace SmartFan.Data
{
    public record ParameterValues
    {
        public double TarmValueC { get; set; }
        public double TarmValueF { get; set; }
        public int BarValueMGH { get; set; }
        public int BarValuePascal { get; set; }
        public int GigValue { get; set; }
    }
}
