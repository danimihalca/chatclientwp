#pragma once

namespace WinRTChat
{
	public ref class WinRTUserDetails sealed
	{
	public:
		property int id;
		property Platform::String^ firstName;
		property Platform::String^ lastName;

	public:
		WinRTUserDetails()
		{
		}

		WinRTUserDetails(int id, Platform::String^ firstName, Platform::String^ lastName)
		{
			this->id = id;
			this->firstName = firstName;
			this->lastName = lastName;
		}
	};
}