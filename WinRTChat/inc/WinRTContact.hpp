#pragma once

namespace WinRTChat
{
	public ref class WinRTContact sealed
	{
	public:
		property int id;
		property Platform::String^ userName;
		property Platform::String^ firstName;
		property Platform::String^ lastName;
		property unsigned char state;

	public:
		WinRTContact();
		WinRTContact(int id, Platform::String^ userName, Platform::String^ firstName, Platform::String^ lastName, unsigned char state);

	};
}