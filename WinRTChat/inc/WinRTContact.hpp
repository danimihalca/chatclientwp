#pragma once

namespace WinRTChat
{
	public ref class WinRTContact sealed
	{
	private:
		int m_id;
		Platform::String^ m_userName;
		Platform::String^ m_fullName;
		bool m_isOnline;

	public:
		WinRTContact();
		WinRTContact(int id, Platform::String^ userName, Platform::String^ fullName, bool isOnline);

		int GetId();
		void SetId(int id);

		Platform::String^ GetUserName();
		void SetUserName(Platform::String^ userName);

		Platform::String^ GetFullName();
		void SetFullName(Platform::String^ fullName);

		bool IsOnline();
		void SetOnline(bool isOnline);


	};
}