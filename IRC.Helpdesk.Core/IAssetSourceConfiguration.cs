namespace IRC.Helpdesk.Core
{
    public interface IAssetSourceConfiguration
    {
        int FirstRow { get; set; }
        int MakeIndex { get; set; }
        int ModelIndex { get; set; }
        int InventoryNumberIndex { get; set; }
        int UserIndex { get; set; }
        int DelivaryDateIndex { get; set; }

        void Update(JsonConfiguration configuration);
        void SaveChanges();
        JsonConfiguration GetJsonObject();
    }
}