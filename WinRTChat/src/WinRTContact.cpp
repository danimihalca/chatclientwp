#include "WinRTContact.hpp"

namespace WinRTChat
{
	WinRTContact::WinRTContact()
	{
	}

	WinRTContact::WinRTContact(int id, Platform::String^ userName, Platform::String^ fullName, bool isOnline) :
		m_id(id),
		m_userName(userName),
		m_fullName(fullName),
		m_isOnline(isOnline)
	{
	}


	int WinRTContact::GetId()
	{
		return m_id;
	}

	void WinRTContact::SetId(int id)
	{
		m_id = id;
	}

	Platform::String^ WinRTContact::GetUserName()
	{
		return m_userName;
	}

	void WinRTContact::SetUserName(Platform::String^ userName)
	{
		m_userName = userName;
	}

	Platform::String^ WinRTContact::GetFullName()
	{
		return m_fullName;
	}

	void WinRTContact::SetFullName(Platform::String^ fullName)
	{
		m_fullName = fullName;
	}

	bool WinRTContact::IsOnline()
	{
		return m_isOnline;
	}

	void WinRTContact::SetOnline(bool isOnline)
	{
		m_isOnline = isOnline;
	}
}