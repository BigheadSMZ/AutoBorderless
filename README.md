# AutoBorderless
An application that can force windowed mode applications into fullscreen borderless window.

<img width="333" height="397" alt="image" src="https://github.com/user-attachments/assets/e98b6ff9-5bca-4227-a4e2-6bb13d75b4ec" />

# Purpose
This is a simple application written in C# .NET Framework v4.8 that forces windowed applications into fullscreen borderless window. Most useful for games that either don't have a proper fullscreen mode, old games that struggle with fullscreen on modern hardware, or games that are locked to resolutions lower than your monitor's maximum resolution. For example, a game that runs at 1080p no matter what and you have a 1440p monitor. Do note that this does not increase the resolution, rather it is stretched to fit the window. This is obviously most useful for pixel art games. Many of the earlier games by IntiCreates have issues that this application was designed to fix.

The main feature of this application is to automate launching a game in fullsceen borderless window. Once it's configured, simply launch the executable and the game window will open and immediately enter fullscreen borderless window. Unlike other applications, this can be used to set a permanent configuration within the game folder. 

# How to Use
There are two methods that this application can employ to force a game into fullscreen borderless window mode. You only need to use one of them and fill in one of the two textboxes.

<ins>**Method 1:**</ins> AutoBorderless will launch an executable by name, wait for the window to appear, and apply fullscreen borderless window.
- Copy **AutoBorderless.exe** to the same folder as the executable you want to launch.
- Open it up. The first run will show a GUI where the executable name can be entered.
- Enter the name of the executable into the **Executable** section textbox. Omit *".exe"* from the name.
- Optionally use **Create Shortcut** to create a shortcut somewhere to launch the game in borderless.
- Press the **Set Borderless** button. This will launch the executable and apply borderless to the window.
- Close out AutoBorderless. Future runs will remember launching the executable and not display the GUI.
- To see the GUI again, delete the INI file or set **Executable** and **SearchString** to empty text.

<ins>**Method 2:**</ins> AutoBorderless can search for an existing process by executable/process name or by searching for a window title.
- Open **AutoBorderless.exe** from anywhere. It does not need to be in the same folder as the executable.
- The first run will show a GUI where the **Search String** can be entered.
- Here you can enter either the name of a running executable or the title of the window.
- Press the **Set Borderless** button. This will find a matching window and apply fullscreen borderless to it.
- Close out AutoBorderless. Future runs will remember the search string and not display the GUI.
- To see the GUI again, delete the INI file or set **Executable** and **SearchString** to empty text.

Most borderless window applications employ method 2, which always requires at least two steps: open the application, apply borderless. The beauty of method 1 is that once it's set up, launching the game with borderless fullscreen window is as simple as opening the application.

# Caveats/Shortcomings
As with all my applications I like to cover any limitations.
- Window to fullscreen borderless window stretches the image. It is not a resolution increase.
- It may not work with all games or applications. Very limited testing has been done.

# Why I Created It
While I've been using Lossless Scaling to handle most of my upscaling needs, I was thinking how nice it would be to have an automated process where I just open an icon and the game is automatically fullscreen. Many of the IntiCreates earlier games struggle with fullscreen mode, and sometimes I just want to launch and play with no additional configuration. Some examples are Bloodstained: Curse of the Moon, Mighty Gunvolt Burst, and Gunvolt Chronicles - Luminous Avenger iX. Fortunately they fixed later titles. There are also a few other games, like indie or homebrew games that have only a window mode. With this application, I can now play them fullscreen with relative ease. Double click, go.

# Credits
To be honest, I initially got a lot of help from ChatGPT. It proved to be an invaluable resource for a novice like me.
- **[fullwin](https://github.com/0dm/fullwin):** Simple borderless application by [@0dm](https://github.com/0dm) which was a great reference to some of the windows API calls.
- **[Modern FolderSelectDialog](https://stackoverflow.com/questions/66823581/use-the-upgraded-folderbrowserdialog-vista-style-in-powershell):** Ben Philipp at stackoverflow
- **[INI File System](https://stackoverflow.com/questions/217902/reading-writing-an-ini-file):** Danny Beckett at stackoverflow.
