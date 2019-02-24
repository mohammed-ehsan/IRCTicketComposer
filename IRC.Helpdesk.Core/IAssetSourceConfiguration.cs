namespace IRC.Helpdesk.Core
{
    public interface IAssetSourceConfiguration
    {
        int FirstRow { get; set; }
        int InventoryNumberIndex { get; set; }
        int SerialNumberIndex { get; set; }
        int LocationIndex { get; set; }
        int SubLocationIndex { get; set; }
        int MainCategoryIndex { get; set; }
        int SubCategoryIndex { get; set; }
    }
}