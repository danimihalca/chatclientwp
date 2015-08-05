#include "pch.h"
#include "ChatClientRtC.h"
#include "utils.h"
#include <ChatClient/ChatClient.hpp>
#include "ProxyChatClientListener.h"

using namespace ChatClientRC;
using namespace Platform;

ChatClientRtC::ChatClientRtC():
m_chatClient(new ChatClient())
{
}

void ChatClientRtC::setServerProperties(Platform::String^ address, int port)
{
	m_chatClient->setServerProperties(ToStdString(address), port);

}
void ChatClientRtC::login(Platform::String^ userName, Platform::String^ password)
{
	m_chatClient->login(ToStdString(userName), ToStdString(password));
}

void ChatClientRtC::sendMessage(int receiverId, Platform::String^ message)
{
	m_chatClient->sendMessage(receiverId, ToStdString(message));
}

void ChatClientRtC::disconnect()
{
	m_chatClient->disconnect();
}


void ChatClientRtC::setNotificationCallbacks(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m)
{
	std::shared_ptr<IChatClientListener> listener(new ProxyChatClientListener(c, d, m));
	m_chatClient->addListener(listener);
}
