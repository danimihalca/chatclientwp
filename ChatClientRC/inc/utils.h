#pragma once

#include <string>

std::string ToStdString(Platform::String^ s);

Platform::String^ ToPlatformString(const std::string& s);