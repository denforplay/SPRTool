using SPR.Client.Abstractions.Core;
using unvell.ReoGrid;

namespace SPR.Client.ViewModels.Management
{
    public class ManagementViewModel : ViewModelBase
    {
        public ReoGridControl ReoGrid { get; set; }
        
        public ManagementViewModel()
        {
            ReoGrid = new ReoGridControl();
            ReoGrid.AddWorksheet(ReoGrid.CreateWorksheet("QWEQWEQWE"));
        }
    }
}