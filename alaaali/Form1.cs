using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using iTextSharp.text;
using NAudio.Wave;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;
using System.IO;
using System.IO.Compression;
namespace alaaali

{
    public partial class Form1 : Form
    {
        private List<Rectangle> selectedAreas = new List<Rectangle>();
        private bool isSelecting = false;
        private Color selectedColor;
        private bool colorSelected = false;
        
        private AudioRecorder audioRecorder;
        private AudioPlayer audioPlayer;
        private string audioFilePath;
        public Form1()
        {
            InitializeComponent();

            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;

            audioRecorder = new AudioRecorder();
            audioPlayer = new AudioPlayer();

            // Add buttons for recording and playing audio comments
            Button recordButton = new Button { Text = "Record", Location = new Point(10, 400) };
            Button stopButton = new Button { Text = "Stop", Location = new Point(100, 400) };
            Button playButton = new Button { Text = "Play", Location = new Point(190, 400) };

            recordButton.Click += recordButtonToolStripMenuItem_Click;
            stopButton.Click += stopButtonToolStripMenuItem_Click;
            playButton.Click += playButtonToolStripMenuItem_Click;

            this.Controls.Add(recordButton);
            this.Controls.Add(stopButton);
            this.Controls.Add(playButton);
            
            
            // Add a button for generating PDF reports
            Button pdfButton = new Button { Text = "Generate PDF", Location = new Point(280, 400) };
            pdfButton.Click += pdfButtonToolStripMenuItem_Click;
            this.Controls.Add(pdfButton);
        }
        private void pdfButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Compressed PDF file (*.pdf.gz)|*.pdf.gz";
            saveFileDialog.Title = "Save PDF Report";
            saveFileDialog.InitialDirectory = @"C:\"; // Update initial directory as needed
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string compressedFilePath = saveFileDialog.FileName;
                string uncompressedFilePath = Path.ChangeExtension(compressedFilePath, null); // Remove .gz extension

                CreatePdfReport(uncompressedFilePath); // Generate PDF report

                CompressFile(uncompressedFilePath, compressedFilePath); // Compress PDF file

