#include "NativeChatClientRC.hpp"
#include "utils.h"
#include <ChatClient/ChatClient.hpp>
#include "RCChatClientListener.hpp"

using namespace ChatClientRC;
using namespace Platform;

NativeChatClientRC::NativeChatClientRC() :
m_chatClient(new ChatClient())
{
}

void NativeChatClientRC::setServerProperties(Platform::String^ address, int port)
{
	m_chatClient->setServerProperties(ToStdString(address), port);

}
void NativeChatClientRC::login(Platform::String^ userName, Platform::String^ password)
{
	m_chatClient->login(ToStdString(userName), ToStdString(password));
}

void NativeChatClientRC::sendMessage(int receiverId, Platform::String^ message)
{
	m_chatClient->sendMessage(receiverId, ToStdString(message));
}

void NativeChatClientRC::disconnect()
{
	m_chatClient->disconnect();
}


void NativeChatClientRC::setNotifier(RCChatClientNotifier^ notifier)
{
	std::shared_ptr<IChatClientListener> listener(new RCChatClientListener(notifier));
	m_chatClient->addListener(listener);
}
