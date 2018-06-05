//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Common
{
    ///// <summary>
    ///// Managed structure equivalent to IplImage
    ///// </summary>
    //[StructLayout(LayoutKind.Sequential)]
    //public struct MIplImage
    //{
    //    /// <summary>
    //    /// sizeof(IplImage) 
    //    /// </summary>
    //    public int NSize;
    //    /// <summary>
    //    /// version (=0)
    //    /// </summary>
    //    public int ID;
    //    /// <summary>
    //    /// Most of OpenCV functions support 1,2,3 or 4 channels 
    //    /// </summary>
    //    public int NChannels;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public int AlphaChannel;
    //    /// <summary>
    //    /// pixel depth in bits: IPL_DEPTH_8U, IPL_DEPTH_8S, IPL_DEPTH_16U, IPL_DEPTH_16S, IPL_DEPTH_32S, IPL_DEPTH_32F and IPL_DEPTH_64F are supported 
    //    /// </summary>
    //    public IplDepth Depth;

    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ColorModel0;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ColorModel1;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ColorModel2;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ColorModel3;

    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ChannelSeq0;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ChannelSeq1;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ChannelSeq2;
    //    /// <summary>
    //    /// ignored by OpenCV 
    //    /// </summary>
    //    public byte ChannelSeq3;

    //    /// <summary>
    //    /// 0 - interleaved color channels, 1 - separate color channels.
    //    /// cvCreateImage can only create interleaved images 
    //    /// </summary>
    //    public int DataOrder;
    //    /// <summary>
    //    /// 0 - top-left origin,
    //    /// 1 - bottom-left origin (Windows bitmaps style)
    //    /// </summary>
    //    public int Origin;
    //    /// <summary>
    //    /// Alignment of image rows (4 or 8).
    //    /// OpenCV ignores it and uses widthStep instead 
    //    /// </summary>
    //    public int Align;
    //    /// <summary>
    //    /// image width in pixels 
    //    /// </summary>
    //    public int Width;
    //    /// <summary>
    //    /// image height in pixels 
    //    /// </summary>
    //    public int Height;
    //    /// <summary>
    //    /// image ROI. when it is not NULL, this specifies image region to process 
    //    /// </summary>
    //    public IntPtr Roi;
    //    /// <summary>
    //    /// must be NULL in OpenCV 
    //    /// </summary>
    //    public IntPtr MaskROI;
    //    /// <summary>
    //    /// ditto
    //    /// </summary>
    //    public IntPtr ImageId;
    //    /// <summary>
    //    /// ditto 
    //    /// </summary>
    //    public IntPtr TileInfo;
    //    /// <summary>
    //    /// image data size in bytes
    //    /// (=image->height*image->widthStep in case of interleaved data)
    //    /// </summary>
    //    public int ImageSize;
    //    /// <summary>
    //    /// pointer to aligned image data 
    //    /// </summary>
    //    public IntPtr ImageData;
    //    /// <summary>
    //    /// size of aligned image row in bytes 
    //    /// </summary>
    //    public int WidthStep;

    //    /// <summary>
    //    /// border completion mode, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderMode0;
    //    /// <summary>
    //    /// border completion mode, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderMode1;
    //    /// <summary>
    //    /// border completion mode, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderMode2;
    //    /// <summary>
    //    /// border completion mode, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderMode3;

    //    /// <summary>
    //    /// border const, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderConst0;
    //    /// <summary>
    //    /// border const, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderConst1;
    //    /// <summary>
    //    /// border const, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderConst2;
    //    /// <summary>
    //    /// border const, ignored by OpenCV 
    //    /// </summary>
    //    public int BorderConst3;

    //    /// <summary>
    //    ///  pointer to a very origin of image data (not necessarily aligned) - it is needed for correct image deallocation 
    //    /// </summary>
    //    public IntPtr ImageDataOrigin;
    //}

    ///// <summary>
    ///// IPL_DEPTH
    ///// </summary>
    //public enum IplDepth : uint
    //{
    //    /// <summary>
    //    /// indicates if the value is signed
    //    /// </summary>
    //    IplDepthSign = 0x80000000,
    //    /// <summary>
    //    /// 1bit unsigned
    //    /// </summary>
    //    IplDepth_1U = 1,
    //    /// <summary>
    //    /// 8bit unsigned (Byte)
    //    /// </summary>
    //    IplDepth_8U = 8,
    //    /// <summary>
    //    /// 16bit unsigned
    //    /// </summary>
    //    IplDepth16U = 16,
    //    /// <summary>
    //    /// 32bit float (Single)
    //    /// </summary>
    //    IplDepth32F = 32,
    //    /// <summary>
    //    /// 8bit signed
    //    /// </summary>
    //    IplDepth_8S = (IplDepthSign | 8),
    //    /// <summary>
    //    /// 16bit signed
    //    /// </summary>
    //    IplDepth16S = (IplDepthSign | 16),
    //    /// <summary>
    //    /// 32bit signed 
    //    /// </summary>
    //    IplDepth32S = (IplDepthSign | 32),
    //    /// <summary>
    //    /// double
    //    /// </summary>
    //    IplDepth64F = 64
    //}

    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    [StructLayout(LayoutKind.Sequential)]
    public struct MIplImage
    {
        //    
        // 摘要:    
        //     sizeof(IplImage)    
        public int nSize;
        //    
        // 摘要:    
        //     version (=0)    
        public int ID;
        //    
        // 摘要:    
        //     Most of OpenCV functions support 1,2,3 or 4 channels    
        public int nChannels;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public int alphaChannel;
        //    
        // 摘要:    
        //     pixel depth in bits: IPL_DEPTH_8U, IPL_DEPTH_8S, IPL_DEPTH_16U, IPL_DEPTH_16S,    
        //     IPL_DEPTH_32S, IPL_DEPTH_32F and IPL_DEPTH_64F are supported    
        public IPL_DEPTH depth;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte colorModel0;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte colorModel1;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte colorModel2;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte colorModel3;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte channelSeq0;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte channelSeq1;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte channelSeq2;
        //    
        // 摘要:    
        //     ignored by OpenCV    
        public byte channelSeq3;
        //    
        // 摘要:    
        //     0 - interleaved color channels, 1 - separate color channels.  cvCreateImage    
        //     can only create interleaved images    
        public int dataOrder;
        //    
        // 摘要:    
        //     0 - top-left origin, 1 - bottom-left origin (Windows bitmaps style)    
        public int origin;

        // 摘要:    
        //     Alignment of image rows (4 or 8).  OpenCV ignores it and uses widthStep instead    
        public int align;
        //    
        // 摘要:    
        //     image width in pixels    
        public int width;
        //    
        // 摘要:    
        //     image height in pixels    
        public int height;
        //    
        // 摘要:    
        //     image ROI. when it is not NULL, this specifies image region to process    
        public IntPtr roi;
        //    
        // 摘要:    
        //     must be NULL in OpenCV    
        public IntPtr maskROI;
        //    
        // 摘要:    
        //     ditto    
        public IntPtr imageId;
        //    
        // 摘要:    
        //     ditto    
        public IntPtr tileInfo;
        //    
        // 摘要:    
        //     image data size in bytes (=image->height*image->widthStep in case of interleaved    
        //     data)    
        public int imageSize;
        //    
        // 摘要:    
        //     pointer to aligned image data  
        //[MarshalAs(UnmanagedType.SysUInt)]
        public IntPtr imageData;
        //    
        // 摘要:    
        //     size of aligned image row in bytes    
        public int widthStep;
        //    
        // 摘要:    
        //     border completion mode, ignored by OpenCV    
        public int BorderMode0;
        //    
        // 摘要:    
        //     border completion mode, ignored by OpenCV    
        public int BorderMode1;
        //    
        // 摘要:    
        //     border completion mode, ignored by OpenCV    
        public int BorderMode2;
        //    
        // 摘要:    
        //     border completion mode, ignored by OpenCV    
        public int BorderMode3;
        //    
        // 摘要:    
        //     border const, ignored by OpenCV    
        public int BorderConst0;
        //    
        // 摘要:    
        //     border const, ignored by OpenCV    
        public int BorderConst1;
        //    
        // 摘要:    
        //     border const, ignored by OpenCV    
        public int BorderConst2;
        //    
        // 摘要:    
        //     border const, ignored by OpenCV    
        public int BorderConst3;
        //    
        // 摘要:    
        //     pointer to a very origin of image data (not necessarily aligned) - it is    
        //     needed for correct image deallocation    
        public IntPtr imageDataOrigin;
    }

    public enum IPL_DEPTH:uint
    {
        // 摘要:    
        //     1bit unsigned    
        IPL_DEPTH_1U = 1,
        //    
        // 摘要:    
        //     8bit unsigned (Byte)    
        IPL_DEPTH_8U = 8,
        //    
        // 摘要:    
        //     16bit unsigned    
        IPL_DEPTH_16U = 16,
        //    
        // 摘要:    
        //     32bit float (Single)    
        IPL_DEPTH_32F = 32,
        //    
        // 摘要:    
        //     double    
        IPL_DEPTH_64F = 64,
        //    
        // 摘要:    
        //     indicates if the value is signed    
        IPL_DEPTH_SIGN = 0x80000000,
        //    
        // 摘要:    
        //     8bit signed    
        IPL_DEPTH_8S = IPL_DEPTH_SIGN | 8,
        //    
        // 摘要:    
        //     16bit signed    
        IPL_DEPTH_16S = IPL_DEPTH_SIGN | 16,
        //    
        // 摘要:    
        //     32bit signed    
        IPL_DEPTH_32S = IPL_DEPTH_SIGN | 32,
    }


    public class Cv
    {
        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        [DllImport("MyDll.dll")]
        public static extern IntPtr ReadImage(string fileName);

        [DllImport("MyDll.dll")]
        public static extern MIplImage ReadImage1(string fileName);

        [DllImport("MyDll.dll")]
        public static extern void ShowImage();

        [DllImport("MyDll.dll")]
        public static extern IntPtr FindCircleCenter();

        public static BitmapSource ReadBitmapImage(string fileName)
        {
            //ShowImage();
            //return null;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MIplImage)));
            //ptr = ReadImage(fileName);
            ptr = ReadImage(fileName);
            var n = Marshal.PtrToStructure(ptr, typeof(MIplImage));
            MIplImage m = (MIplImage)n;
            
            //Bitmap img = new Bitmap(m.width, m.height, m.widthStep, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m.imageData);
            BitmapSource bs = BitmapSource.Create(m.width, m.height, 96, 96, System.Windows.Media.PixelFormats.Rgb24, null, m.imageData, m.imageSize, m.widthStep);
            return bs;// BitmapToBitmapImage(img);
        }

        public static BitmapSource ReadBitmapImage1(string fileName)
        {
            System.Windows.Media.PixelFormat pixelFormat = System.Windows.Media.PixelFormats.Gray8;
            MIplImage m = ReadImage1(fileName);
            switch (m.nChannels)
            {
                case 3:
                    pixelFormat = System.Windows.Media.PixelFormats.Rgb24;
                    break;
                case 4:
                    pixelFormat = System.Windows.Media.PixelFormats.Rgba64;
                    break;
            }
            BitmapSource bs = BitmapSource.Create(m.width, m.height, 96, 96, pixelFormat, null, m.imageData, m.imageSize, m.widthStep);
            return bs;// BitmapToBitmapImage(img);
        }

        // Bitmap --> BitmapImage
        public static BitmapSource BitmapToBitmapImage(Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHbitmap();//从GDI+ Bitmap创建GDI位图对象
            BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                ptr,
                IntPtr.Zero,
                Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ptr); //release the HBitmap
            return bs;
        }


        // BitmapImage --> Bitmap
        public static Bitmap BitmapImageToBitmap(BitmapSource bitmapSource)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
            new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride); bmp.UnlockBits(data);
            return bmp;

        }
    }
}
