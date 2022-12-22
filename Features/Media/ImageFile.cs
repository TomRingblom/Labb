using EPiServer.Framework.DataAnnotations;

namespace Labb.Features.Media
{
    [ContentType(
        DisplayName = "ImageFile", 
        GUID = "13d776e9-17fc-4aba-8f67-705006331e9e", 
        Description = "Used for images of different file formats.")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,ico,gif,bmp,png,eps")]
    public class ImageFile : ImageData
    {
    }
}
