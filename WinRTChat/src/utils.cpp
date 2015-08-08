#include "utils.h"

std::string ToStdString(Platform::String^ s)
{
	std::wstring stdwString(s->Begin());
	return std::string(stdwString.begin(), stdwString.end());
}


Platform::String^ ToPlatformString(const std::string& s)
{
	std::wstring wString(s.begin(), s.end());
	return ref new Platform::String(wString.c_str());
}