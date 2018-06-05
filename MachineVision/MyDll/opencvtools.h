
#ifndef OPENCVTOOLS_H
#define OPENCVTOOLS_H

using namespace System::Drawing;
using namespace System::Runtime::InteropServices;
using namespace System::Drawing::Imaging;

IplImage* Bitmap2IplImage(Bitmap^ pBitmap);
Bitmap^  IplImage2Bitmap(IplImage* pIplImg);
#endif