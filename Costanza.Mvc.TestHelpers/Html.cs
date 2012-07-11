using System.Web.Mvc;

using Moq;

namespace Costanza.Mvc.TestHelpers
{
    public static class Html
    {
        public static HtmlHelper<TModel> GetHtmlHelper<TModel>( TModel model )
        {
            var viewData = new ViewDataDictionary( model );

            var viewContext = FakeViewContext.New( viewData );
            var dataContainer = new Mock<IViewDataContainer>();
            dataContainer.Setup( dc => dc.ViewData ).Returns( viewData );

            return new HtmlHelper<TModel>( viewContext, dataContainer.Object );
        }
    }
}