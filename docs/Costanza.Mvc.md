Features of Costanza.Mvc
=======

- [Attributes](#attributes)
    - IsAjaxRequestAttribute
- [Action results](#action-results)
    - AtomResult
    - CsvResult
    - ImageResult
- [Routing](#routing)
    - EnumNameConstraint
    - EnumValueConstraint
    - GuidConstraint
    - ValueListConstraint
- [HtmlHelper extensions](#htmlhelper-extensions)
    - UrlLink
    - ConfirmationNotice
- [UrlHelper extensions](#urlhelper-extensions)
    - RemoveParameters
    - SetParameters

Attributes
-------

### IsAjaxRequestAttribute

The `IsAjaxRequestAttribute` adds a boolean variable named `isAjax` to the parameters of your action. This boolean is `true` if the request is made with Ajax/XMLHTTP, and `false` otherwise. 
This is useful when you want to have one action method return two types of view, depending on whether the request was made with Ajax.

A simple example:

```C#
[IsAjaxRequest]
public ActionResult EditProduct( int id, bool isAjax )
{
    var product = ...; // Retrieve the product from the DB
    if( product == null ) return HttpNotFound();

    /* The isAjax variable was inserted by the IsAjaxRequest attribute.
     * Use this to send back a partial view. The partial view can
     * then be used by the calling JavaScript to render the edit form. */
    if( isAjax )
    {
        return PartialView( "EditForm", product );
    }

    return View( product );
}
```

If you don't like the default name of the parameter, there's an overload:

```C#
[IsAjaxRequest( "isXmlHttp" )]
public ActionResult EditProduct( int id, bool isXmlHttp )
{
    // Code
}
```


Action results
-------

### AtomResult

`AtomResult` is a custom `ActionResult` that renders items as an Atom feed. Create a `SyndicationFeed`, fill it with the items you want in your feed, and `AtomResult` does the rest.

You can find `SyndicationFeed` and related classes in the `System.ServiceModel.Syndication` namespace of .NET. This is the preferred way of creating feeds.

```C#
public ActionResult ArticleAtom()
{
    var articles = ...; // Retrieve articles from DB.
    var items = from a in articles
                select new SyndicationItem(
                    a.Title,
                    a.Contents,
                    new Uri( "http://site/articlelink" ),
                    a.Id.ToString(),
                    a.PostDate
                );
    var feed = new SyndicationFeed( "Feed title", "Feed description", new Uri( "http://site/feed" ), items );
    return new AtomResult( feed );
}
```

**Note**: Atom is similar to RSS. However, RSS isn't a standard and should therefore be avoided. Always use Atom where possible.

### CsvResult

`CsvResult` is a custom `ActionResult` that renders a data table to a CSV file. CSV files can be imported in Excel, so this can help provide a simple way export your data.

```C#
public ActionResult Csv()
{
    var dataTable = ...; // Retrieve a DataTable from the DB
    return new CsvResult( dataTable );
}
```

There's an overload if you want to override the column headers of your `DataTable`, for example for localization purpopes.

```C#
public ActionResult Csv()
{
    var dataTable = ...; // Retrieve a DataTable from the DB
    return new CsvResult( dataTable, "Column 1", "Column 2" );
}
```

**Note**: the `TextInfo.ListSeparator` property of the CurrentCulture is used to separate the columns.

### ImageResult

`ImageResult` is a custom `ActionResult` that can render an image from the local file system, or a byte stream from memory, 
with automatic support for HTTP 304 caching headers.

An example with an image on the local file system:

```C#
public ActionResult ImageAction()
{
    string virtualPath = "~/images/logo.png";
    return new ImageResult( virtualPath, "image/png" );
}
```

An example with a dynamically generated image using a stream:

```C#
public ActionResult ImageAction()
{
	using( var bitmap = new Bitmap( 125, 125 ) )
	{
		using( var g = Graphics.FromImage( bitmap ) )
		{
			// Do some drawing.

			var stream = new MemoryStream();
			bitmap.Save( stream, ImageFormat.Png );
			return new ImageResult( stream, "image/png" );
		}
	}
}
```

There's an overload to provide a custom last modified date (used for caching):

```C#
var result = new ImageResult( stream, "image/png", DateTime.Now.AddDays( -2 ) );
```


Routing
-------

If you want to ensure a route parameter only matches certain values, you can use a route constraints. Costanza contains a few that might come in handy.

### EnumNameConstraint

The `EnumNameConstraint` restricts the allowed values of a route parameter to the names of an enum. If the route parameter has a value that's not 
present as a name in the enum, the route is not matched.

```C#
/* Define an enum somewhere in your code. */
public enum ProductType : int
{
    Books,
    Magazines
}

/* Apply the constraint in the route definitions. */
routes.MapRoute( 
    "ProductRoute",
    "products/{type}",
    new { controller = "products", action = "bytype" },
    new { type = new EnumNameConstraint<ProductType>() } 
);

/* 
 * This request will match the ProductRoute: /products/magazines
 * This request will not match the ProductRoute: /products/movies
 */
```

### EnumValueConstraint

The `EnumValueConstraint` is similar to the `EnumNameConstraint`, but it restricts the allowed values of a route parameter to the *values* of an enum. 
If the route parameter has a value that's not present as a value in the enum, the route is not matched.

```C#
/* Define an enum somewhere in your code. */
public enum CarType : int
{
    Sedan = 1,
    Coupe = 2
}

/* Apply the constraint in the route definitions. */
routes.MapRoute( 
    "CarRoute",
    "cars/{type}",
    new { controller = "cars", action = "bytype" },
    new { type = new EnumValueConstraint<CarType>() } 
);

/* 
 * This request will match the CarRoute: /cars/1
 * This request will not match the CarRoute: /cars/3
 */
```

### GuidConstraint

The `GuidConstraint` restricts the allowed values of a route parameter to be a valid GUID. For example, if you use GUIDs to identify user profiles, this constraint
is the one to use.

```C#
/* Apply the constraint in the route definitions. */
routes.MapRoute( 
    "UserRoute",
    "users/{id}",
    new { controller = "users", action = "profile" },
    new { id = new GuidConstraint() } 
);

/* 
 * This request will match the UserRoute: /users/21ec2020-3aea-1069-a2dd-08002b30309d
 * This request will not match the UserRoute: /users/45ec2020-1069-a2dd-08002b30309d
 */
```

### ValueListConstraint

The `ValueListConstraint` restricts the allowed values of a route parameter to a fixed list of values.

```C#
/* Apply the constraint in the route definitions. */
routes.MapRoute( 
    "CarRoute",
    "cars/{brand}",
    new { controller = "cars", action = "bybrand" },
    new { brand = new ValueListConstraint( "bmw", "honda", "nissan") } 
);

/* 
 * This request will match the CarRoute: /cars/bmw
 * This request will not match the CarRoute: /cars/audi
 */
```


HtmlHelper extensions
-------

### UrlLink

`UrlLink` generates the HTML for a hyperlink to a URI. This helper method is created as a partner of the `ActionLink` method that's provided by ASP.NET MVC. `UrlLink` can link to a fixed URL, whereas `ActionLink` can only link to actions.

Action methods can be renamed, while URL's should stay the same as much as possible, so it's better to link directly to URL's than to action methods.

```Razor
@Html.UrlLink( "http://google.com", "Google" )
```

There's an overload for adding extra HTML attributes.

```Razor
@Html.UrlLink( "http://google.com", "Google", new { rel = "external" } )
```

It's a good practice to add all your application's URLs as extension methods of `UrlHelper`. This reduces the amount of maintenance when you're renaming action methods or moving them to another controller. `UrlLink` was built with this in mind.

Add your URLs to UrlHelper with extension methods:

```C#
public static string UserProfile( this UrlHelper helper, int userId )
{
    return helper.Action( "profile", "user", new { userid = userId } );
}

public static string EditUser( this UrlHelper helper, int userId )
{
    return helper.Action( "edit", "user", new { userid = userId } );
}
```

Then, use them in your view or controllers:

```Razor
@model User

@Html.UrlLink( Url.EditUser( Model.Id ), "Edit" )
```

### ConfirmationNotice

`ConfirmationNotice` generates a `div` HTML element containing a message that will be gone when the page is refreshed. 
Use this HTML extension when you want to show the user a notice after a form has been succcesfully posted.

The method checks the TempData dictionary for a key. The HTML and the message is only generated if that key is present. 
As usual with TempData, when the page is refreshed, the key is not present anymore and thus the messge is not shown.

In your action method that handles the form POST, add a key to TempData:

```C#
public ActionResult Edit( FormCollection form )
{
    // Process the form data
    // ...

    TempData.Add( "ShowNotice", true );
    return RedirectToAction( "Index" );
}
```

Then, call `ConfirmationNotice` with that key, in the view you're redirecting to.

```Razor
@Html.ConfirmationNotice( "ShowNotice", "Your data was successfully edited." )
```

When the form has been successfully processed, you'll see the message *"Your data was successfully edited."* in the view. Refresh the page, and it's gone again.

There's an overload for adding extra HTML attributes.

```Razor
@Html.ConfirmationNotice( "ShowNotice", "Your data was successfully edited.", new { @class = "notice notice-success" } )
```


UrlHelper extensions
-------

### RemoveParameters

Query string manipulation is usually annoying and can result in verbose code. 
`RemoveParameters` makes it easy to remove one or more parameters from the query string **of the current request**, while leaving the other parameters intact.

An example: the current request is `/products?price=50&category=2&page=3` and you want to link to the same URL, but without the category parameter.

```Razor
<a href="@Url.RemoveParameters( "category" )">Link without categorie</a>
```

Or remove multiple parameters:

```Razor
<a href="@Url.RemoveParameters( "category", "page" )">Link without category and page</a>
```

### SetParameters

Similar to `RemoveParameters`, `SetParameters` adds one or more parameters to the the query string **of the current request**.

An example: the current request is `/products?price=50` and you want to link to the same URL, but with two extra parameters.

```Razor
<a href="@Url.SetParameters( new { category = 10, page = 3 } )">Link with price, category and page</a>
```