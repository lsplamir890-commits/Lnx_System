@echo off 
del resources\lnkrnl.elf
echo "random" > resources/lnkrnl.elf

echo LNXOS builder 
echo If it rebuilds again. let it it will re-compile the resources inside it
cosmos build -c release
copy output-x64\lnkrnl.elf resources\lnkrnl.elf
echo "random" > resources/patata.exe
echo "random" > resources/patata.exe

echo "random" > resources/patata.exe
echo "random" > resources/patata.exe
echo "random" > resources/patata.exe

echo "random" > resources/patata.exe
cosmos build -c release
