FROM mcr.microsoft.com/windows/servercore:ltsc2019
WORKDIR /app
# This looks for the new folder you just made
COPY out/ .
ENTRYPOINT ["Transport_Management_System.exe"]