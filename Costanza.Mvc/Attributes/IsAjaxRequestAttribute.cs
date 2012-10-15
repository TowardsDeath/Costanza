using System.Web.Mvc;

namespace Costanza.Mvc
{
    /// <summary>
    /// Represents an attribute that adds a boolean parameter to an action, indicating whether the current request is made with XMLHTTP.
    /// </summary>
    public class IsAjaxRequestAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the name of the parameter that will be added to the action.
        /// </summary>
        public string ActionParameterName
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance and sets <see cref="ActionParameterName"/> to "isAjax".
        /// </summary>
        public IsAjaxRequestAttribute()
            : this( "isAjax" )
        {
        }

        /// <summary>
        /// Initializes a new instance, with the name of the parameter to add to the action.
        /// </summary>
        /// <param name="actionParameterName">The name of the parameter that will be added to the action.</param>
        public IsAjaxRequestAttribute( string actionParameterName )
        {
            this.ActionParameterName = actionParameterName;
        }

        /// <summary>
        /// Called by before the action method executes.
        /// </summary>
        /// <param name="filterContext">The context in which the filter operates.</param>
        /// <remarks>
        /// Adds a boolean parameter to the executing action, indicating if the current request is made with XMLHTTP.
        /// </remarks>
        public override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            filterContext.ActionParameters[ this.ActionParameterName ] = filterContext.HttpContext.Request.IsAjaxRequest();
        }

        /// <summary>
        /// Called after the action method executes.
        /// </summary>
        /// <remarks>
        /// Required by the IActionFilter interface, but without custom behaviour.
        /// </remarks>
        /// <param name="filterContext">The context in which the filter operates.</param>
        public override void OnActionExecuted( ActionExecutedContext filterContext )
        {
            return;
        }
    }
}