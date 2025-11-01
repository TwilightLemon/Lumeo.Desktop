# Lumeo.Desktop
A MyToolBar extension for displaying frames on the desktop.  
This is a simple sample project demonstrating how to develop extensions for MyToolBar.

![screenshot](https://raw.githubusercontent.com/TwilightLemon/Data/refs/heads/master/Lumeo.Desktop.jpg)

## Features

- Lumeo.Desktop.ImageSlideshowFrame
  - Display a slideshow of images on the desktop in SILENCE.
  - Support arbitrary zoom and pan.
  - More...

- Lumeo.Desktop.EverydayPoemFrame
- More utils coming soon!

## Install

1. Clone this repository and build with `dotnet build`.
2. Copy the main dll from `\bin\Release\net8.0-windows\Lumeo.Desktop.dll` to your MyToolBar extensions directory.  
   A possible directory structure is:  
   ```
   MyToolBar(Install location)
   └── Plugins
       └── Lumeo.Desktop
           └── Lumeo.Desktop.dll
   ```
3. Restart MyToolBar to load the new extension.
4. Enable the Lumeo.Desktop extensions in the Service page.