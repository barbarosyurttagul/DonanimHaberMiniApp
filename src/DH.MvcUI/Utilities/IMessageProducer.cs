using System;
namespace DH.MvcUI.Utilities
{
	public interface IMessageProducer
	{
		void SendPostMessage<T>(T message);
	}
}

