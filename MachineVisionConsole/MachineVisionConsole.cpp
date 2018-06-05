#include "stdafx.h"
#include<iostream>
#include<opencv2/opencv.hpp>
#include<vector>

using namespace std;
using namespace cv;

int g_CannyThred = 180, g_CannyP = 0, g_CannySize = 9, g_HoughThred = 105, g_HoughThick = 1;
int g_Blue = 255, g_Green = 255, g_Red = 0;
int g_nWay = 1;
int g_nHoughLineMax = 10, g_nHoughLineMin = 50;

int g_nDp = 0;
int g_nMinDist = 100;
int g_nMinRadius = 0, g_nMaxRadius = 0;

int g_SizeWidth = 13, g_SizeHeight = 13, g_SigmaX = 9, g_SigmaY = 105;

int main()
{
	//Mat srcImage = imread("E:\\Image\\1\\1.bmp");
	//imshow("【原图】", srcImage);

	Mat grayImage = imread("E:\\Image\\1\\1.bmp",0);
	//cvtColor(srcImage, grayImage, CV_BGR2GRAY);
	//GaussianBlur(grayImage, grayImage, Size(9, 9), 2, 2);

	//Mat cannyImage;
	vector<Vec3f> circles;

	namedWindow("【滚动条窗口】", 0);
	createTrackbar("dp", "【滚动条窗口】", &g_nDp, 100, 0);
	createTrackbar("MinDist", "【滚动条窗口】", &g_nMinDist, 100, 0);
	createTrackbar("CannyThred", "【滚动条窗口】", &g_CannyThred, 300, 0);
	createTrackbar("HoughThred", "【滚动条窗口】", &g_HoughThred, 255, 0);
	createTrackbar("Blue", "【滚动条窗口】", &g_Blue, 255, 0);
	createTrackbar("Green", "【滚动条窗口】", &g_Green, 255, 0);
	createTrackbar("Red", "【滚动条窗口】", &g_Red, 255, 0);
	createTrackbar("Bgr/Gray", "【滚动条窗口】", &g_nWay, 1, 0);
	createTrackbar("HoughThred", "【滚动条窗口】", &g_HoughThred, 200, 0);
	createTrackbar("MinRadius", "【滚动条窗口】", &g_nMinRadius, 100, 0);
	createTrackbar("MaxRadius", "【滚动条窗口】", &g_nMaxRadius, 100, 0);
	createTrackbar("HoughThick", "【滚动条窗口】", &g_HoughThick, 100, 0);

	createTrackbar("SizeWidth", "【滚动条窗口】", &g_SizeWidth, 50, 0);
	createTrackbar("SizeHeight", "【滚动条窗口】", &g_SizeHeight, 50, 0);
	createTrackbar("SigmaX", "【滚动条窗口】", &g_SigmaX, 200, 0);
	createTrackbar("SigmaY", "【滚动条窗口】", &g_SigmaY, 200, 0);

	char key;
	Mat dstImage;
	while (1)
	{
		//cvtColor(srcImage, grayImage, CV_BGR2GRAY);
		
		//GaussianBlur(grayImage, grayImage, Size(g_SizeWidth, g_SizeHeight), (double)g_SigmaX, (double)g_SigmaY);
		//medianBlur(grayImage, dstImage, g_SigmaX);
		blur(grayImage, dstImage, Size(g_SizeWidth, g_SizeWidth));
		threshold(dstImage, dstImage, g_SigmaY, 255, CV_THRESH_BINARY);
		

		//int scale = 1;
		//int delta = 0;
		//int ddepth = CV_16S;
		//Mat grad_x, grad_y;
		//Mat abs_grad_x, abs_grad_y;

		//Sobel(grayImage, grad_x, ddepth, 1, 0, 3, scale, delta, BORDER_DEFAULT);

		//Sobel(grayImage, grad_y, ddepth, 0, 1, 3, scale, delta, BORDER_DEFAULT);
		////![sobel]

		////![convert]
		//convertScaleAbs(grad_x, abs_grad_x);
		//convertScaleAbs(grad_y, abs_grad_y);
		////![convert]

		////![blend]
		///// Total Gradient (approximate)
		//addWeighted(abs_grad_x, 0.5, abs_grad_y, 0.5, 0, grayImage);

		HoughCircles(dstImage, circles, CV_HOUGH_GRADIENT, (double)((g_nDp + 1.5)), (double)g_nMinDist + 1
			, (double)g_CannyThred + 1, g_HoughThred + 1, g_nMinRadius, g_nMaxRadius);

		//HoughCircles(dstImage, circles, CV_HOUGH_GRADIENT, 3, 10, 200, 1000, 0, 0);

		//显示线段
		for (size_t i = 0; i < circles.size(); i++)
		{
			circle(dstImage, Point(cvRound(circles[i][0]), cvRound(circles[i][1])), cvRound(circles[i][2])
				, Scalar(g_Blue, g_Green, g_Red), g_HoughThick);
		}

		imshow("【处理后】", dstImage);

		key = waitKey(2000);
		if (key == 27)
			break;
	}

	return 0;
}