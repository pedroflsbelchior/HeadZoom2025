# HeadZoom2025

This repository contains an up-to-date copy of HeadZoom, a head and eye tracking interaction research prototype built under project Xavier.

This prototype was build on Unity 6 version 43+ and requires the following assets to run as-is:

- Meta XR SDK (All-in-one) - For headset, controller and hand interactions
- Shapes - For the ring target and environment grid
- SOAP - For the data architecture
- Modern UI SFX - For the UI sound effects
- Super Confetti FX - For the success visual effect
- GUI Pro - Super Casual - For the UI textures and fonts
- AVPro Movie Capture - For the video/audio recording. We used the Ultra Edition, but the version for your specific platform will suffice.

Voice clips were synthesized using Google Cloud's Text-to-Speech service.

- If you own and install the AVPro Movie Capture asset, uncomment the appropriate lines in the file AVProConnector.cs
- If you own and install the Shapes asset, uncomment the appropriate lines in the files EyeTrackingTarget.cs and FloorAndCeilingGrid.cs
