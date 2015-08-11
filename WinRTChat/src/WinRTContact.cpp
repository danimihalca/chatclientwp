#include "WinRTContact.hpp"

namespace WinRTChat
{
	WinRTContact::WinRTContact()
	{
	}

	WinRTContact::WinRTContact(int id, Platform::String^ userName, Platform::String^ firstName, Platform::String^ lastName, unsigned char state)
	{
		this->id = id;
		this->userName = userName;
		this->firstName = firstName;
		this->lastName = lastName;
		this->state = state;
	}

}