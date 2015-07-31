#include "pch.h"
#include "ChatClientRtC.h"
#include "utils.h"
#include <ChatClient/ChatClient.h>
#include "ProxyChatClientListener.h"

using namespace ChatClientRC;
using namespace Platform;

ChatClientRtC::ChatClientRtC():
m_chatClient(new ChatClient())
{
}

void ChatClientRtC::connect(Platform::String^ address, uint16 port)
{
	m_chatClient->setServerProperties(ToStdString(address), port);
	m_chatClient->login("user3", "pwd3");

}


void ChatClientRtC::sendMessage(Platform::String^ message)
{
	m_chatClient->sendMessage(ToStdString(message));
}

void ChatClientRtC::disconnect()
{
	m_chatClient->disconnect();
}


void ChatClientRtC::setNotificationCallbacks(onConnectedCallback^ c, onDisconnectedCallback^ d, onMessageCallback^ m)
{
	std::shared_ptr<IChatClientListener> listener(new ProxyChatClientListener(c, d, m));
	m_chatClient->addChatClientListener(listener);
}
