#include "pch.h"
#include "utils.h"

std::string ToStdString(Platform::String^ s)
{
	std::wstring stdwString(s->Begin());
	return std::string(stdwString.begin(), stdwString.end());
}