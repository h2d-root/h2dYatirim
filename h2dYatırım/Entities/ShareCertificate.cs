namespace h2dYatırım.Entities
{
    public class ShareCertificate
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Difference { get; set; }
        public double BasePrice { get; set; }
        public double CeilingPrice { get; set; }
        public double DailyPrice { get; set;}
    }
}
