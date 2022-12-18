using Labb.Features.Shared;

namespace Labb.Features.User.MyProfile
{
    public class MyProfilePageViewModel : ContentViewModel<MyProfilePage>
    {
        public MyProfilePageViewModel(MyProfilePage currentPage) : base(currentPage)
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
