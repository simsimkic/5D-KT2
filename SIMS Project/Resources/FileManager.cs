using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace SIMS_Project.Resources
{
    public class FileManager
    {
        public FileManager()
        {

        }

        public List<string> BrowseImages()
        {
            List<string> images = new List<string>();

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Add images";
            openFile.Filter = "Image files|*.jpg; *.jpeg; *.png; *.gif;";
            openFile.Multiselect = true;
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFile.ShowDialog() == true)
            {
                images.AddRange(openFile.FileNames);
            }

            return images;
        }

        // Id should be id of the object which image belongs to (example: AccommodaionId)
        public List<string> UploadImages(List<string> sources, string resourcePath, int id)
        {
            List<string> images = new List<string>();
            string destinationDirectory = Path.Combine(resourcePath, id.ToString());

            try
            {
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                
                foreach (string source in sources)
                {
                    string destination = Path.Combine(destinationDirectory, Path.GetFileNameWithoutExtension(source) + "-" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + Path.GetExtension(source));
                    File.Copy(source, destination, false);
                    images.Add(Path.GetFileName(destination));
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("Upload error. File not found\nDetails:\n" + exception);
            }

            return images;
        }

    }
}
