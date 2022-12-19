using Labb.Features.Blog.BlogList;
using Labb.Features.Login;
using Labb.Features.Shared;
using Labb.Features.User.MyProfile;

namespace Labb.Features.Home;

[ContentType(
    DisplayName = "Home Page",
    Description = "This is the home page",
    GUID = "3AE03C90-2A61-42CC-A20B-BD36E0FBCF92")]
[AvailableContentTypes(Include = new[] { typeof(BlogListPage), typeof(MyProfilePage), typeof(LoginPage) })]
[ImageUrl("/icons/cms/pages/home-page.png")]
public class HomePage : LabbPageData
{
    
}