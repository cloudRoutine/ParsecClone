// StructReader.h

#pragma once

#include <stdio.h>
#include <string.h>

using namespace System;
using namespace std;

namespace ParserClone {
namespace Native {

	public ref class StructReader abstract sealed
	{

	public:
		generic <typename T> static array<T>^ Read(array<System::Byte>^ data, int objectSize, bool networkOrder)
		{
			int numObjects = data->Length / objectSize;

			array<T>^ resultList = gcnew array<T>(numObjects);

			pin_ptr<System::Byte> src = &data[0];
			
			if(networkOrder){
				int count = 0;
				for(int i = numObjects - 1; i >= 0 ; i--){
					T value;
					pin_ptr<T> dst = &value;

					memcpy((void*)dst, (void*)(src + (objectSize * i)), sizeof(T));

					resultList[count++] = value;
				}
			}
			else{
				for(int i = 0; i < numObjects; i++){
					T value;
					pin_ptr<T> dst = &value;

					memcpy((void*)dst, (void*)(src + (objectSize * i)), sizeof(T));

					resultList[i] = value;
				}
			}

			return resultList;
		}
	}; 
}
}