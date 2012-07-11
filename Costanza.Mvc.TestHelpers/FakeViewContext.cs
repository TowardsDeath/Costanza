using System.IO;
using System.Web.Mvc;

using Moq;

namespace Costanza.Mvc.TestHelpers
{
    public static class FakeViewContext
    {
        public static ViewContext New( ViewDataDictionary viewData )
        {
            var controllerContext = FakeControllerContext.New();
            var view = new Mock<IView>();
            var tempData = new Mock<TempDataDictionary>();
            var writer = new Mock<TextWriter>();

            var viewContext = new Mock<ViewContext>( controllerContext, view.Object, viewData, tempData.Object, writer.Object );
            return viewContext.Object;
        }
    }
}