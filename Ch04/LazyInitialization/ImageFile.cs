namespace LazyInitialization
{
    public class ImageFile
    {
        private string fileName;
        private object _loadImage;

        public ImageFile(string fileName)
        {
            this.fileName = fileName;
        }

        public object GetImage()
        {
            if (_loadImage == null)
            {
                LoadImageFromDisk();
            }

            return _loadImage;
        }
        
        public object LoadImageFromDisk()
        {
            this._loadImage = $"File {this.fileName} loaded from Disk";
            return _loadImage;
        }
    }
}