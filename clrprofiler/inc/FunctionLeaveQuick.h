#pragma once
#include "ICommand.h"
namespace Commands
{
	class FunctionLeaveQuick :
		public virtual ICommand
	{
	public:
		FunctionLeaveQuick(FunctionID data, ThreadID threadid, __int64 timestamp);
		~FunctionLeaveQuick();
		virtual std::shared_ptr<std::vector<char>> Encode();
		virtual std::shared_ptr<ICommand> Decode(std::shared_ptr<std::vector<char>> &data);
		virtual std::wstring Name();
		virtual std::wstring Description();
		virtual short Code() { return code; }

	private:
		__int64 function;
		__int64 thread;
		short code;
		std::shared_ptr<std::vector<char>> m_internalvector;
		bool hasEncoded;
		__int64 timestamp;
	};

}
