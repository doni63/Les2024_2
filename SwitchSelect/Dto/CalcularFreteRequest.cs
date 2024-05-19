namespace SwitchSelect.Dto
{
    public class CalcularFreteRequest
    {
        public string cep { get; set; }
    }

    public class FreteRequest
    {
        public From from { get; set; }
        public To to { get; set; }
        public Package package { get; set; }
    }

    public class From
    {
        public string postal_code { get; set; }
    }

    public class To
    {
        public string postal_code { get; set; }
    }

    public class Package
    {
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public double weight { get; set; }
    }

    public class FreteResponse
    {
        public string price { get; set; }
    }

}
