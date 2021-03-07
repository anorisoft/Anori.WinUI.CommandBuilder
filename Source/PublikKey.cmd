@ECHO OFF
SET sn="C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\sn.exe"
%sn% -p Anorisoft.snk Anorisoft.PublicKey
%sn% -tp Anorisoft.PublicKey
PAUSE