                MessageBox.Show("PDF report generated and compressed successfully.");
            }
        }
        private void CompressFile(string inputFile, string outputFile)
        {
            // Check if the input file exists
            if (!File.Exists(inputFile))
            {
                MessageBox.Show("Input file not found.");
                return;
            }

            // Create the output directory if it doesn't exist
            string outputDirectory = Path.GetDirectoryName(outputFile);
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Compress the input file
            using (FileStream inputStream = File.OpenRead(inputFile))
            {
                using (FileStream outputStream = File.Create(outputFile))
                {
                    using (GZipStream compressStream = new GZipStream(outputStream, CompressionMode.Compress))
                    {
                        inputStream.CopyTo(compressStream);
                    }
                }
            }

            MessageBox.Show("File compressed successfully.");
        }

        
        private void compressButtonToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Prompt user to select file to compress
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.Title = "Select File to Compress";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string inputFile = openFileDialog.FileName;

                // Prompt user for output file path
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Compressed files (*.gz)|*.gz";
                saveFileDialog.Title = "Save Compressed File";
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(inputFile);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected output file path
                    string outputFile = saveFileDialog.FileName;

                    // Compress the file
                    CompressFile(inputFile, outputFile);
                }
            }
        }
        // private void pdfButtonToolStripMenuItem_Click(object sender, EventArgs e)
        // {
        //
        //     SaveFileDialog saveFileDialog = new SaveFileDialog();
        //     saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
        //     saveFileDialog.Title = "Save PDF Report";
        //     saveFileDialog.InitialDirectory = @"C:\";
        //     saveFileDialog.RestoreDirectory = true;
        //
        //     if (saveFileDialog.ShowDialog() == DialogResult.OK)
        //     {
        //         CreatePdfReport(saveFileDialog.FileName);
        //         MessageBox.Show("PDF report generated successfully.");
        //     }
        // }

        private void CreatePdfReport(string pdfFilePath)
        {
            using (FileStream stream = new FileStream(pdfFilePath, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();

                // Add a title to the PDF
                pdfDoc.Add(new Paragraph("Medical Report"));
                pdfDoc.Add(new Paragraph(" "));

                // Add images to the PDF
                if (pictureBox1.Image != null)
                {
                    AddImageToPdf(pdfDoc, pictureBox1.Image, "Original Image");
                }

                if (pictureBox2.Image != null)
                {
                    AddImageToPdf(pdfDoc, pictureBox2.Image, "Processed Image");
                }

                // Add audio comments to the PDF
                if (!string.IsNullOrEmpty(audioFilePath) && File.Exists(audioFilePath))
                {
                    pdfDoc.Add(new Paragraph($"Audio Comment: {audioFilePath}"));
                }

                pdfDoc.Close();
            }
        }

        private void AddImageToPdf(Document pdfDoc, Image image, string title)
        {
            pdfDoc.Add(new Paragraph(title));
            pdfDoc.Add(new Paragraph(" "));

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Png);
                iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.ToArray());
                pdfImage.ScaleToFit(pdfDoc.PageSize.Width - 50, pdfDoc.PageSize.Height - 150);
                pdfDoc.Add(pdfImage);
                pdfDoc.Add(new Paragraph(" "));
            }
        }
        private void recordButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Use SaveFileDialog to prompt the user for a filename and path
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "WAV file (*.wav)|*.wav";
            saveFileDialog.Title = "Save Audio As";
            saveFileDialog.InitialDirectory = @"C:\Users\alial\Desktop\audio";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the audio file path to the selected path
                audioFilePath = saveFileDialog.FileName;

                // Ensure the directory exists
                string directoryPath = Path.GetDirectoryName(audioFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Start recording the audio
                audioRecorder.StartRecording(audioFilePath);
                MessageBox.Show("Recording started...");
            }
        }

        private void stopButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
            audioRecorder.StopRecording();
            MessageBox.Show("Recording stopped.");
        }
        private void playButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(audioFilePath))
            {
                audioPlayer.Play(audioFilePath);
                MessageBox.Show("Playing recorded audio...");
            }
            else
            {
                MessageBox.Show("No recorded audio found.");
            }
        }


        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            {
                foreach (var area in selectedAreas)
                {
                    e.Graphics.DrawRectangle(pen, area);
                }
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isSelecting = false;
            if (selectedAreas.Count == 0) return;

            pictureBox1.Invalidate();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isSelecting = true;
            selectedAreas.Add(new Rectangle(e.X, e.Y, 0, 0));
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isSelecting) return;
            selectedAreas[selectedAreas.Count - 1] = new Rectangle(selectedAreas[selectedAreas.Count - 1].X,
                selectedAreas[selectedAreas.Count - 1].Y, e.X - selectedAreas[selectedAreas.Count - 1].X,
                e.Y - selectedAreas[selectedAreas.Count - 1].Y);
            pictureBox1.Invalidate();
        }

        private Color[] GetColormap(Color baseColor)
        {
            List<Color> colormap = new List<Color>();
            int gradientLevels = 10;
            for (int i = 0; i <= gradientLevels; i++)
            {
                int r = (int)(baseColor.R * (i / (double)gradientLevels));
                int g = (int)(baseColor.G * (i / (double)gradientLevels));
                int b = (int)(baseColor.B * (i / (double)gradientLevels));
                colormap.Add(Color.FromArgb(r, g, b));
            }

            return colormap.ToArray();
        }

        private Color[] GetDefaultColormap()
        {
            return new Color[]
            {
                Color.FromArgb(255, 0, 0), // Red
                Color.FromArgb(0, 255, 0), // Green
                Color.FromArgb(0, 0, 255), // Blue
                Color.FromArgb(255, 255, 0), // Yellow
                Color.FromArgb(0, 255, 255), // Cyan
                Color.FromArgb(255, 0, 255), // Magenta
            };
        }

        private void colorSelectedAreaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (selectedAreas.Count == 0 || pictureBox1.Image == null) return;

            Bitmap originalBmp = (Bitmap)pictureBox1.Image;
            Bitmap modifiedBmp = new Bitmap(originalBmp.Width, originalBmp.Height);

            using (Graphics g = Graphics.FromImage(modifiedBmp))
            {
                g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);

                Color[] colormap;
                if (colorSelected)
                {
                    colormap = GetColormap(selectedColor);
                }
                else
                {
                    colormap = GetDefaultColormap();
                }

                foreach (var area in selectedAreas)
                {
                    for (int x = area.X; x < area.X + area.Width; x++)
                    {
                        for (int y = area.Y; y < area.Y + area.Height; y++)
                        {
                            if (x < originalBmp.Width && y < originalBmp.Height)
                            {
                                Color originalColor = originalBmp.GetPixel(x, y);
                                double grayValue = (originalColor.R + originalColor.G + originalColor.B) / 3.0;

                                int index = (int)(grayValue / 255.0 * (colormap.Length - 1));
                                Color newColor = colormap[index];

                                int transparency = originalColor.A;
                                newColor = Color.FromArgb(transparency, newColor.R, newColor.G, newColor.B);

                                modifiedBmp.SetPixel(x, y, newColor);
                            }
                        }
                    }
                }
            }

            pictureBox2.Image = modifiedBmp;
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog1.FileName);

                int newWidth = pictureBox1.Width;
                int newHeight = pictureBox1.Height;
                float aspectRatio = (float)image.Width / image.Height;

                if (aspectRatio > 1)
                {
                    newHeight = (int)(newWidth / aspectRatio);
                }
                else
                {
                    newWidth = (int)(newHeight * aspectRatio);
                }

                Bitmap resizedImage = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                pictureBox1.Image = resizedImage;
            }
        }

        private void sepiaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sepia sp = new Sepia();
            pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
        }

        private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HueModifier hue = new HueModifier();
            pictureBox2.Image = hue.Apply((Bitmap)pictureBox1.Image);
        }

        private void rotateChannelsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            RotateChannels rc = new RotateChannels();
            pictureBox2.Image = rc.Apply((Bitmap)pictureBox1.Image);
        }

        private void invertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No image loaded.");
                return;
            }

            // Convert the image to 24bpp RGB format if it is not already in that format
            Bitmap sourceImage = (Bitmap)pictureBox1.Image;
            if (sourceImage.PixelFormat != PixelFormat.Format24bppRgb)
            {
                Bitmap convertedImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);
                using (Graphics g = Graphics.FromImage(convertedImage))
                {
                    g.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
                }

                sourceImage = convertedImage;
            }

            // Apply the Invert filter
            Invert invertFilter = new Invert();
            Bitmap invertedImage = invertFilter.Apply(sourceImage);

            // Display the result
            pictureBox2.Image = invertedImage;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Bitmap Image (*.bmp)|*.bmp";
                saveFileDialog.Title = "Save Image";
                saveFileDialog.InitialDirectory = @"C:\";
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the image: " + ex.Message);
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
                colorSelected = true;
            }
        }

        private void xRayScannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayscaleImage = grayscaleFilter.Apply((Bitmap)pictureBox1.Image);

            SobelEdgeDetector sobelEdgeDetector = new SobelEdgeDetector();
            Bitmap edgeDetectedImage = sobelEdgeDetector.Apply(grayscaleImage);

            pictureBox2.Image = edgeDetectedImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }

        private void openSecondImageToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog2.FileName);

                int newWidth = pictureBox2.Width;
                int newHeight = pictureBox2.Height;
                float aspectRatio = (float)image.Width / image.Height;

                if (aspectRatio > 1)
                {
                    newHeight = (int)(newWidth / aspectRatio);
                }
                else
                {
                    newWidth = (int)(newHeight * aspectRatio);
                }

                Bitmap resizedImage = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                pictureBox2.Image = resizedImage;
            }
        }
        private void searchImagesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
       
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderDialog.SelectedPath;

                    // Prompt the user for search criteria
                    SearchCriteriaForm criteriaForm = new SearchCriteriaForm();
                    if (criteriaForm.ShowDialog() == DialogResult.OK)
                    {
                        long minSize = criteriaForm.MinSize;
                        long maxSize = criteriaForm.MaxSize;
                        DateTime? minDate = criteriaForm.MinDate;
                        DateTime? maxDate = criteriaForm.MaxDate;

                        // Get all image files in the directory
                        string[] imageFiles = Directory.GetFiles(folderPath, "*.bmp", SearchOption.AllDirectories);

                        List<string> filteredFiles = new List<string>();

                        foreach (string file in imageFiles)
                        {
                            FileInfo fileInfo = new FileInfo(file);

                            // Filter by size
                            if (fileInfo.Length < minSize || fileInfo.Length > maxSize)
                                continue;

                            // Filter by last write time
                            if (minDate.HasValue && fileInfo.LastWriteTime < minDate.Value)
                                continue;
                            if (maxDate.HasValue && fileInfo.LastWriteTime > maxDate.Value)
                                continue;

                            filteredFiles.Add(file);
                        }

                        // Display the results
                        if (filteredFiles.Count > 0)
                        {
                            StringBuilder resultMessage = new StringBuilder("Found files:\n");
                            foreach (string filteredFile in filteredFiles)
                            {
                                resultMessage.AppendLine(filteredFile);
                            }

                            MessageBox.Show(resultMessage.ToString(), "Search Results");
                        }
                        else
                        {
                            MessageBox.Show("No files found matching the criteria.", "Search Results");
                        }
                    }
                }
            }
        }


              private void compareImagesToolStripMenuItem_Click_1(object sender, EventArgs e)
         {
             if (pictureBox1.Image == null || pictureBox2.Image == null)
             {
                 MessageBox.Show("Please load both images first.");
                 return;
             }

             if (pictureBox1.Image.Size != pictureBox2.Image.Size)
             {
                 MessageBox.Show("Images must have the same dimensions for comparison.");
                 return;
             }

             int differenceCount = 0;
             Bitmap firstBitmap = (Bitmap)pictureBox1.Image;
             Bitmap secondBitmap = (Bitmap)pictureBox2.Image;

             for (int x = 0; x < firstBitmap.Width; x++)
             {
                 for (int y = 0; y < firstBitmap.Height; y++)
                 {
                     Color firstColor = firstBitmap.GetPixel(x, y);
                     Color secondColor = secondBitmap.GetPixel(x, y);

                     if (firstColor != secondColor)
                     {
                         differenceCount++;
                     }
                 }
             }

             double differencePercentage = (double)differenceCount / (firstBitmap.Width * firstBitmap.Height) * 100;

             if (differencePercentage <= 5)
             {
                 MessageBox.Show("There is no significant difference between the images. No advance in treatment or disease detected.");
             }
             else if (differencePercentage <= 20)
             {
                 MessageBox.Show("There is a significant difference between the images. Possible advance in treatment or disease.");
             }
         }

         private void categorizeSeverityToolStripMenuItem_Click_1(object sender, EventArgs e)
         {
             if (selectedAreas.Count == 0 || pictureBox1.Image == null)
             {
                 MessageBox.Show("Please select an area and load an image first.");
                 return;
             }

             Bitmap image = (Bitmap)pictureBox1.Image;
             foreach (var area in selectedAreas)
             {
                 int sumIntensity = 0;
                 int pixelCount = 0;

                 for (int x = area.X; x < area.X + area.Width; x++)
                 {
                     for (int y = area.Y; y < area.Y + area.Height; y++)
                     {
                         if (x < image.Width && y < image.Height)
                         {
                             Color color = image.GetPixel(x, y);
                             int intensity = (color.R + color.G + color.B) / 3;
                             sumIntensity += intensity;
                             pixelCount++;
                         }
                     }
                 }

                 double averageIntensity = sumIntensity / (double)pixelCount;

                 string severity = "Unknown";
                 if (averageIntensity < 85) // Example threshold for severe
                 {
                     severity = "Severe";
                 }
                 else if (averageIntensity < 170) // Example threshold for intermediate
                 {
                     severity = "Intermediate";
                 }
                 else // Mild
                 {
                     severity = "Mild";
                 }

                 MessageBox.Show($"Selected area has an average intensity of {averageIntensity} and is categorized as {severity}.");
             }
         }

         private Bitmap CutSelectedArea(Bitmap originalImage, Rectangle selectedArea)
         {
             Bitmap croppedImage = new Bitmap(selectedArea.Width, selectedArea.Height);

             using (Graphics g = Graphics.FromImage(croppedImage))
             {
                 g.DrawImage(originalImage, new Rectangle(0, 0, selectedArea.Width, selectedArea.Height), 
                     selectedArea, GraphicsUnit.Pixel);
             }

             return croppedImage;
         }

         private void cutSelectedAreaToolStripMenuItem_Click_1(object sender, EventArgs e)
         {
             if (selectedAreas.Count == 0 || pictureBox1.Image == null) return;

             Bitmap originalImage = (Bitmap)pictureBox1.Image;

             // Assuming only the first selected area will be cut
             Rectangle selectedArea = selectedAreas[0];

             // Cut the selected area from the original image
             Bitmap croppedImage = CutSelectedArea(originalImage, selectedArea);

             // Display the cropped image in pictureBox2
             pictureBox2.Image = croppedImage;

             // Clear the selected areas list and update pictureBox1
             selectedAreas.Clear();
             pictureBox1.Invalidate();
         }

         private void AddCommentToImage(string comment)
         {
             if (pictureBox1.Image == null) return;

             Bitmap originalBmp = (Bitmap)pictureBox1.Image;
             Bitmap bmpWithComment = new Bitmap(originalBmp.Width, originalBmp.Height);

             using (Graphics g = Graphics.FromImage(bmpWithComment))
             {
                 g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);

                 using (Font myFont = new Font("Arial", 18))
                 {
                     PointF location = new PointF(10, 10); // Change location as needed
                     g.DrawString(comment, myFont, Brushes.Red, location);
                 }
             }

             pictureBox2.Image = bmpWithComment;
             pictureBox2.Invalidate();
         }
         private void addCommentToolStripMenuItem_Click_1(object sender, EventArgs e)
         {
          string comment = Prompt.ShowDialog("Enter comment:", "Add Comment");
             if (!string.IsNullOrEmpty(comment))
             {
                 AddCommentToImage(comment);
             }
         }


      
    }
}
