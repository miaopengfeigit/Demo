
//#include "stdafx.h"
//#include <opencv2/opencv.hpp>
//
//using namespace cv;
//
//#define DLL_API extern "C" _declspec(dllexport)
//
//extern "C" __declspec(dllexport) void Show()
//{
//	IplImage *img = cvLoadImage("1.bmp");
//	cvNamedWindow("Image:", 1);
//	cvShowImage("Image:", img);
//	cvWaitKey();
//	cvDestroyWindow("Image:");
//	cvReleaseImage(&img);
//	return;
//}
//
//extern "C" __declspec(dllexport) uchar* Imread()
//{
//	Mat img = imread("1.jpg");
//	return img.data;
//}
//
//DLL_API IplImage* _stdcall ReadImage()
//{
//	IplImage* src = cvLoadImage("1.jpg");
//	return src;
//}
//
//DLL_API void ShowImage(char* fileName)
//{
//	IplImage* src = cvLoadImage("1.jpg");
//	cvShowImage("Image:", src);
//}

//IplImage* Bitmap2IplImage(Bitmap* pBitmap)
//{
//	if (!pBitmap)
//		return NULL;
//
//	int w = pBitmap->GetWidth();
//	int h = pBitmap->GetHeight();
//
//	BitmapData bmpData;
//	Rect rect(0, 0, w, h);
//	pBitmap->LockBits(&rect, ImageLockModeRead, PixelFormat24bppRGB, &bmpData);
//	BYTE* temp = (bmpData.Stride>0) ? ((BYTE*)bmpData.Scan0) : ((BYTE*)bmpData.Scan0 + bmpData.Stride*(h - 1));
//
//	IplImage* pIplImg = cvCreateImage(cvSize(w, h), IPL_DEPTH_8U, 3);
//	if (!pIplImg)
//	{
//		pBitmap->UnlockBits(&bmpData);
//		return NULL;
//	}
//
//	memcpy(pIplImg->imageData, temp, abs(bmpData.Stride)*bmpData.Height);
//	pBitmap->UnlockBits(&bmpData);
//
//	//判断Top-Down or Bottom-Up
//	if (bmpData.Stride<0)
//		cvFlip(pIplImg, pIplImg);
//
//	return pIplImg;
//}
//
//// pBitmap 同样需要外部释放
//Bitmap*  IplImage2Bitmap(IplImage* pIplImg)
//{
//	if (!pIplImg)
//		return NULL;
//
//	Bitmap *pBitmap = new Bitmap(pIplImg->width, pIplImg->height, PixelFormat24bppRGB);
//	if (!pBitmap)
//		return NULL;
//
//	BitmapData bmpData;
//	Rect rect(0, 0, pIplImg->width, pIplImg->height);
//	pBitmap->LockBits(&rect, ImageLockModeWrite, PixelFormat24bppRGB, &bmpData);
//	BYTE *pByte = (BYTE*)bmpData.Scan0;
//
//	if (pIplImg->widthStep == bmpData.Stride) //likely
//		memcpy(bmpData.Scan0, pIplImg->imageDataOrigin, pIplImg->imageSize);
//
//	pBitmap->UnlockBits(&bmpData);
//	return pBitmap;
//}

#include "stdafx.h"

#define DLL_API extern "C" _declspec(dllexport)     
#include <opencv2\opencv.hpp>  
using namespace cv;
Mat Img;
DLL_API IplImage* _stdcall ReadImage(char* fileName)
{
	return cvLoadImage(fileName);
}

DLL_API IplImage _stdcall ReadImage1(char* fileName)
{
	//return cvLoadImage(fileName);
	Img = imread(fileName,-1);
	IplImage pBinary = IplImage(Img);
	return pBinary;
}

DLL_API void _stdcall ShowImage()
{
	/*IplImage* src = cvLoadImage("1.jpg");
	cvShowImage("Image", src);*/
	//imshow("m",Img);
}

DLL_API IplImage* _stdcall FindCircleCenter()
{
	Mat Img = imread("1.jpg");
	IplImage* pBinary = &IplImage(Img);
	IplImage* input = cvCloneImage(pBinary);
	return input;
}


