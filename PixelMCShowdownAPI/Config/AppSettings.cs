namespace PixelMCShowdownAPI.Config
{
    public class AppSettings
    {
        public String Secret { get; set; }
        public ELOConfig ELO { get; set; }
    }

    public class ELOConfig { 
        public int KFactor { get; set; }
        public int PointsPerRank { get; set; }
        public int ChangeCap { get; set; }
    }
}
