// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using System.IO;
// using AForge;
// using AForge.Imaging;
// using AForge.Imaging.Filters;
// using AForge.Imaging.ComplexFilters;
// using Image = System.Drawing.Image;
// using System.Drawing.Imaging;
// namespace alaaali
// {
//     public partial class Form1 : Form
//     {
//         private List<Rectangle> selectedAreas = new List<Rectangle>();
//         // private Rectangle selectedArea;
//         private bool isSelecting = false;
//         private Color selectedColor; // Class-level variable to store the selected color
//         private bool colorSelected = false; // Flag to indicate if a color has been selected
//         private Dictionary<string, Func<Color, Color>> colormaps = new Dictionary<string, Func<Color, Color>>();
//
//         public Form1()
//         {
//             InitializeComponent();
//             
//             pictureBox1.MouseDown += PictureBox1_MouseDown;
//             pictureBox1.MouseMove += PictureBox1_MouseMove;
//             pictureBox1.MouseUp += PictureBox1_MouseUp;
//             pictureBox1.Paint += PictureBox1_Paint; // Subscribe to the Paint event
//          }
//         
//         private void PictureBox1_Paint(object sender, PaintEventArgs e)
//         {
//             using (Pen pen = new Pen(Color.Black, 2)) // Use a red pen with a thickness of 2 pixels
//             {
//                 foreach (var area in selectedAreas)
//                 {
//                     e.Graphics.DrawRectangle(pen, area);
//                 }
//             }
//         }
//         
//      
//         
//
//         private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
//         {
//             isSelecting = false;
//             if (selectedAreas.Count == 0) return; // Ensure there is at least one selection
//
//             pictureBox1.Invalidate(); // Redraw the PictureBox to show the selection rectangles
//         }
//
//
//           private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
//         {
//             isSelecting = true;
//             selectedAreas.Add(new Rectangle(e.X, e.Y, 0, 0)); // Start selecting
//         }
//
//         private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
//         {
//             if (!isSelecting) return;
//             selectedAreas[selectedAreas.Count - 1] = new Rectangle(selectedAreas[selectedAreas.Count - 1].X, selectedAreas[selectedAreas.Count - 1].Y, e.X - selectedAreas[selectedAreas.Count - 1].X, e.Y - selectedAreas[selectedAreas.Count - 1].Y);
//             pictureBox1.Invalidate(); // Redraw the PictureBox to show the updated selection rectangle
//          }
//         private Color[] GetColormap(Bitmap originalBmp)
//         {
//             List<Color> colormap = new List<Color>();
//
//             // Calculate gradient levels based on the original image's RGB values
//             int gradientLevels = 5; // You can adjust this value for more or fewer levels
//
//             // Determine the range for each color channel (R, G, B)
//             int redRange = 255 / gradientLevels;
//             int greenRange = 255 / gradientLevels;
//             int blueRange = 255 / gradientLevels;
//
//             // Generate colors for each gradient level
//             for (int r = 0; r < gradientLevels; r++)
//             {
//                 for (int g = 0; g < gradientLevels; g++)
//                 {
//                     for (int b = 0; b < gradientLevels; b++)
//                     {
//                         // Create color based on current RGB values
//                         Color color = Color.FromArgb(
//                             Math.Min(255, r * redRange), 
//                             Math.Min(255, g * greenRange), 
//                             Math.Min(255, b * blueRange)
//                         );
//
//                         // Add the color to the colormap
//                         colormap.Add(color);
//                     }
//                 }
//             }
//
//             return colormap.ToArray();
//         }
//         
//         private void  colorSelectedAreaToolStripMenuItem_Click_1_1(object sender, EventArgs e)
//         {
//  
//     // Check if a selection has been made and a color has been selected
//     if (selectedAreas.Count == 0 || pictureBox1.Image == null) return;
//
//     // Create a copy of the original image from pictureBox1
//     Bitmap originalBmp = (Bitmap)pictureBox1.Image;
//     Bitmap modifiedBmp = new Bitmap(originalBmp.Width, originalBmp.Height);
//
//     // Create a graphics object to draw onto the modified bitmap
//     using (Graphics g = Graphics.FromImage(modifiedBmp))
//     {
//         // Draw the original image onto the modified bitmap with some transparency
//         g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);
//         g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.White)), 0, 0, modifiedBmp.Width, modifiedBmp.Height);
//
//         // Define a colormap based on the original image
//         Color[] colormap = GetColormap(originalBmp);
//
//         // Apply the colormap to the selected areas of the copied image
//         foreach (var area in selectedAreas)
//         {
//             for (int x = area.X; x < area.X + area.Width; x++)
//             {
//                 for (int y = area.Y; y < area.Y + area.Height; y++)
//                 {
//                     if (x < originalBmp.Width && y < originalBmp.Height)
//                     {
//                         Color originalColor = originalBmp.GetPixel(x, y);
//                         double grayValue = (originalColor.R + originalColor.G + originalColor.B) / 3.0; // Convert to grayscale
//
//                         // Interpolate between colormap colors based on grayscale value
//                         int index = (int)(grayValue / 255.0 * (colormap.Length - 1));
//                         Color newColor = colormap[index];
//
//                         // Apply transparency based on original pixel's alpha channel
//                         int transparency = originalColor.A;
//                         newColor = Color.FromArgb(transparency, newColor.R, newColor.G, newColor.B);
//
//                         modifiedBmp.SetPixel(x, y, newColor);
//                     }
//                 }
//             }
//         }
//     }
//
//     // Update pictureBox2 with the modified image
//     pictureBox2.Image = modifiedBmp;
// }
//
//      // private void textBox1_Click(object sender, EventArgs e)
//         // {
//         //     textBox1.Text = "dsdsddsds";
//         // }
//         private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             this.Close();
//         }
//
//         private void openToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//            
//             if (openFileDialog1.ShowDialog() == DialogResult.OK)
//             {
//                 Image image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
//         
//                 // Calculate the new width and height while maintaining the aspect ratio
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1) // Landscape orientation
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else // Portrait orientation
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 // Resize the image
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 // Display the resized image in PictureBox1
//                 pictureBox1.Image = resizedImage;
//             }
//         }
//
//         // private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
//         // {
//         //     Sepia sp = new Sepia();
//         //
//         //     pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
//         // }
//
//         private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             HueModifier hue = new HueModifier();
//             pictureBox2.Image = hue.Apply((Bitmap)pictureBox1.Image);
//             // hue.ApplyInPlace((Bitmap)pictureBox1.Image);
//         }
//
//        
//         private void rotateChannelsToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             RotateChannels rc = new RotateChannels();
//             pictureBox2.Image = rc.Apply((Bitmap)pictureBox1.Image);
//         }
//         
//         private void invertToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             if (pictureBox1.Image == null)
//             {
//                 MessageBox.Show("No image loaded.");
//                 return;
//             }
//  
//             // Convert the image to 24bpp RGB format if it is not already in that format
//             Bitmap sourceImage = (Bitmap)pictureBox1.Image;
//             if (sourceImage.PixelFormat != PixelFormat.Format24bppRgb)
//             {
//                 Bitmap convertedImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);
//                 using (Graphics g = Graphics.FromImage(convertedImage))
//                 {
//                     g.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
//                 }
//                 sourceImage = convertedImage;
//             }
//
//             // Apply the Invert filter
//             Invert invertFilter = new Invert();
//             Bitmap invertedImage = invertFilter.Apply(sourceImage);
//
//             // Display the result
//             pictureBox2.Image = invertedImage;
//         }
//         
//         private void saveToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             try
//             {
//                 // Prompt the user to choose a file name and location
//                 SaveFileDialog saveFileDialog = new SaveFileDialog();
//                 saveFileDialog.Filter = "Bitmap Image (*.bmp)|*.bmp"; // Filter for BMP files
//                 saveFileDialog.Title = "Save Image";
//                 saveFileDialog.InitialDirectory = @"C:\"; // Default directory
//                 saveFileDialog.RestoreDirectory = true; // Restore the directory after saving
//
//                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                 {
//                     // Save the image to the selected location
//                     pictureBox2.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show("An error occurred while saving the image: " + ex.Message);
//             }
//         }
//         private void colorToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//       
//             ColorDialog colorDialog = new ColorDialog();
//             if (colorDialog.ShowDialog() == DialogResult.OK)
//             {
//                 selectedColor = colorDialog.Color; // Store the selected color
//                 colorSelected = true; // Indicate that a color has been selected
//             }
//         }
//
//         private void xRayScannerToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//       
//             // Convert the image to grayscale
//             Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayscaleImage = grayscaleFilter.Apply((Bitmap)pictureBox1.Image);
//
//             // Apply the SobelEdgeDetector filter to enhance edges
//             SobelEdgeDetector sobelEdgeDetector = new SobelEdgeDetector();
//             Bitmap edgeDetectedImage = sobelEdgeDetector.Apply(grayscaleImage);
//
//             // Display the result
//             pictureBox2.Image = edgeDetectedImage;
//         }
//
//       
//         private void Form1_Load(object sender, EventArgs e)
//         {
//             // Ensure pictureBox1 starts empty
//             pictureBox1.Image = null;
//
//             // No default image is set here
//         }
//
//
//         private void sepiaToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             Sepia sp = new Sepia();
//
//             pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
//         { 
//             if (openFileDialog1.ShowDialog() == DialogResult.OK)
//             {
//                 Image image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
//         
//                 // Calculate the new width and height while maintaining the aspect ratio
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1) // Landscape orientation
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else // Portrait orientation
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 // Resize the image
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 // Display the resized image in PictureBox1
//                 pictureBox1.Image = resizedImage;
//             }
//
//         }
//
//
//       
//     }
// }


