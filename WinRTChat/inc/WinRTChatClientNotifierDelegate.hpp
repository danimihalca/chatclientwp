#pragma once
#include "WinRTContact.hpp"
#include "WinRTUserDetails.hpp"

namespace WinRTChat
{
	public delegate void onDisconnectedCallback();
	public delegate void onMessageReceivedCallback(int senderId, Platform::String^ message);
	public delegate void onLoginFailedCallback(unsigned char  reason);
	public delegate void onConnectionErrorCallback();
	public delegate void onLoginSuccessfulCallback(WinRTUserDetails^ details);
	public delegate void onContactsReceivedCallback(const Platform::Array<WinRTContact^>^ contacts);
	public delegate void onContactStateChangedCallback(int contactId, unsigned char state);

	public delegate void onRemovedByContactCallback(int contactId);
	public delegate void onAddContactResponseCallback(Platform::String^, unsigned char status);
	public delegate bool onAddRequestCallback(Platform::String^);

	public delegate void onRegisterUpdateResponseCallback(unsigned char status);

	public ref class WinRTChatClientNotifierDelegate sealed
	{
	public:
		inline WinRTChatClientNotifierDelegate()
		{
		}

		property onDisconnectedCallback^ OnDisconnected;
		property onMessageReceivedCallback^ OnMessageReceived;
		property onLoginFailedCallback^ OnLoginFailed;
		property onConnectionErrorCallback^ OnConnectionError;
		property onLoginSuccessfulCallback^ OnLoginSuccessful;
		property onContactsReceivedCallback^ OnContactsReceived;
		property onContactStateChangedCallback^ OnContactStateChanged;
		property onRemovedByContactCallback^ OnRemovedByContact;
		property onAddContactResponseCallback^ OnAddContactResponse;
		property onAddRequestCallback^ OnAddRequest;
		property onRegisterUpdateResponseCallback^ OnRegisterUpdateResponse;
	};
}
