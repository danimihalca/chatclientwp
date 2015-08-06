#include "RCContact.hpp"

using namespace ChatClientRC;

RCContact::RCContact()
{
}

RCContact::RCContact(int id, Platform::String^ userName, Platform::String^ fullName, bool isOnline):
m_id(id),
m_userName(userName),
m_fullName(fullName),
m_isOnline(isOnline)
{
}


int RCContact::GetId()
{
	return m_id;
}

void RCContact::SetId(int id)
{
	m_id = id;
}

Platform::String^ RCContact::GetUserName()
{
	return m_userName;
}

void RCContact::SetUserName(Platform::String^ userName)
{
	m_userName = userName;
}

Platform::String^ RCContact::GetFullName()
{
	return m_fullName;
}

void RCContact::SetFullName(Platform::String^ fullName)
{
	m_fullName = fullName;
}

bool RCContact::IsOnline()
{
	return m_isOnline;
}

void RCContact::SetOnline(bool isOnline)
{
	m_isOnline = isOnline;
}