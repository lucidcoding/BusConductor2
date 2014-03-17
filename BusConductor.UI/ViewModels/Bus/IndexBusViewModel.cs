using System;

namespace BusConductor.UI.ViewModels.Bus
{
    public class IndexBusViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Overview { get; set; }
        public string MainImageUrl { get; set; }
    }
}