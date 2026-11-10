#include <stdbool.h>
#include <stdint.h>
char* buffer;
int CosmosInit();
int sysBffrCnter(int buffer,int add);
int Console_WriteLine(const char* text, const char* bffr);
int WriteToBuffer(const char* text, char* bffr);