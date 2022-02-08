namespace Dignite.SiteBuilding
{
    public static class SiteBuildingDbProperties
    {
        public static string DbTablePrefix { get; set; } = "sbd";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "SiteBuilding";
    }
}
