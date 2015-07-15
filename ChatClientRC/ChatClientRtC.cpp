#include "pch.h"
#include "ChatClientRtC.h"
#include "utils.h"
#include <chatClientAPI\ChatClient.h>

using namespace ChatClientRC;
using namespace Platform;

ChatClientRtC::ChatClientRtC():
m_chatClient(new ChatClient())
{
}

void ChatClientRtC::initialize()
{
	m_chatClient->initialize();
}

void ChatClientRtC::connect(Platform::String^ address, uint16 port)
{
	m_chatClient->connect(ToStdString(address));
}

void ChatClientRtC::startService()
{
	m_chatClient->startService();
}

void ChatClientRtC::sendMessage(Platform::String^ message)
{
	m_chatClient->sendMessage(ToStdString(message));
}

void ChatClientRtC::closeConnection()
{
	m_chatClient->closeConnection();

}
