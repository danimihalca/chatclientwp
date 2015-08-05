#include "pch.h"
#include "RCChatClientListener.hpp"
#include "utils.h"

using namespace ChatClientRC;

RCChatClientListener::RCChatClientListener(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m) :
_c(c),
_d(d),
_m(m)
{
}

void RCChatClientListener::onConnected()
{
	_c();
}

void RCChatClientListener::onDisconnected()
{
	if (_d!=nullptr)
		_d();
}

void RCChatClientListener::onMessageReceived(int senderId, const std::string& message)
{
	//m_actualListener.onMessageReceived(message);
	//m_actualListener->onMessageReceived(ToPlatformString(message));
	if (_m != nullptr)
		_m(ToPlatformString(message));
}

RCChatClientListener::~RCChatClientListener()
{
}


void RCChatClientListener::onLoginFailed(const std::string& message)
{
	if (_m != nullptr)
		_m(ToPlatformString("FAILED"));
}
void RCChatClientListener::onConnectionError(){
	if (_m != nullptr)
		_m(ToPlatformString("ERROR"));
}
void RCChatClientListener::onLoginSuccessful(){
	if (_m != nullptr)
		_m(ToPlatformString("SUCCESS"));
}

void RCChatClientListener::onContactsReceived(const Contacts& contacts)
{

}

void RCChatClientListener::onContactOnlineStatusChanged(int contactId, bool isOnline)
{

}