#pragma once

namespace ChatClientRC
{
	public delegate void onConnectedCallback();
	public delegate void onDisconnectedCallback();
	public delegate void onMessageCallback(int senderId, Platform::String^ message);
	public delegate void onLoginFailedCallback(Platform::String^ message);
	public delegate void onConnectionErrorCallback();
	public delegate void onLoginSuccessfulCallback();
	//public delegate void onContactsReceivedCallback(Contacts^ contacts);
	public delegate void onContactOnlineStatusChangedCallback(int contactId, bool isOnline);

	public ref class RCChatClientNotifier sealed
	{
	public:
		RCChatClientNotifier();

		property onConnectedCallback^ OnConnected;
		property onDisconnectedCallback^ OnDisconnected;
		property onMessageCallback^ OnMessage;
		property onLoginFailedCallback^ OnLoginFailed;
		property onConnectionErrorCallback^ OnConnectionError;
		property onLoginSuccessfulCallback^ OnLoginSuccessful;
		//property onContactsReceived(const Contacts& contacts);
		property onContactOnlineStatusChangedCallback^ OnContactOnlineStatusChanged;
	};
}