/////////////////
//
// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using System.IO;
// using AForge;
// using AForge.Imaging;
// using AForge.Imaging.Filters;
// using AForge.Imaging.ComplexFilters;
// using System.Drawing.Imaging;
//
// namespace alaaali
// {
//     public partial class Form1 : Form
//     {
//         private List<Rectangle> selectedAreas = new List<Rectangle>();
//         private bool isSelecting = false;
//         private Color selectedColor;
//         private bool colorSelected = false;
//         private Bitmap secondImage;
//         public Form1()
//         {
//             InitializeComponent();
//             
//             pictureBox1.MouseDown += PictureBox1_MouseDown;
//             pictureBox1.MouseMove += PictureBox1_MouseMove;
//             pictureBox1.MouseUp += PictureBox1_MouseUp;
//             pictureBox1.Paint += PictureBox1_Paint;
//          }
//
//         private void PictureBox1_Paint(object sender, PaintEventArgs e)
//         {
//             using (Pen pen = new Pen(Color.Black, 2))
//             {
//                 foreach (var area in selectedAreas)
//                 {
//                     e.Graphics.DrawRectangle(pen, area);
//                 }
//             }
//         }
//
//         private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
//         {
//             isSelecting = false;
//             if (selectedAreas.Count == 0) return;
//
//             pictureBox1.Invalidate();
//         }
//
//         private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
//         {
//             isSelecting = true;
//             selectedAreas.Add(new Rectangle(e.X, e.Y, 0, 0));
//         }
//
//         private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
//         {
//             if (!isSelecting) return;
//             selectedAreas[selectedAreas.Count - 1] = new Rectangle(selectedAreas[selectedAreas.Count - 1].X, selectedAreas[selectedAreas.Count - 1].Y, e.X - selectedAreas[selectedAreas.Count - 1].X, e.Y - selectedAreas[selectedAreas.Count - 1].Y);
//             pictureBox1.Invalidate();
//          }
//
//         private Color[] GetColormap(Color baseColor)
//         {
//             List<Color> colormap = new List<Color>();
//             int gradientLevels = 10;
//             for (int i = 0; i <= gradientLevels; i++)
//             {
//                 int r = (int)(baseColor.R * (i / (double)gradientLevels));
//                 int g = (int)(baseColor.G * (i / (double)gradientLevels));
//                 int b = (int)(baseColor.B * (i / (double)gradientLevels));
//                 colormap.Add(Color.FromArgb(r, g, b));
//             }
//             return colormap.ToArray();
//         }
//
//         private Color[] GetDefaultColormap()
//         {
//             return new Color[] {
//                 Color.FromArgb(255, 0, 0),   // Red
//                 Color.FromArgb(0, 255, 0),   // Green
//                 Color.FromArgb(0, 0, 255),   // Blue
//                 Color.FromArgb(255, 255, 0), // Yellow
//                 Color.FromArgb(0, 255, 255), // Cyan
//                 Color.FromArgb(255, 0, 255), // Magenta
//             };
//         }
//
//         private void  colorSelectedAreaToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (selectedAreas.Count == 0 || pictureBox1.Image == null) return;
//
//             Bitmap originalBmp = (Bitmap)pictureBox1.Image;
//             Bitmap modifiedBmp = new Bitmap(originalBmp.Width, originalBmp.Height);
//
//             using (Graphics g = Graphics.FromImage(modifiedBmp))
//             {
//                 g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);
//
//                 Color[] colormap;
//                 if (colorSelected)
//                 {
//                     colormap = GetColormap(selectedColor);
//                 }
//                 else
//                 {
//                     colormap = GetDefaultColormap();
//                 }
//
//                 foreach (var area in selectedAreas)
//                 {
//                     for (int x = area.X; x < area.X + area.Width; x++)
//                     {
//                         for (int y = area.Y; y < area.Y + area.Height; y++)
//                         {
//                             if (x < originalBmp.Width && y < originalBmp.Height)
//                             {
//                                 Color originalColor = originalBmp.GetPixel(x, y);
//                                 double grayValue = (originalColor.R + originalColor.G + originalColor.B) / 3.0;
//
//                                 int index = (int)(grayValue / 255.0 * (colormap.Length - 1));
//                                 Color newColor = colormap[index];
//
//                                 int transparency = originalColor.A;
//                                 newColor = Color.FromArgb(transparency, newColor.R, newColor.G, newColor.B);
//
//                                 modifiedBmp.SetPixel(x, y, newColor);
//                             }
//                         }
//                     }
//                 }
//             }
//
//             pictureBox2.Image = modifiedBmp;
//         }
//
//         private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             this.Close();
//         }
//
//         private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (openFileDialog1.ShowDialog() == DialogResult.OK)
//             {
//                 System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
//         
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1)
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 pictureBox1.Image = resizedImage;
//             }
//         }
//
//         private void sepiaToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             Sepia sp = new Sepia();
//             pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             HueModifier hue = new HueModifier();
//             pictureBox2.Image = hue.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void rotateChannelsToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             RotateChannels rc = new RotateChannels();
//             pictureBox2.Image = rc.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void invertToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             if (pictureBox1.Image == null)
//             {
//                 MessageBox.Show("No image loaded.");
//                 return;
//             }
//  
//             // Convert the image to 24bpp RGB format if it is not already in that format
//             Bitmap sourceImage = (Bitmap)pictureBox1.Image;
//             if (sourceImage.PixelFormat != PixelFormat.Format24bppRgb)
//             {
//                 Bitmap convertedImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);
//                 using (Graphics g = Graphics.FromImage(convertedImage))
//                 {
//                     g.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
//                 }
//                 sourceImage = convertedImage;
//             }
//
//             // Apply the Invert filter
//             Invert invertFilter = new Invert();
//             Bitmap invertedImage = invertFilter.Apply(sourceImage);
//
//             // Display the result
//             pictureBox2.Image = invertedImage;
//         }
//
//         private void saveToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             try
//             {
//                 SaveFileDialog saveFileDialog = new SaveFileDialog();
//                 saveFileDialog.Filter = "Bitmap Image (*.bmp)|*.bmp";
//                 saveFileDialog.Title = "Save Image";
//                 saveFileDialog.InitialDirectory = @"C:\";
//                 saveFileDialog.RestoreDirectory = true;
//
//                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                 {
//                     pictureBox2.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show("An error occurred while saving the image: " + ex.Message);
//             }
//         }
//
//         private void colorToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             ColorDialog colorDialog = new ColorDialog();
//             if (colorDialog.ShowDialog() == DialogResult.OK)
//             {
//                 selectedColor = colorDialog.Color;
//                 colorSelected = true;
//             }
//         }
//
//         private void xRayScannerToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayscaleImage = grayscaleFilter.Apply((Bitmap)pictureBox1.Image);
//
//             SobelEdgeDetector sobelEdgeDetector = new SobelEdgeDetector();
//             Bitmap edgeDetectedImage = sobelEdgeDetector.Apply(grayscaleImage);
//
//             pictureBox2.Image = edgeDetectedImage;
//         }
//
//         private void Form1_Load(object sender, EventArgs e)
//         {
//             pictureBox1.Image = null;
//            // pictureBox2.Image = null;
//         }
//         private void openSecondImageToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (openFileDialog2.ShowDialog() == DialogResult.OK)
//             {
//                 System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog2.FileName);
//         
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1)
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 pictureBox2.Image = resizedImage;
//             }
//         }
//         private void compareImagesToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (pictureBox1.Image == null || pictureBox2.Image == null)
//             {
//                 MessageBox.Show("Please load both images first.");
//                 return;
//             }
//
//             if (pictureBox1.Image.Size != pictureBox2.Image.Size)
//             {
//                 MessageBox.Show("Images must have the same dimensions for comparison.");
//                 return;
//             }
//
//             int differenceCount = 0;
//             Bitmap firstBitmap = (Bitmap)pictureBox1.Image;
//             Bitmap secondBitmap = (Bitmap)pictureBox2.Image;
//
//             for (int x = 0; x < firstBitmap.Width; x++)
//             {
//                 for (int y = 0; y < firstBitmap.Height; y++)
//                 {
//                     Color firstColor = firstBitmap.GetPixel(x, y);
//                     Color secondColor = secondBitmap.GetPixel(x, y);
//
//                     if (firstColor != secondColor)
//                     {
//                         differenceCount++;
//                     }
//                 }
//             }
//
//             double differencePercentage = (double)differenceCount / (firstBitmap.Width * firstBitmap.Height) * 100;
//
//             if (differencePercentage <= 5)
//             {
//                 MessageBox.Show("There is no significant difference between the images. No advance in treatment or disease detected.");
//                
//             }
//             else if (differencePercentage <= 20)
//             {
//                 MessageBox.Show("There is a significant difference between the images. Possible advance in treatment or disease.");
//             }
//             // else
//             // {
//             // MessageBox.Show("There is a minor difference between the images. Possible advance in treatment or disease.");
//             // }
//         }
//
//
//     
//     }
// }
///////////////
//
// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
// using System.IO;
// using AForge;
// using AForge.Imaging;
// using AForge.Imaging.Filters;
// using AForge.Imaging.ComplexFilters;
// using System.Drawing.Imaging;
//
// namespace alaaali
// {
//     public partial class Form1 : Form
//     {
//         private List<Rectangle> selectedAreas = new List<Rectangle>();
//         private bool isSelecting = false;
//         private Color selectedColor;
//         private bool colorSelected = false;
//         private Bitmap secondImage;
//
//         public Form1()
//         {
//             InitializeComponent();
//             
//             pictureBox1.MouseDown += PictureBox1_MouseDown;
//             pictureBox1.MouseMove += PictureBox1_MouseMove;
//             pictureBox1.MouseUp += PictureBox1_MouseUp;
//             pictureBox1.Paint += PictureBox1_Paint;
//         }
//
//         private void PictureBox1_Paint(object sender, PaintEventArgs e)
//         {
//             using (Pen pen = new Pen(Color.Black, 2))
//             {
//                 foreach (var area in selectedAreas)
//                 {
//                     e.Graphics.DrawRectangle(pen, area);
//                 }
//             }
//         }
//
//         private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
//         {
//             isSelecting = false;
//             if (selectedAreas.Count == 0) return;
//
//             pictureBox1.Invalidate();
//         }
//
//         private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
//         {
//             isSelecting = true;
//             selectedAreas.Add(new Rectangle(e.X, e.Y, 0, 0));
//         }
//
//         private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
//         {
//             if (!isSelecting) return;
//             selectedAreas[selectedAreas.Count - 1] = new Rectangle(selectedAreas[selectedAreas.Count - 1].X, selectedAreas[selectedAreas.Count - 1].Y, e.X - selectedAreas[selectedAreas.Count - 1].X, e.Y - selectedAreas[selectedAreas.Count - 1].Y);
//             pictureBox1.Invalidate();
//         }
//
//         private Color[] GetColormap(Color baseColor)
//         {
//             List<Color> colormap = new List<Color>();
//             int gradientLevels = 10;
//             for (int i = 0; i <= gradientLevels; i++)
//             {
//                 int r = (int)(baseColor.R * (i / (double)gradientLevels));
//                 int g = (int)(baseColor.G * (i / (double)gradientLevels));
//                 int b = (int)(baseColor.B * (i / (double)gradientLevels));
//                 colormap.Add(Color.FromArgb(r, g, b));
//             }
//             return colormap.ToArray();
//         }
//
//         private Color[] GetDefaultColormap()
//         {
//             return new Color[] {
//                 Color.FromArgb(255, 0, 0),   // Red
//                 Color.FromArgb(0, 255, 0),   // Green
//                 Color.FromArgb(0, 0, 255),   // Blue
//                 Color.FromArgb(255, 255, 0), // Yellow
//                 Color.FromArgb(0, 255, 255), // Cyan
//                 Color.FromArgb(255, 0, 255), // Magenta
//             };
//         }
//
//         private void colorSelectedAreaToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (selectedAreas.Count == 0 || pictureBox1.Image == null) return;
//
//             Bitmap originalBmp = (Bitmap)pictureBox1.Image;
//             Bitmap modifiedBmp = new Bitmap(originalBmp.Width, originalBmp.Height);
//
//             using (Graphics g = Graphics.FromImage(modifiedBmp))
//             {
//                 g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);
//
//                 Color[] colormap;
//                 if (colorSelected)
//                 {
//                     colormap = GetColormap(selectedColor);
//                 }
//                 else
//                 {
//                     colormap = GetDefaultColormap();
//                 }
//
//                 foreach (var area in selectedAreas)
//                 {
//                     for (int x = area.X; x < area.X + area.Width; x++)
//                     {
//                         for (int y = area.Y; y < area.Y + area.Height; y++)
//                         {
//                             if (x < originalBmp.Width && y < originalBmp.Height)
//                             {
//                                 Color originalColor = originalBmp.GetPixel(x, y);
//                                 double grayValue = (originalColor.R + originalColor.G + originalColor.B) / 3.0;
//
//                                 int index = (int)(grayValue / 255.0 * (colormap.Length - 1));
//                                 Color newColor = colormap[index];
//
//                                 int transparency = originalColor.A;
//                                 newColor = Color.FromArgb(transparency, newColor.R, newColor.G, newColor.B);
//
//                                 modifiedBmp.SetPixel(x, y, newColor);
//                             }
//                         }
//                     }
//                 }
//             }
//
//             pictureBox2.Image = modifiedBmp;
//         }
//
//         private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             this.Close();
//         }
//
//         private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (openFileDialog1.ShowDialog() == DialogResult.OK)
//             {
//                 System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
//         
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1)
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 pictureBox1.Image = resizedImage;
//             }
//         }
//
//         private void sepiaToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             Sepia sp = new Sepia();
//             pictureBox2.Image = sp.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void hueModifierToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             HueModifier hue = new HueModifier();
//             pictureBox2.Image = hue.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void rotateChannelsToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             RotateChannels rc = new RotateChannels();
//             pictureBox2.Image = rc.Apply((Bitmap)pictureBox1.Image);
//         }
//
//         private void invertToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             if (pictureBox1.Image == null)
//             {
//                 MessageBox.Show("No image loaded.");
//                 return;
//             }
//  
//             // Convert the image to 24bpp RGB format if it is not already in that format
//             Bitmap sourceImage = (Bitmap)pictureBox1.Image;
//             if (sourceImage.PixelFormat != PixelFormat.Format24bppRgb)
//             {
//                 Bitmap convertedImage = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format24bppRgb);
//                 using (Graphics g = Graphics.FromImage(convertedImage))
//                 {
//                     g.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
//                 }
//                 sourceImage = convertedImage;
//             }
//
//             // Apply the Invert filter
//             Invert invertFilter = new Invert();
//             Bitmap invertedImage = invertFilter.Apply(sourceImage);
//
//             // Display the result
//             pictureBox2.Image = invertedImage;
//         }
//
//         private void saveToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             try
//             {
//                 SaveFileDialog saveFileDialog = new SaveFileDialog();
//                 saveFileDialog.Filter = "Bitmap Image (*.bmp)|*.bmp";
//                 saveFileDialog.Title = "Save Image";
//                 saveFileDialog.InitialDirectory = @"C:\";
//                 saveFileDialog.RestoreDirectory = true;
//
//                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
//                 {
//                     pictureBox2.Image.Save(saveFileDialog.FileName, ImageFormat.Bmp);
//                 }
//             }
//             catch (Exception ex)
//             {
//                 MessageBox.Show("An error occurred while saving the image: " + ex.Message);
//             }
//         }
//
//         private void colorToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             ColorDialog colorDialog = new ColorDialog();
//             if (colorDialog.ShowDialog() == DialogResult.OK)
//             {
//                 selectedColor = colorDialog.Color;
//                 colorSelected = true;
//             }
//         }
//
//         private void xRayScannerToolStripMenuItem_Click(object sender, EventArgs e)
//         {
//             Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
//             Bitmap grayscaleImage = grayscaleFilter.Apply((Bitmap)pictureBox1.Image);
//
//             SobelEdgeDetector sobelEdgeDetector = new SobelEdgeDetector();
//             Bitmap edgeDetectedImage = sobelEdgeDetector.Apply(grayscaleImage);
//
//             pictureBox2.Image = edgeDetectedImage;
//         }
//
//         private void Form1_Load(object sender, EventArgs e)
//         {
//             pictureBox1.Image = null;
//             pictureBox2.Image = null;
//         }
//
//         private void openSecondImageToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (openFileDialog2.ShowDialog() == DialogResult.OK)
//             {
//                 System.Drawing.Image image = System.Drawing.Image.FromFile(openFileDialog2.FileName);
//         
//                 int newWidth = pictureBox1.Width;
//                 int newHeight = pictureBox1.Height;
//                 float aspectRatio = (float)image.Width / image.Height;
//         
//                 if (aspectRatio > 1)
//                 {
//                     newHeight = (int)(newWidth / aspectRatio);
//                 }
//                 else
//                 {
//                     newWidth = (int)(newHeight * aspectRatio);
//                 }
//         
//                 Bitmap resizedImage = new Bitmap(newWidth, newHeight);
//                 using (Graphics g = Graphics.FromImage(resizedImage))
//                 {
//                     g.DrawImage(image, 0, 0, newWidth, newHeight);
//                 }
//         
//                 pictureBox2.Image = resizedImage;
//             }
//         }
//
//         private void compareImagesToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (pictureBox1.Image == null || pictureBox2.Image == null)
//             {
//                 MessageBox.Show("Please load both images first.");
//                 return;
//             }
//
//             if (pictureBox1.Image.Size != pictureBox2.Image.Size)
//             {
//                 MessageBox.Show("Images must have the same dimensions for comparison.");
//                 return;
//             }
//
//             int differenceCount = 0;
//             Bitmap firstBitmap = (Bitmap)pictureBox1.Image;
//             Bitmap secondBitmap = (Bitmap)pictureBox2.Image;
//
//             for (int x = 0; x < firstBitmap.Width; x++)
//             {
//                 for (int y = 0; y < firstBitmap.Height; y++)
//                 {
//                     Color firstColor = firstBitmap.GetPixel(x, y);
//                     Color secondColor = secondBitmap.GetPixel(x, y);
//
//                     if (firstColor != secondColor)
//                     {
//                         differenceCount++;
//                     }
//                 }
//             }
//
//             double differencePercentage = (double)differenceCount / (firstBitmap.Width * firstBitmap.Height) * 100;
//
//             if (differencePercentage <= 5)
//             {
//                 MessageBox.Show("There is no significant difference between the images. No advance in treatment or disease detected.");
//             }
//             else if (differencePercentage <= 20)
//             {
//                 MessageBox.Show("There is a significant difference between the images. Possible advance in treatment or disease.");
//             }
//         }
//
//         private void categorizeSeverityToolStripMenuItem_Click_1(object sender, EventArgs e)
//         {
//             if (selectedAreas.Count == 0 || pictureBox1.Image == null)
//             {
//                 MessageBox.Show("Please select an area and load an image first.");
//                 return;
//             }
//
//             Bitmap image = (Bitmap)pictureBox1.Image;
//             foreach (var area in selectedAreas)
//             {
//                 int sumIntensity = 0;
//                 int pixelCount = 0;
//
//                 for (int x = area.X; x < area.X + area.Width; x++)
//                 {
//                     for (int y = area.Y; y < area.Y + area.Height; y++)
//                     {
//                         if (x < image.Width && y < image.Height)
//                         {
//                             Color color = image.GetPixel(x, y);
//                             int intensity = (color.R + color.G + color.B) / 3;
//                             sumIntensity += intensity;
//                             pixelCount++;
//                         }
//                     }
//                 }
//
//                 double averageIntensity = sumIntensity / (double)pixelCount;
//
//                 string severity = "Unknown";
//                 if (averageIntensity < 85) // Example threshold for severe
//                 {
//                     severity = "Severe";
//                 }
//                 else if (averageIntensity < 170) // Example threshold for intermediate
//                 {
//                     severity = "Intermediate";
//                 }
//                 else // Mild
//                 {
//                     severity = "Mild";
//                 }
//
//                 MessageBox.Show($"Selected area has an average intensity of {averageIntensity} and is categorized as {severity}.");
//             }
//         }
//
//        
//     }
// }
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;

namespace alaaali
{
    public partial class Form1 : Form
    {
        private List<Rectangle> selectedAreas = new List<Rectangle>();
        private bool isSelecting = false;
        private Color selectedColor;
        private bool colorSelected = false;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;
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
    }
}
