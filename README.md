# 🔒 Encrypted Image Viewer (EIMG)

**Encrypted Image Viewer (EIMG)** is a secure image viewer that allows users to encrypt and decrypt images using AES encryption. With EIMG, you can protect your images with a password, ensuring that only authorized users can access them.

## 🛡️ Features
- 🔐 **AES Encryption** – Encrypt images with a secure password.
- 🔓 **Secure Decryption** – Only the correct password can unlock your images.
- 🖼️ **Supports Common Image Formats** – JPG, PNG, BMP, etc.
- ⚡ **Fast & Lightweight** – Optimized for performance.
- 🛑 **No Password Recovery** – For maximum security, forgotten passwords cannot be recovered.

## ❗ Important Notice
Once an image is encrypted, **the password cannot be recovered** if lost. Without the correct password, the image will remain inaccessible permanently. Be sure to store your password safely!

---

## 🚀 Getting Started
### 📦 Prerequisites
- Windows 8.1 or later

### 📥 Download
You can download the latest release of **Encrypted Image Viewer (EIMG)** from the **Releases** section on GitHub:

🔽 **[Download Latest Version](https://github.com/korayustundag/eimg/releases)**  

### Installation:
1. Download the latest **Setup file (`eimg_x64_setup.exe`)** from the link above.
2. Run the installer and follow the on-screen instructions.
3. Once installed, launch **Encrypted Image Viewer (EIMG)** from the Start Menu or Desktop shortcut.

---

## 🛠️ Technical Details
- **Language:** C# (WPF)
- **Encryption Algorithm:** AES (Advanced Encryption Standard)
- **File Format:** `.eimg` (Custom Encrypted Image Format)
- **Framework:** .NET 8.0+

### ⚙️ File Structure
EIMG uses **AES encryption** to protect images. The encrypted file structure is as follows:
```
|-- Magic Header (4 Bytes)
|-- AES IV (16 Bytes)
|-- Salt (16 Bytes)
|-- Encrypted Image Data
```

When decrypting an image:
1. The file's **Magic Header** is verified.
2. The AES **IV** and **Salt** are extracted.
3. The user-provided password is processed to generate the decryption key.
4. The image is decrypted and displayed as a `BitmapFrame`.
---

## 🔑 Security Considerations
- **EIMG does not store passwords.** Always remember your password!
- **No recovery mechanism** exists if a password is lost.
- **AES encryption ensures strong security** when used with a strong password.

---

## 📜 License
This project is licensed under the **GNU General Public License v3.0 (GPLv3)**.  
See the [LICENSE](https://github.com/korayustundag/eimg/blob/main/LICENSE) file for more details.

---

## 🤝 Contributing
Contributions are welcome! Feel free to submit a pull request or open an issue.
