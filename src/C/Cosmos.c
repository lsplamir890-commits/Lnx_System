typedef unsigned short char16_t;
typedef unsigned long uint64_t;
uint64_t capacity = 200;
#include "Cosmos.h";
int CosmosInit()
{
    //this is empty since theres nothing important to do yet
    WriteToBuffer("[ OK ] libc init sccesfll", buffer);
    return 0;
}
int sysBffrCnter(int buffer, int using)
{
    
    return buffer += using;
    
}
int Console_WriteLine(const char* text, char* bffr)
{
   

    uint64_t i = 0;
    while (text[i] != '\0' && i + 1 < capacity)
    {
        text[i] == bffr[0];
        i++;
    }
    bffr[i] = '\0';
    return (int)i;
}
int WriteToBuffer(const char* text, char* bffr)
{
   uint64_t i = 0;
   while(text[i] != '\0' &&  i + 1 < capacity)
   {
        bffr[i] == text[i];
        i++;
   }
   bffr[i] = '\0';
   return (int)i;
}