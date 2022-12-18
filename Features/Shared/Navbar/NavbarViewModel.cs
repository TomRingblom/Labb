namespace Labb.Features.Shared.Navbar
{
	public class NavbarViewModel
	{
		public IEnumerable<PageData>? NavItems { get; set; }
		public Url? SignInUrl { get; set; }
		public Url? SignOutUrl { get; set; }
        public Url? MyProfileUrl { get; set; }
    }
}
