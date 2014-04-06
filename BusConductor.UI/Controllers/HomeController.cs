using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using BusConductor.UI.ViewModelMappers.Home;

namespace BusConductor.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var r = XmlReader.Create("http://coolcatcampers.blogspot.co.uk/atom.xml");
            //var blogFeed = SyndicationFeed.Load(r);
            //r.Close();

            //var viewModel = IndexViewModelMapper.Map(blogFeed);

            var viewModel = IndexViewModelMapper.Map(null);

            return View(viewModel);
        }
    }
}
