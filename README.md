[Unity]: https://unity.com/
[Fish-Networking]: https://github.com/FirstGearGames/FishNet/
[UniVoice]: https://github.com/adrenak/univoice

# FishyVoice

FishyVoice is [Fish-Networking] and [UniVoice] based voice service for Unity. It allows developers to provide voice communication to their users without needing to utilize (often costly) third-party services, everything can be routed through whatever hosting provider is running hosting your primary servers!

**NOTE:** This repository has been extracted from our larger project: https://github.com/hpcvis/MuVR. It was deemed that FishyVoice would likely be useful on its own for projects that don't need all of the VR support uMuVR provides!

**NOTE: This is alpha-quality software, it works for our cases but I am sure many edge cases haven't been checked. And I am unsure how much time I will have to support it...**

## Features

* Room Based Voice Communication
* Plug-And-Play
* Automatic Positional Audio Support
* (Disableable) Audio compression
* Based on UniVoice under the hood, thus our implementation can be completely discarded (or used as a base) in favor of further customization!

## Installation

The library is designed in such a way that you can download the repository from the code tab above, drop it into the assets folder of your project and you should be good to go. Cloning the repository (into your assets folder) should only be necessary if you would like to receive the sporadic updates the project gets automatically!

## Getting Started

The VoiceNetwork is responsible for all of FishNetworking integration! It also provides a function that creates a UniVoice agent utilizing it and the (optional) IAudioInput and IAudioOutputFactory passed in (see [UniVoice]'s documentation for more detailed descriptions of these interfaces).

We integrate very closely with [Fish-Networking], look at their documentation for more specifics. The samples directory includes an example of how to set up a scene that adds a VoiceNetwork to the default Fish-Networking sample menu. The samples directory also includes an example of how to adapt the basic example to support Positional Audio! This requires the addition of a PlayerAudioPositionReference which will record the position of the player and automatically transfer it through the network (the PositionalAudioOutput provides a completely functional example of how this data can then be utilized on the receiving end!)
