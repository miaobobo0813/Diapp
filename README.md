# Diapp

A privacy-focused personal diary application.

## Features

- **Modern UI**: Developed with the WinUI 3 framework for an elegant interface and excellent performance.
- **Lightweight**: The installed application is only about 11.9 MB in size.
- **Highly Secure**:
    - Supports setting independent passwords for each diary entry.
    - Custom encryption algorithm using multi-factor authentication (username, password, etc.).
    - Unlimited maximum password length for ultra-high-strength passwords.
- **Easy Installation**: Packaged as MSIX for a one-click installation experience.
- **Fully Open Source**: Released under the MIT License with all code publicly available.

## Installation

1.  Navigate to the `Installer` folder and select the subfolder corresponding to your system architecture (e.g., x64, ARM64).
2.  **Install the Certificate First**: Double-click the `Diapp_2.1.0.0_[architecture].cer` certificate file and follow the prompts to install it into the "Trusted Root Certification Authorities" store.
3.  **Install the App**: Double-click the `Diapp_2.1.0.0_[architecture].msix` file to install the application.

## Tech Stack

- **Framework**: WinUI 3, C#
- **Runtime**: Windows 10 2004 (Build 19041) / Windows 11

## License

This project is released under the [MIT License](./LICENSE).
