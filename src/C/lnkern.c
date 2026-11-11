#include "Cosmos.h"

int Buffer_Cnt()
{
    return sysBffrCnter(64,64);
}
int Kernel_C(int safeToInitialize)
{
    CosmosInit();

    if(safeToInitialize == 1)
    {
        return 1;
    }
    
    return WriteToBuffer("Test text hello hello hellooooo",buffer);
}
