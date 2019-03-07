using IRC.Helpdesk.Core.POCOs;

namespace IRC.Helpdesk.Core
{
    public interface IMessageComposer
    {
        string ComposeTicket(string mainCategory, string secondaryCategory, string details);
        string ComposeSetupTicket(AssetTicket asset);
        string ComposeKACEMSOfficeTicket(AssetTicket asset);
    }
}
