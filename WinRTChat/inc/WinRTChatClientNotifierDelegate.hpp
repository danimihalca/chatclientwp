#pragma once
#include "WinRTContact.hpp"
#include "WinRTUserDetails.hpp"

namespace WinRTChat
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageReceivedCallback(int senderId, Platform::String^ message);
	public delegate void onLoginFailedCallback(Platform::String^ message);
	public delegate void onConnectionErrorCallback();
	public delegate void onLoginSuccessfulCallback(WinRTUserDetails^ details);
	public delegate void onContactsReceivedCallback(const Platform::Array<WinRTContact^>^ contacts);
	public delegate void onContactStatusChangedCallback(int contactId, unsigned char state);

	public delegate void OnRemovedByContactCallback(int contactId);
	public delegate void OnAddContactResponseCallback(Platform::String^, bool accepted);
	public delegate bool OnAddingByContactCallback(Platform::String^);

	public ref class WinRTChatClientNotifierDelegate sealed
	{
	public:
		inline WinRTChatClientNotifierDelegate()
		{
		}

		property onConnectedCallback^ OnConnected;
		property onDisconnectedCallback^ OnDisconnected;
		property onMessageReceivedCallback^ OnMessageReceived;
		property onLoginFailedCallback^ OnLoginFailed;
		property onConnectionErrorCallback^ OnConnectionError;
		property onLoginSuccessfulCallback^ OnLoginSuccessful;
		property onContactsReceivedCallback^ OnContactsReceived;
		property onContactStatusChangedCallback^ OnContactStatusChanged;
		property OnRemovedByContactCallback^ OnRemovedByContact;
		property OnAddContactResponseCallback^ OnAddContactResponse;
		property OnAddingByContactCallback^ OnAddingByContact;
	};
}
