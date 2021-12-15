namespace SmartFan.Data
{
    public record ParameterValues
    {
        public double TarmValueC { get; set; }
        public double TarmValueF { get; set; }
        public int BarValue { get; set; }
        public int GidValue { get; set; }
    }
}
