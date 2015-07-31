#include "pch.h"
#include "ProxyChatClientListener.h"
#include "utils.h"

//#include "AbstractRTChatClientListener.h"

using namespace ChatClientRC;

ProxyChatClientListener::ProxyChatClientListener(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m) :
_c(c),
_d(d),
_m(m)
{
}

void ProxyChatClientListener::onConnected()
{
	_c();
}

void ProxyChatClientListener::onDisconnected()
{
	if (_d!=nullptr)
		_d();
}

void ProxyChatClientListener::onMessageReceived(const std::string& message)
{
	//m_actualListener.onMessageReceived(message);
	//m_actualListener->onMessageReceived(ToPlatformString(message));
	if (_m != nullptr)
		_m(ToPlatformString(message));
}

ProxyChatClientListener::~ProxyChatClientListener()
{
}


void ProxyChatClientListener::onLoginFailed(const std::string& message)
{
	if (_m != nullptr)
		_m(ToPlatformString("FAILED"));
}
void ProxyChatClientListener::onConnectionError(){
	if (_m != nullptr)
		_m(ToPlatformString("ERROR"));
}
void ProxyChatClientListener::onLoginSuccessfull(){
	if (_m != nullptr)
		_m(ToPlatformString("SUCCESS"));
}