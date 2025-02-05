#pragma warning disable CA1416 // Validate platform compatibility (Windows Only) (for AesCng)
using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;

namespace eimg
{
    internal static class Core
    {
        private static readonly byte[] MagicHeader = { 0x45, 0x49, 0x05, 0x04 };

        private static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] DeriveKey(string password, byte[] salt, int keySize)
        {
            using (Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256))
            {
                return pdb.GetBytes(keySize);
            }
        }

        private static bool CompareHeaders(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }

        internal static void EncryptImage(string imagePath, string outputPath, string password)
        {
            using (AesCng aes = new AesCng())
            {
                aes.KeySize = 256;
                aes.BlockSize = 128;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                aes.GenerateIV();
                byte[] salt = GenerateSalt(16);
                
                aes.Key = DeriveKey(password, salt, aes.KeySize / 8);

                using (FileStream fsOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            fsOutput.Write(MagicHeader, 0, MagicHeader.Length);
                            fsOutput.Write(aes.IV, 0, aes.IV.Length);
                            fsOutput.Write(salt, 0, salt.Length);
                            fsInput.CopyTo(cs);
                        }
                    }
                }
            }
        }

        internal static BitmapFrame DecryptImage(string encryptedPath, string password)
        {
            try
            {
                using (FileStream fsInput = new FileStream(encryptedPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] header = new byte[4];
                    fsInput.Read(header, 0, header.Length);
                    if (!CompareHeaders(header, MagicHeader))
                    {
                        throw new InvalidDataException("Invalid file format.");
                    }

                    byte[] iv = new byte[16];
                    fsInput.Read(iv, 0, iv.Length);

                    byte[] salt = new byte[16];
                    fsInput.Read(salt, 0, salt.Length);

                    using (AesCng aes = new AesCng())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        aes.Key = DeriveKey(password, salt, aes.KeySize / 8);
                        aes.IV = iv;

                        using (CryptoStream cs = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                cs.CopyTo(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                return BitmapFrame.Create(ms, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                                // return BitmapFrame.Create(ms, BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.OnLoad);
                            }
                        }
                    }
                }
            }
            catch (CryptographicException)
            {
                throw new UnauthorizedAccessException("Decryption failed: Incorrect password or corrupted file.");
            }
            catch (Exception ex)
            {
                throw new IOException("An error occurred while decrypting the file.", ex);
            }
        }
    }
}