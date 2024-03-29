﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace RDPLoader
{
    public enum Store
    {
        USE_MACHINE_STORE = 1,
        USE_USER_STORE
    }

    class DPCryptor
    {
        public Store CurrentStore;

        private const int CRYPTPROTECT_UI_FORBIDDEN = 0x1;

        // Wrapper for the NULL handle or pointer.
        static private IntPtr NullPtr = ((IntPtr)((int)(0)));

        // Wrapper for DPAPI CryptProtectData function.
        [DllImport("crypt32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern bool CryptProtectData(ref DATA_BLOB pPlainText,
                                                    [MarshalAs(UnmanagedType.LPWStr)]string szDescription,
                                                    IntPtr pEntroy,
                                                    IntPtr pReserved,
                                                    IntPtr pPrompt,
                                                    int dwFlags,
                                                    ref DATA_BLOB pCipherText);
        
        // BLOB structure used to pass data to DPAPI functions.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;
        }

        private void InitBLOB(byte[] data, ref DATA_BLOB blob)
        {
            blob.pbData = Marshal.AllocHGlobal(data.Length);
            if (blob.pbData == IntPtr.Zero)
                throw new Exception("Unable to allocate buffer for BLOB data.");

            blob.cbData = data.Length;
            Marshal.Copy(data, 0, blob.pbData, data.Length);
        }

        public string Encrypt(string plainText)
        {
            bool success = false;
            byte[] bTextIn = Encoding.Unicode.GetBytes(plainText);
            byte[] bTextOut;
            DATA_BLOB dataIn = new DATA_BLOB();
            DATA_BLOB dataOut = new DATA_BLOB();
            StringBuilder encryptedText = new StringBuilder();

            try
            {
                try
                {
                    InitBLOB(bTextIn, ref dataIn);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot initialize dataIn BLOB.", ex);
                }

                success = CryptProtectData(ref dataIn, "psw", NullPtr, NullPtr, NullPtr, CRYPTPROTECT_UI_FORBIDDEN, ref dataOut);

                if (!success)
                {
                    int errCode = Marshal.GetLastWin32Error();
                    throw new Exception("CryptProtectData failed.", new Win32Exception(errCode));
                }

                bTextOut = new byte[dataOut.cbData];
                Marshal.Copy(dataOut.pbData, bTextOut, 0, dataOut.cbData);
                // Convert hex data to hex characters (suitable for a string)
                for (int i = 0; i < dataOut.cbData; i++)
                    encryptedText.Append(Convert.ToString(bTextOut[i], 16).PadLeft(2, '0').ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception("unable to encrypt data.", ex);
            }
            finally
            {
                if (dataIn.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(dataIn.pbData);

                if (dataOut.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(dataOut.pbData);
            }
            return encryptedText.ToString();
        }        
    }
}
