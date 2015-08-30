   
 
 
/****************************************************************************************
*
* This code was generated by the Command Creator tool for the ChainsAPM project. 
* If manual changes are made to the code they could be lost.
* 
****************************************************************************************/ 
#pragma once
#include "ICommand.h"

namespace Commands  
{
    class MethodsToInstrument :
        public virtual ICommand
    {


    public:
            enum class MethodProperties
        {
          Public = 0x00000001,
          Protected = 0x00000004,
          Private = 0x00000008,
          Friend = 0x00000010,
          Abstract = 0x00000020,
          Static = 0x00000040,
          IgnoreGetSet = 0x00000080,
          EntireClass = 0x00000100
  
        };

        MethodsToInstrument(__int64 timestamp, std::vector<MethodProperties> methodproplist, std::vector<std::wstring> methodclasslist, std::vector<std::wstring> methodlist); 
        ~MethodsToInstrument();
        virtual std::shared_ptr<std::vector<char>> Encode();
        virtual std::shared_ptr<ICommand> Decode(std::shared_ptr<std::vector<char>> &data);  
        virtual std::wstring Name();
        virtual std::wstring Description();
        virtual short Code() { return code; }
        std::vector<MethodProperties> MethodPropList;
        std::vector<std::wstring> MethodClassList;
        std::vector<std::wstring> MethodList;
      private:
        __int64 timestamp; // Always use a 64bit so the message doesn't change
        std::shared_ptr<std::vector<char>> internalvector;
        bool hasEncoded;
        short code;
    };

}