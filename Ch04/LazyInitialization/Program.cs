using System;
using System.Threading;

namespace LazyInitialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Lazy<ImageFile> imageFile = new Lazy<ImageFile>(() => new ImageFile("test"));
            var image = imageFile.Value.GetImage();
            Console.WriteLine(image);
            
            Func<object> imageFileFunc = new Func<object>(() => {
                var obj = new ImageFile("test_2");
                return obj.LoadImageFromDisk(); });
            Lazy<object> lazyImage = new Lazy<object>(imageFileFunc);
            var imageSecond = lazyImage.Value;
            Console.WriteLine(imageSecond);
            
            // Smallest memory footprint
            object imageThird = null;
            LazyInitializer.EnsureInitialized(ref imageThird, () =>
            {
                var obj = new ImageFile("test_3");
                return obj.LoadImageFromDisk();
            });
            Console.WriteLine(imageThird);
        }
    }
}