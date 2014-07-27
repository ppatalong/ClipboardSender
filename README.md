ClipboardSender
===============
ClipboardSender- read the text from clipboard and sends it by simulating key press. 

How it work:
- Check whether the clipboard contains valid text. 
- Parse the text from clipboard, into 'send able' format - keys
- Send the keys string to the previous focused window

The WinAPI is used to get the previous focused window. 